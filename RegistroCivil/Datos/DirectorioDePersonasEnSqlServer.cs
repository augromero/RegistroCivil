using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RegistroCivil.Dominio.DTOs;
using RegistroCivil.Dominio.Entidades;
using RegistroCivil.Dominio.TiposIdentificacion;

namespace RegistroCivil.Datos
{
    public class DirectorioDePersonasEnSqlServer : DbContext, IDirectorioDePersonas
    {
     
        public DirectorioDePersonasEnSqlServer(DbContextOptions<DirectorioDePersonasEnSqlServer> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonaPersistencia>(builder =>
            {
                builder.ToTable("Personas");
                builder.HasKey(p => p.NumeroIdentificacion);
                builder.Property(p => p.Tipo).HasColumnType("VARCHAR(4)").IsRequired();
                builder.Property(p => p.NumeroIdentificacion).HasColumnType("VARCHAR(16)");
                builder.Property(p => p.Apellidos).HasColumnType("VARCHAR(60)").IsRequired();
                builder.Property(p => p.Nombres).HasColumnType("VARCHAR(60)").IsRequired();
                builder.Property(p => p.FechaNacimiento).HasColumnType("DATE").IsRequired();
            });
        }

        public DbSet<PersonaPersistencia> Personas { get; set; }

        public void AgregarPersona(SolcitudCreacionPersona solicitudCreacion)
        {
            var personaAAgregar = Persona.CrearDesdeSolicitud(solicitudCreacion);
            if (ExistePersona(personaAAgregar))
                throw new ConstraintException(DirectorioDePersonasBase.ErrorNoSePuedeRegistrarPorqueYaExisteUnaPersonaRegistradaConEsaIdentificacion);

            Personas.Add(personaAAgregar.ParaPersistir());
            SaveChanges();
        }

        public List<string> ObtenerTodasLasPersonas()
        {
            var personas = ObtenerPersonasDesdePersistencia();

            return personas.OrderBy(d => d.NombreCompleto).Select(p => p.ToString()).ToList();
        }

        private List<Persona> ObtenerPersonasDesdePersistencia()
        {
            return Personas.ToList().Select(Persona.CrearDesdePersistencia).ToList();
        }

        public void EliminarPersona(Identificacion identificacion)
        {
            var personaAEliminar = ObtenerPersonaPersistencia(identificacion);

            Personas.Remove(personaAEliminar);
            SaveChanges();
        }

        private PersonaPersistencia ObtenerPersonaPersistencia(Identificacion identificacion)
        {
            var personaObtenida =  Personas.FirstOrDefault(d => d.NumeroIdentificacion.Equals(identificacion.Numero));
            if (personaObtenida == null)
                throw new ConstraintException(string.Format(DirectorioDePersonasBase.ErrorLaPersonaIdentificadaConNoEstaRegistradaEnElDirectorio, identificacion));

            return personaObtenida;
        }

        public Persona ObtenerPersona(Identificacion identificacion)
        {
            var personaObtenida =  Personas.FirstOrDefault(d => d.NumeroIdentificacion.Equals(identificacion.Numero));
            if (personaObtenida == null)
                throw new ConstraintException(string.Format(DirectorioDePersonasBase.ErrorLaPersonaIdentificadaConNoEstaRegistradaEnElDirectorio, identificacion));

            return personaObtenida.APersona();
        }

        public int CantidadDePersonas()
        {
            return Personas.Count();
        }

        private bool ExistePersona(Persona persona)
        {
            var personas = ObtenerPersonasDesdePersistencia();

            return personas.Any(d => d.Equals(persona));
        }

    }
}