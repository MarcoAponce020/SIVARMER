using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfazRiesgosSimefin_API.Migrations
{
    public partial class CrearDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portafolios",
                columns: table => new
                {
                    IdPortafolio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    F_Posicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombrePortafolio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubPortafolio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    listaDatos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    No_Envio = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portafolios", x => x.IdPortafolio);
                });

            migrationBuilder.InsertData(
                table: "Portafolios",
                columns: new[] { "IdPortafolio", "F_Posicion", "FechaCreacion", "FechaModificacion", "No_Envio", "NombrePortafolio", "SubPortafolio", "listaDatos" },
                values: new object[] { 1, "20231127", new DateTime(2023, 11, 28, 12, 31, 41, 491, DateTimeKind.Local).AddTicks(3215), new DateTime(2023, 11, 28, 12, 31, 41, 491, DateTimeKind.Local).AddTicks(3222), 1, "TOTAL", "DERIVADOS DE NEGOCIACION", "3208895.824" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Portafolios");
        }
    }
}
