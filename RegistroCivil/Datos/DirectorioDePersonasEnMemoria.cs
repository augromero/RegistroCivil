using System.Collections.Generic;
using System.Data;
using System.Linq;
using RegistroCivil.Dominio.DTOs;
using RegistroCivil.Dominio.Entidades;
using RegistroCivil.Dominio.TiposIdentificacion;

namespace RegistroCivil.Datos.EnMemoria
{
    public class DirectorioDePersonasEnMemoria : DirectorioDePersonasBase, IDirectorioDePersonas
    {
        private readonly List<Persona> _directorio = new List<Persona>();

        public void AgregarPersona(SolcitudCreacionPersona solicitudCreacion)
        {
            var personaAAgregar = Persona.CrearDesdeSolicitud(solicitudCreacion);
            if (ExistePersona(personaAAgregar))
                throw new ConstraintException(ErrorNoSePuedeRegistrarPorqueYaExisteUnaPersonaRegistradaConEsaIdentificacion);

            _directorio.Add(personaAAgregar);
        }

        public List<string> ObtenerTodasLasPersonas()
        {
            return _directorio.OrderBy(d => d.NombreCompleto).Select(p => p.ToString()).ToList();
        }

        public void EliminarPersona(Identificacion identificacion)
        {
            var personaAEliminar = ObtenerPersona(identificacion);

            _directorio.Remove(personaAEliminar);

        }

        public Persona ObtenerPersona(Identificacion identificacion)
        {
            var personaObtenida =  _directorio.FirstOrDefault(d => d.EstaIdentificadoCon(identificacion));
            if (personaObtenida == null)
                throw new ConstraintException(string.Format(ErrorLaPersonaIdentificadaConNoEstaRegistradaEnElDirectorio, identificacion));

            return personaObtenida;
        }

        private bool ExistePersona(Persona persona)
        {
            return _directorio.Any(d => d.Equals(persona));
        }

        public int CantidadDePersonas()
        {
            return _directorio.Count;
        }
    }
}