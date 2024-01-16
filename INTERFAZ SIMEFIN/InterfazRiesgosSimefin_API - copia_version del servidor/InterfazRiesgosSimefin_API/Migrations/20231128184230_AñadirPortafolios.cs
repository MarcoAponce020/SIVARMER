using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfazRiesgosSimefin_API.Migrations
{
    public partial class AñadirPortafolios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 1,
                columns: new[] { "F_Posicion", "FechaCreacion", "FechaModificacion" },
                values: new object[] { "20230831", new DateTime(2023, 11, 28, 12, 42, 30, 669, DateTimeKind.Local).AddTicks(9683), new DateTime(2023, 11, 28, 12, 42, 30, 669, DateTimeKind.Local).AddTicks(9689) });

            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 2,
                columns: new[] { "F_Posicion", "FechaCreacion", "FechaModificacion" },
                values: new object[] { "20230831", new DateTime(2023, 11, 28, 12, 42, 30, 669, DateTimeKind.Local).AddTicks(9690), new DateTime(2023, 11, 28, 12, 42, 30, 669, DateTimeKind.Local).AddTicks(9690) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 1,
                columns: new[] { "F_Posicion", "FechaCreacion", "FechaModificacion" },
                values: new object[] { "20231127", new DateTime(2023, 11, 28, 12, 37, 57, 418, DateTimeKind.Local).AddTicks(8907), new DateTime(2023, 11, 28, 12, 37, 57, 418, DateTimeKind.Local).AddTicks(8914) });

            migrationBuilder.UpdateData(
                table: "Portafolios",
                keyColumn: "IdPortafolio",
                keyValue: 2,
                columns: new[] { "F_Posicion", "FechaCreacion", "FechaModificacion" },
                values: new object[] { "20231127", new DateTime(2023, 11, 28, 12, 37, 57, 418, DateTimeKind.Local).AddTicks(8915), new DateTime(2023, 11, 28, 12, 37, 57, 418, DateTimeKind.Local).AddTicks(8915) });
        }
    }
}
