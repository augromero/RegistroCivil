using System.Text.RegularExpressions;

namespace RegistroCivil.Dominio.TiposIdentificacion
{
    public class Pasaporte : Identificacion
    {
        public static readonly string ErrorElPasaporteDebeEmpezarPorDosLetrasYTerminarConCincoNumeros = "El pasaporte debe empezar con dos letras y terminar con cinco números.";

        public Pasaporte(string numero) : base("PA", numero)
        {
            ValidarNumeroDocumento(numero, ErrorElPasaporteDebeEmpezarPorDosLetrasYTerminarConCincoNumeros);
        }

        protected override Regex ExpresionDeValidacion => new("^[A-Za-z]{2}[0-9]{5}$");
    }
}