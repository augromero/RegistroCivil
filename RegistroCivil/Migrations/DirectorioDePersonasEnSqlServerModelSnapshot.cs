﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistroCivil.Datos.EnMemoria;

namespace RegistroCivil.Migrations
{
    [DbContext(typeof(DirectorioDePersonasEnSqlServer))]
    partial class DirectorioDePersonasEnSqlServerModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RegistroCivil.Dominio.DTOs.PersonaPersistencia", b =>
                {
                    b.Property<string>("NumeroIdentificacion")
                        .HasColumnType("VARCHAR(16)");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("VARCHAR(60)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("DATE");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("VARCHAR(60)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("VARCHAR(4)");

                    b.HasKey("NumeroIdentificacion");

                    b.ToTable("Personas");
                });
#pragma warning restore 612, 618
        }
    }
}