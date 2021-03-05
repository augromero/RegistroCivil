using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RegistroCivil.Datos.EnMemoria;
using RegistroCivil.Dominio.DTOs;
using RegistroCivil.Dominio.TiposIdentificacion;

namespace RegistroCivil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorioController : ControllerBase
    {
        private readonly DirectorioDePersonasEnMemoria _directorio;

        public DirectorioController(DirectorioDePersonasEnMemoria directorio)
        {
            _directorio = directorio;
        }

        [HttpGet]
        public List<string> ObtenerTodos()
        {
            return _directorio.ObtenerTodasLasPersonas();
        }

        [HttpPost]
        public List<string> Adicionar(SolcitudCreacionPersona solicitud)
        {
            _directorio.AgregarPersona(solicitud);
            return _directorio.ObtenerTodasLasPersonas();
        }

        [HttpDelete("TipoDocumento={tipo}&NumeroDocumento={numero}")]
        public List<string> Eliminar(string tipo, string numero)
        {
            var identificacion = Identificacion.Crear(tipo, numero);
            _directorio.EliminarPersona(identificacion);

            return _directorio.ObtenerTodasLasPersonas();
        }
    }
}
