using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfazRiesgosSimefin_API.Migrations
{
    public partial class UsuarioMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 11, 30, 13, 46, 34, 333, DateTimeKind.Local).AddTicks(3850), new DateTime(2023, 11, 30, 13, 46, 34, 333, DateTimeKind.Local).AddTicks(3857) });

            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 11, 30, 13, 46, 34, 333, DateTimeKind.Local).AddTicks(3858), new DateTime(2023, 11, 30, 13, 46, 34, 333, DateTimeKind.Local).AddTicks(3858) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 11, 28, 12, 42, 30, 669, DateTimeKind.Local).AddTicks(9683), new DateTime(2023, 11, 28, 12, 42, 30, 669, DateTimeKind.Local).AddTicks(9689) });

            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 11, 28, 12, 42, 30, 669, DateTimeKind.Local).AddTicks(9690), new DateTime(2023, 11, 28, 12, 42, 30, 669, DateTimeKind.Local).AddTicks(9690) });
        }
    }
}
