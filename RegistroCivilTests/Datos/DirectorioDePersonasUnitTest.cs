using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistroCivil.Datos.EnMemoria;
using RegistroCivil.Dominio.DTOs;
using RegistroCivil.Dominio.TiposIdentificacion;

namespace RegistroCivilTests.Datos
{
    [TestClass]
    public class DirectorioDePersonasUnitTest
    {
        //En memoria
        //private DirectorioDePersonasEnMemoria _directorio;

        //relacional
        private DirectorioDePersonasEnSqlServer _directorio;

        [TestInitialize]
        public void Inicializar()
        {
            //test de la implementación en memoria
            //_directorio = new DirectorioDePersonasEnMemoria();


            //Test de la implementación relacional pero en sqlServerInMemory
            _directorio = new DirectorioDePersonasEnSqlServer(new DbContextOptionsBuilder<DirectorioDePersonasEnSqlServer>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}").Options);
        }

        [TestMethod]
        public void AgregaPersonaAlDirectorio()
        {
            _directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "79879078", "Augusto", "Romero", new DateTime(1978, 12, 7)));

            var personaObtenida = _directorio.ObtenerPersona(new CedulaCiudadania("79879078"));

            Assert.AreEqual("Augusto Romero", personaObtenida.NombreCompleto);
        }

        [TestMethod]
        public void LanzaErrorSiIntentaAgregarDosPersonasConLaMismaIdentificacion()
        {
            _directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "79879078", "Augusto", "Romero", new DateTime(1978, 12, 7)));
            var ex = Assert.ThrowsException<ConstraintException>(() => _directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "79879078", "Octavio Augusto", "Romero", new DateTime(1978, 12, 7))));

            Assert.AreEqual(DirectorioDePersonasBase.ErrorNoSePuedeRegistrarPorqueYaExisteUnaPersonaRegistradaConEsaIdentificacion, ex.Message);
        }

        [TestMethod]
        public void ObtenerTodasLasPersonasRetornaElListadoOrdenadoPorNombre()
        {
            _directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "52427119", "Elena", "Jaramillo", new DateTime(1978, 5, 23)));
            _directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "52118442", "Zenayda", "Gonzalez", new DateTime(1979, 2, 3)));
            _directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "79879078", "Augusto", "Romero", new DateTime(1978, 12, 7)));

            var todasLasPersonas = _directorio.ObtenerTodasLasPersonas();
            
            Assert.AreEqual(3, _directorio.CantidadDePersonas());
            Assert.IsTrue(todasLasPersonas.First().Contains("Augusto Romero"));
            Assert.IsTrue(todasLasPersonas.Last().Contains("Zenayda Gonzalez"));
        }

        [TestMethod]
        public void EliminaUnaPersonaDelDirectorio()
        {
            _directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "79879078", "Augusto", "Romero", new DateTime(1978, 12, 7)));
            _directorio.EliminarPersona(new CedulaCiudadania("79879078"));

            Assert.AreEqual(0, _directorio.CantidadDePersonas());
        }

        [TestMethod]
        public void LanzaErrorSiIntentaEliminarUnaPersonaQueNoEstaRegistrada()
        {
            var identificacion = new CedulaCiudadania("79879078");
            var ex = Assert.ThrowsException<ConstraintException>(() => _directorio.EliminarPersona(identificacion));

            Assert.AreEqual(string.Format(DirectorioDePersonasBase.ErrorLaPersonaIdentificadaConNoEstaRegistradaEnElDirectorio, identificacion), ex.Message);
        }
    }
}