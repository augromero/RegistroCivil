using System.Text.RegularExpressions;

namespace RegistroCivil.Dominio.TiposIdentificacion
{
    public class TarjetaDeIdentidad : Identificacion
    {
        public static readonly string ErrorTarjetaDeIdentidadDebeSerNumerica = "La tarjeta de identidad debe ser numérica.";

        public TarjetaDeIdentidad(string numero) : base("TI", numero)
        {
            ValidarNumeroDocumento(numero, ErrorTarjetaDeIdentidadDebeSerNumerica );
        }

        protected override Regex ExpresionDeValidacion  => new("^[0-9]+$");
    }
}