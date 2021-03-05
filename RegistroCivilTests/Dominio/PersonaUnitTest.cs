using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistroCivil.Dominio.DTOs;
using RegistroCivil.Dominio.Entidades;
using RegistroCivil.Dominio.TiposIdentificacion;

namespace RegistroCivilTests.Dominio
{
    [TestClass]
    public class PersonaUnitTest
    {
        [TestMethod]
        public void CreaUnaPersonaConDatosCompletos()
        {
            const string numero = "79879078";
            const string nombres = "Augusto";
            const string apellidos = "Romero Arango";
            DateTime fechaNacimiento = new DateTime(1978, 12, 7);
            
            var solicitud = new SolcitudCreacionPersona("CC", numero, nombres, apellidos, fechaNacimiento);

            var persona = Persona.CrearDesdeSolicitud(solicitud);

            Assert.AreEqual("Augusto Romero Arango", persona.NombreCompleto);
            Assert.AreEqual("CC 79879078 - Augusto Romero Arango", persona.NombreCompletoConIdentificacion);
        }

        [TestMethod]
        public void LanzaErrorCuandoLefaltaNombreALaPersona()
        {
            const string numero = "79879078";
            const string nombres = "";
            const string apellidos = "Romero Arango";
            DateTime fechaNacimiento = new DateTime(1978, 12, 7);

            var solicitud = new SolcitudCreacionPersona("CC", numero, nombres, apellidos, fechaNacimiento);
         
            var ex = Assert.ThrowsException<ArgumentException>(() => Persona.CrearDesdeSolicitud(solicitud));
            Assert.AreEqual(Persona.ErrorDebeTenerNombre, ex.Message);

        }

        [TestMethod]
        public void LanzaErrorCuandoLeFaltaApellidoALaPersona()
        {
            const string tipo = "CC";
            const string numero = "79879078";
            const string nombres = "Augusto";
            const string apellidos = null;
            DateTime fechaNacimiento = new DateTime(1978, 12, 7);

            var solicitud = new SolcitudCreacionPersona(tipo, numero, nombres, apellidos, fechaNacimiento);
         
            var ex = Assert.ThrowsException<ArgumentException>(() => Persona.CrearDesdeSolicitud(solicitud));
            Assert.AreEqual(Persona.ErrorDebeTenerApellidos, ex.Message);
        }

        [TestMethod]
        public void LanzaErrorCuandoLeFaltaElTipoDeIdentificacion()
        {
            const string tipo = null;
            const string numero = "79879078";
            const string nombres = "Augusto";
            const string apellidos = "Romero Arango";
            DateTime fechaNacimiento = new DateTime(1978, 12, 7);
         
            var solicitud = new SolcitudCreacionPersona(tipo, numero, nombres, apellidos, fechaNacimiento);
         
            var ex = Assert.ThrowsException<ArgumentException>(() => Persona.CrearDesdeSolicitud(solicitud));
            Assert.AreEqual(Identificacion.ErrorElTipoDeDocumentoNoExiste, ex.Message);
        }

        [TestMethod]
        public void LanzaErrorCuandoLeFaltaElNumeroDeIdentificacion()
        {
            const string tipo = "CC";
            const string numero = null;
            const string nombres = "Augusto";
            const string apellidos = "Romero Arango";
            DateTime fechaNacimiento = new DateTime(1978, 12, 7);
         
            var solicitud = new SolcitudCreacionPersona(tipo, numero, nombres, apellidos, fechaNacimiento);
         
            var ex = Assert.ThrowsException<ArgumentException>(() => Persona.CrearDesdeSolicitud(solicitud));

            Assert.AreEqual(Identificacion.ErrorDebeTenerNumeroDeDocumentoDeIdentidad, ex.Message);
        }

        [TestMethod]
        public void LanzaErrorCuandoElTipoDeDocumentoNoExiste()
        {
            const string tipo = "XX";
            const string numero = "79879078";
            const string nombres = "Augusto";
            const string apellidos = "Romero Arango";
            DateTime fechaNacimiento = new DateTime(1978, 12, 7);
         
            var solicitud = new SolcitudCreacionPersona(tipo, numero, nombres, apellidos, fechaNacimiento);
         
            var ex = Assert.ThrowsException<ArgumentException>(() => Persona.CrearDesdeSolicitud(solicitud));

            Assert.AreEqual(Identificacion.ErrorElTipoDeDocumentoNoExiste, ex.Message);
        }

        [TestMethod]
        public void SiDosPersonasTienenElMismoNumeroDeDocumentoSonLaMismaPersona()
        {
            var persona1 = Persona.CrearDesdeSolicitud(new SolcitudCreacionPersona("TI", "79879078", "Augusto", "Romero", new DateTime(1978, 12, 7)));
            var persona2 = Persona.CrearDesdeSolicitud(new SolcitudCreacionPersona("CC", "79879078", "Octavio", "Romero Arango", new DateTime(1978, 12, 7)));

            Assert.AreEqual(persona1, persona2);
        }


        [TestMethod]
        public void LanzaErrorCuandoLaPersonaNoHaNacido()
        {
            var fechaNacimiento = new DateTime(2070, 12, 7);    
            var ex = Assert.ThrowsException<ConstraintException>(() => Persona.CrearDesdeSolicitud(new SolcitudCreacionPersona("TI", "79879078", "Augusto", "Romero", fechaNacimiento)));

            Assert.AreEqual(Persona.ErrorNoSePuedenRegistrarPersonasQueNoHayanNacidoPorFavorVerifiqueLaFechaDeNacimiento, ex.Message);
        }
    }
}
