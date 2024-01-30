using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfazRiesgosSimefin_API.Migrations
{
    public partial class AgregarRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombres",
                table: "AspNetUsers",
                newName: "Nombre");

            migrationBuilder.CreateTable(
                name: "refreshTokens",
                columns: table => new
                {
                    idRefresh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tokenId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    refreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isValid = table.Column<bool>(type: "bit", nullable: false),
                    TiempoExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refreshTokens", x => x.idRefresh);
                });

            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2024, 1, 8, 12, 32, 53, 940, DateTimeKind.Local).AddTicks(2985), new DateTime(2024, 1, 8, 12, 32, 53, 940, DateTimeKind.Local).AddTicks(2993) });

            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2024, 1, 8, 12, 32, 53, 940, DateTimeKind.Local).AddTicks(2995), new DateTime(2024, 1, 8, 12, 32, 53, 940, DateTimeKind.Local).AddTicks(2995) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "refreshTokens");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "AspNetUsers",
                newName: "Nombres");

            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 12, 13, 17, 9, 17, 478, DateTimeKind.Local).AddTicks(7092), new DateTime(2023, 12, 13, 17, 9, 17, 478, DateTimeKind.Local).AddTicks(7102) });

            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 12, 13, 17, 9, 17, 478, DateTimeKind.Local).AddTicks(7103), new DateTime(2023, 12, 13, 17, 9, 17, 478, DateTimeKind.Local).AddTicks(7104) });
        }
    }
}
