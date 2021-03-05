using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistroCivil.Dominio.TiposIdentificacion;

namespace RegistroCivilTests.Dominio
{
    [TestClass]
    public class IdentificacionTest
    {
        [TestMethod]
        public void CedulaDeCiudadaniaDebeSerNumerica()
        {
            Assert.AreEqual("CC 79879078", Identificacion.Crear("CC", "79879078").ToString());
        }

        [TestMethod]
        public void TarjetaDeIdentidadDebeSerNumerica()
        {
            Assert.AreEqual("TI 79879078", Identificacion.Crear("TI", "79879078").ToString());
        }

        [TestMethod]
        public void PasaporteDebeEmpeazarPorDosLetrasYTerminarCon5Numeros()
        {
            Assert.AreEqual("PA KK79879", Identificacion.Crear("PA", "KK79879").ToString());
        }

        [TestMethod]
        public void LanzaErrorSiCedulaNoEsNumerica()
        {
            var ex = Assert.ThrowsException<FormatException>(() => Identificacion.Crear("CC", "A-123456"));
            Assert.AreEqual(CedulaCiudadania.ErrorCedulaCiudadaniaDebeSerNumerica, ex.Message);
        }

        [TestMethod]
        public void LanzaErrorSiTarjetaDeIdentidadNoEsNumerica()
        {
            var ex = Assert.ThrowsException<FormatException>(() => Identificacion.Crear("TI", "781207-01125"));
            Assert.AreEqual(TarjetaDeIdentidad.ErrorTarjetaDeIdentidadDebeSerNumerica, ex.Message);
        }

        [TestMethod]
        public void LanzaErrorSiPasaporteIniciaPorNumero()
        {
            var ex = Assert.ThrowsException<FormatException>(() => Identificacion.Crear("PA", "79879KK"));
            Assert.AreEqual(Pasaporte.ErrorElPasaporteDebeEmpezarPorDosLetrasYTerminarConCincoNumeros, ex.Message);
        }

        [TestMethod]
        public void LanzaErrorSiPasaporteTieneUnaSolaLetra()
        {
            var ex = Assert.ThrowsException<FormatException>(() => Identificacion.Crear("PA", "K79879"));
            Assert.AreEqual(Pasaporte.ErrorElPasaporteDebeEmpezarPorDosLetrasYTerminarConCincoNumeros, ex.Message);
        }

        [TestMethod]
        public void LanzaErrorSiPasaporteTieneMenosDe5Numeros()
        {
            var ex = Assert.ThrowsException<FormatException>(() => Identificacion.Crear("PA", "KK7987"));
            Assert.AreEqual(Pasaporte.ErrorElPasaporteDebeEmpezarPorDosLetrasYTerminarConCincoNumeros, ex.Message);
        }

        [TestMethod]
        public void LanzaErrorSiPasaporteTieneMasDe5Numeros()
        {
            var ex = Assert.ThrowsException<FormatException>(() => Identificacion.Crear("PA", "KK798790"));
            Assert.AreEqual(Pasaporte.ErrorElPasaporteDebeEmpezarPorDosLetrasYTerminarConCincoNumeros, ex.Message);
        }
    }
}