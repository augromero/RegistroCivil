using System;

namespace RegistroCivil.Dominio.DTOs
{
    public class SolcitudCreacionPersona
    {
        public string Tipo { get; }
        public string Numero { get; }
        public string Nombres { get; }
        public string Apellidos { get; }
        public DateTime FechaNacimiento { get; }

        public SolcitudCreacionPersona(string tipo, string numero, string nombres, string apellidos, DateTime fechaNacimiento)
        {
            Tipo = tipo;
            Numero = numero;
            Nombres = nombres;
            Apellidos = apellidos;
            FechaNacimiento = fechaNacimiento;
        }
    }
}