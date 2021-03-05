using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroCivil.Dominio.Entidades;

namespace RegistroCivil.Dominio.DTOs
{
    public class PersonaPersistencia
    {
        private PersonaPersistencia()
        {
        }

        public string Tipo { get; private set; }
        public string NumeroIdentificacion { get; private set; }
        public string Nombres { get; private set; }
        public string Apellidos { get; private set;}
        public DateTime FechaNacimiento { get; private set;}

        public PersonaPersistencia(string tipo, string numeroIdentificacion, string nombres, string apellidos, DateTime fechaNacimiento)
        {
            Tipo = tipo;
            NumeroIdentificacion = numeroIdentificacion;
            Nombres = nombres;
            Apellidos = apellidos;
            FechaNacimiento = fechaNacimiento;
        }

        public Persona APersona()
        {
            return Persona.CrearDesdePersistencia(this);
        }
    }
}
