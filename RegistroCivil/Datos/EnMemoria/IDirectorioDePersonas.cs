using System.Collections.Generic;
using RegistroCivil.Dominio.DTOs;
using RegistroCivil.Dominio.Entidades;
using RegistroCivil.Dominio.TiposIdentificacion;

namespace RegistroCivil.Datos.EnMemoria
{
    public interface IDirectorioDePersonas
    {
        void AgregarPersona(SolcitudCreacionPersona solicitudCreacion);
        List<string> ObtenerTodasLasPersonas();
        void EliminarPersona(Identificacion identificacion);
        Persona ObtenerPersona(Identificacion identificacion);
        int CantidadDePersonas();
    }
}