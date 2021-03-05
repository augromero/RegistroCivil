using System;
using System.Data;
using RegistroCivil.Dominio.DTOs;
using RegistroCivil.Dominio.TiposIdentificacion;

namespace RegistroCivil.Dominio.Entidades
{
    public class Persona : IEquatable<Persona>
    {
        public static readonly string ErrorDebeTenerNombre = "Debe tener nombre.";
        public static string ErrorDebeTenerApellidos = "Debe tener apellidos.";
        public static readonly string ErrorNoSePuedenRegistrarPersonasQueNoHayanNacidoPorFavorVerifiqueLaFechaDeNacimiento = "No se pueden registrar personas que no hayan nacido. Por favor verifique la fecha de nacimiento.";

        private readonly Identificacion _identificacion;
        private readonly string _nombres;
        private readonly string _apellidos;
        private readonly DateTime _fechaNacimiento;

        public static Persona CrearDesdeSolicitud(SolcitudCreacionPersona solicitud)
        {
            return new Persona(Identificacion.Crear(solicitud.Tipo, solicitud.Numero), solicitud.Nombres, solicitud.Apellidos, solicitud.FechaNacimiento);
        }

        public static Persona CrearDesdePersistencia(PersonaPersistencia personaPersistencia)
        {
            return new Persona(Identificacion.Crear(personaPersistencia.Tipo, personaPersistencia.NumeroIdentificacion), personaPersistencia.Nombres, personaPersistencia.Apellidos, personaPersistencia.FechaNacimiento);
        }

        private Persona(Identificacion identificacion, string nombres, string apellidos, DateTime fechaNacimiento)
        {
            LanzaExcepcionSiHayArgumentosIncompletos(nombres, apellidos);

            _identificacion= identificacion;
            _nombres = nombres;
            _apellidos = apellidos;
            _fechaNacimiento = fechaNacimiento;

            if (Edad < 0)
                throw new ConstraintException(ErrorNoSePuedenRegistrarPersonasQueNoHayanNacidoPorFavorVerifiqueLaFechaDeNacimiento);
        }

        public int Edad => _fechaNacimiento.AniosDeDiferencia(DateTime.Now);
        public string NombreCompleto => $"{_nombres} {_apellidos}";
        public string NombreCompletoConIdentificacion => $"{_identificacion} - {NombreCompleto}";
        public string NombreCompletoConIdentificacionYEdad => $"{NombreCompletoConIdentificacion} - Edad: {Edad} años.";
        
        public PersonaPersistencia ParaPersistir()
        {
            return new PersonaPersistencia(_identificacion.Tipo, _identificacion.Numero, _nombres, _apellidos, _fechaNacimiento);
        }

        public override string ToString()
        {
            return NombreCompletoConIdentificacionYEdad;
        }

        public bool EstaIdentificadoCon(Identificacion identificacion)
        {
            return _identificacion.Equals(identificacion);
        }

        private static void LanzaExcepcionSiHayArgumentosIncompletos(string nombres, string apellidos)
        {
            if (string.IsNullOrEmpty(nombres))
                throw new ArgumentException(ErrorDebeTenerNombre);

            if (string.IsNullOrEmpty(apellidos))
                throw new ArgumentException(ErrorDebeTenerApellidos);
        }

        public bool Equals(Persona other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _identificacion.Equals(other._identificacion);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Persona) obj);
        }

        public override int GetHashCode()
        {
            return _identificacion.GetHashCode();
        }

       
    }
}