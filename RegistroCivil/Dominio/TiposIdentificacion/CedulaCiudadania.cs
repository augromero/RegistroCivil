using System.Text.RegularExpressions;

namespace RegistroCivil.Dominio.TiposIdentificacion
{
    public class CedulaCiudadania : Identificacion
    {
        public static readonly string ErrorCedulaCiudadaniaDebeSerNumerica = "La c�dula de ciudadan�a debe ser num�rica.";

        public CedulaCiudadania(string numero) : base("CC", numero)
        {
            ValidarNumeroDocumento(numero, ErrorCedulaCiudadaniaDebeSerNumerica );
        }

        protected override Regex ExpresionDeValidacion  => new("^[0-9]+$");
    }
}