using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfazRiesgosSimefin_API.Migrations
{
    public partial class CreateSERVER : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 12, 7, 13, 19, 40, 56, DateTimeKind.Local).AddTicks(9961), new DateTime(2023, 12, 7, 13, 19, 40, 56, DateTimeKind.Local).AddTicks(9970) });

            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 12, 7, 13, 19, 40, 56, DateTimeKind.Local).AddTicks(9972), new DateTime(2023, 12, 7, 13, 19, 40, 56, DateTimeKind.Local).AddTicks(9972) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 12, 7, 12, 24, 37, 646, DateTimeKind.Local).AddTicks(7676), new DateTime(2023, 12, 7, 12, 24, 37, 646, DateTimeKind.Local).AddTicks(7687) });

            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 12, 7, 12, 24, 37, 646, DateTimeKind.Local).AddTicks(7689), new DateTime(2023, 12, 7, 12, 24, 37, 646, DateTimeKind.Local).AddTicks(7690) });
        }
    }
}
