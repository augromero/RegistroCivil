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
        
        private DirectorioDePersonasEnMemoria _directorioEnMemoria;
        private DirectorioDePersonasEnSqlServer _directorioSqlServer;

        [TestInitialize]
        public void Inicializar()
        {
            _directorioEnMemoria = new DirectorioDePersonasEnMemoria();


            _directorioSqlServer = new DirectorioDePersonasEnSqlServer(new DbContextOptionsBuilder<DirectorioDePersonasEnSqlServer>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}").Options);
        }

        [TestMethod]
        public void AgregaPersonaAlDirectorio()
        {
            AcertarAgregarPersona(_directorioEnMemoria);
            AcertarAgregarPersona(_directorioSqlServer);
        }

        private void AcertarAgregarPersona(IDirectorioDePersonas directorio)
        {
            directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "79879078", "Augusto", "Romero",
                new DateTime(1978, 12, 7)));

            var personaObtenida = directorio.ObtenerPersona(new CedulaCiudadania("79879078"));

            Assert.AreEqual("Augusto Romero", personaObtenida.NombreCompleto);
        }

        [TestMethod]
        public void LanzaErrorSiIntentaAgregarDosPersonasConLaMismaIdentificacion()
        {
            AcertarIntentoDePersonasRepetidas(_directorioEnMemoria);
            AcertarIntentoDePersonasRepetidas(_directorioSqlServer);
        }

        [TestMethod]
        public void LanzaErrorSiIntentaObtenerPersonaConIdentificacionInexistente()
        {
            AcertarIntentarObtenerPersonaNoRegistrada(_directorioEnMemoria);
            AcertarIntentarObtenerPersonaNoRegistrada(_directorioSqlServer);
        }

        private void AcertarIntentarObtenerPersonaNoRegistrada(IDirectorioDePersonas directorio)
        {
            var identificacion = new CedulaCiudadania("79879078");
            var ex = Assert.ThrowsException<ConstraintException>(() => directorio.ObtenerPersona(identificacion));

            Assert.AreEqual(
                string.Format(DirectorioDePersonasBase.ErrorLaPersonaIdentificadaConNoEstaRegistradaEnElDirectorio,
                    identificacion), ex.Message);
        }

        private void AcertarIntentoDePersonasRepetidas(IDirectorioDePersonas directorio)
        {
            directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "79879078", "Augusto", "Romero", new DateTime(1978, 12, 7)));
            
            var ex = Assert.ThrowsException<ConstraintException>(() => directorio
                .AgregarPersona(new SolcitudCreacionPersona("CC", "79879078", "Octavio Augusto", "Romero", new DateTime(1978, 12, 7))));

            Assert.AreEqual(
                DirectorioDePersonasBase.ErrorNoSePuedeRegistrarPorqueYaExisteUnaPersonaRegistradaConEsaIdentificacion,
                ex.Message);
        }

        [TestMethod]
        public void ObtenerTodasLasPersonasRetornaElListadoOrdenadoPorNombre()
        {
            AcertarRetornoDeTodasLasPersonasOrdenadas(_directorioEnMemoria);
            AcertarRetornoDeTodasLasPersonasOrdenadas(_directorioSqlServer);
        }

        private void AcertarRetornoDeTodasLasPersonasOrdenadas(IDirectorioDePersonas directorio)
        {
            directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "52427119", "Elena", "Jaramillo",
                new DateTime(1978, 5, 23)));
            directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "52118442", "Zenayda", "Gonzalez",
                new DateTime(1979, 2, 3)));
            directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "79879078", "Augusto", "Romero",
                new DateTime(1978, 12, 7)));

            var todasLasPersonas = directorio.ObtenerTodasLasPersonas();

            Assert.AreEqual(3, directorio.CantidadDePersonas());
            Assert.IsTrue(todasLasPersonas.First().Contains("Augusto Romero"));
            Assert.IsTrue(todasLasPersonas.Last().Contains("Zenayda Gonzalez"));
        }

        [TestMethod]
        public void EliminaUnaPersonaDelDirectorio()
        {
            AcertarEliminacionPersona(_directorioEnMemoria);
            AcertarEliminacionPersona(_directorioSqlServer);
        }

        private void AcertarEliminacionPersona(IDirectorioDePersonas directorio)
        {
            directorio.AgregarPersona(new SolcitudCreacionPersona("CC", "79879078", "Augusto", "Romero",
                new DateTime(1978, 12, 7)));
            directorio.EliminarPersona(new CedulaCiudadania("79879078"));

            Assert.AreEqual(0, directorio.CantidadDePersonas());
        }

        [TestMethod]
        public void LanzaErrorSiIntentaEliminarUnaPersonaQueNoEstaRegistrada()
        {
            AcertarEliminacionPersonaInexistente(_directorioEnMemoria);
            AcertarEliminacionPersonaInexistente(_directorioSqlServer);
        }

        private void AcertarEliminacionPersonaInexistente(IDirectorioDePersonas directorio)
        {
            var identificacion = new CedulaCiudadania("79879078");
            var ex = Assert.ThrowsException<ConstraintException>(() => directorio.EliminarPersona(identificacion));

            Assert.AreEqual(
                string.Format(DirectorioDePersonasBase.ErrorLaPersonaIdentificadaConNoEstaRegistradaEnElDirectorio,
                    identificacion), ex.Message);
        }
    }
}