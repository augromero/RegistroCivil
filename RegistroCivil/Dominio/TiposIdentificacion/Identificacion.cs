using System;
using System.Text.RegularExpressions;

namespace RegistroCivil.Dominio.TiposIdentificacion
{
    public abstract class Identificacion : IEquatable<Identificacion>
    {
        public static Identificacion Crear(string tipoDocumento, string numeroDocumento)
        {
            return tipoDocumento switch
            {
                "PA" => new Pasaporte(numeroDocumento),
                "CC" => new CedulaCiudadania(numeroDocumento),
                "TI" => new TarjetaDeIdentidad(numeroDocumento),
                _ => throw new ArgumentException(ErrorElTipoDeDocumentoNoExiste)
            };
        }

        public static readonly string ErrorDebeTenerTipoDeDocumentoDeIdentidad = "Debe tener tipo de documento de identidad.";
        public static readonly string ErrorDebeTenerNumeroDeDocumentoDeIdentidad = "Debe tener número de documento de identidad.";
        public static readonly string ErrorElTipoDeDocumentoNoExiste = "El tipo de documento no existe.";

        protected Identificacion(string tipo, string numero)
        {
            LanzaExcepcionSiLosArgumentosEstanIncompletos(tipo, numero);

            Tipo = tipo;
            Numero = numero;
        }

        public string Tipo { get; private set; }
        public string Numero { get; private set; }
        protected abstract Regex ExpresionDeValidacion { get;  }

        private void LanzaExcepcionSiLosArgumentosEstanIncompletos(string tipo, string numero)
        {
            if (string.IsNullOrEmpty(tipo))
                throw new ArgumentException(ErrorDebeTenerTipoDeDocumentoDeIdentidad);

            if (string.IsNullOrEmpty(numero))
                throw new ArgumentException(ErrorDebeTenerNumeroDeDocumentoDeIdentidad);
        }

        protected void ValidarNumeroDocumento(string numero, string error)
        {
            if (ExpresionDeValidacion.IsMatch(numero) == false)
                throw new FormatException(error);
        }

        public override string ToString()
        {
            return $"{Tipo} {Numero}";
        }


        public bool Equals(Identificacion other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Numero == other.Numero;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Identificacion) obj);
        }

        public override int GetHashCode()
        {
            return Numero.GetHashCode();
        }
    }
}