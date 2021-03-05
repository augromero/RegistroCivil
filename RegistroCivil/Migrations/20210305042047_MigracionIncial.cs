using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroCivil.Migrations
{
    public partial class MigracionIncial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    NumeroIdentificacion = table.Column<string>(type: "VARCHAR(16)", nullable: false),
                    Tipo = table.Column<string>(type: "VARCHAR(4)", nullable: false),
                    Nombres = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    Apellidos = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.NumeroIdentificacion);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}
