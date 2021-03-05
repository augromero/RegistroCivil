using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistroCivil.Dominio;

namespace RegistroCivilTests.Dominio
{
    [TestClass]
    public class ExtensionDateTimeUnitTest
    {
        [TestMethod]
        public void AniosDeDiferenciaCuandoDiaDelAnioEvaluacionEsPosteriorAlDiaDelAnioDeLaBase()
        {
            var fechaInicial = new DateTime(1978, 12, 7);

            var fechaFinal = new DateTime(2021, 12, 31);

            Assert.AreEqual(43, fechaInicial.AniosDeDiferencia(fechaFinal));
        }

        [TestMethod]
        public void AniosDeDiferenciaCuandoDiaDelAnioEvaluacionEsAnteriorAlDiaDelAnioDeLaBase()
        {
            var fechaInicial = new DateTime(1978, 12, 7);

            var fechaFinal = new DateTime(2021, 12, 5);

            Assert.AreEqual(42, fechaInicial.AniosDeDiferencia(fechaFinal));
        }

        [TestMethod]
        public void AniosDeDiferenciaCuandoElDiaDeEvaluacionEsElMismoDelDiaBaseRetornaCero()
        {
            var fechaInicial = new DateTime(1978, 12, 7);

            var fechaFinal =  new DateTime(1978, 12, 7);

            Assert.AreEqual(0, fechaInicial.AniosDeDiferencia(fechaFinal));
        }

        [TestMethod]
        public void AniosDeDiferenciaCuandoLaFechaDeEvaluacionEsAnteriorALaBaseRetornaNegativo()
        {
            var fechaInicial = new DateTime(1978, 12, 7);

            var fechaFinal =  new DateTime(1978, 12, 6);

            Assert.AreEqual(-1, fechaInicial.AniosDeDiferencia(fechaFinal));
        }

    }
}