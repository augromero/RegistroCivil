using System;

namespace RegistroCivil.Dominio
{
    public static class FechaExtensiones
    {
        public static int AniosDeDiferencia(this DateTime fechaBase, DateTime fechaEvaluacion)
        {
            var ajusteAnioActual = 0;
            if (DiaDelAnioDeEvaluacionEsAnteriorAlDiaDelAnioDeLaBase(fechaBase, fechaEvaluacion))
                ajusteAnioActual = -1;

            return fechaEvaluacion.Year - fechaBase.Year + ajusteAnioActual;
        }

        private static bool DiaDelAnioDeEvaluacionEsAnteriorAlDiaDelAnioDeLaBase(DateTime fechaBase, DateTime fechaEvaluacion)
        {
            return fechaBase.Month >= fechaEvaluacion.Month && fechaBase.Day > fechaEvaluacion.Day;
        }
    }
}