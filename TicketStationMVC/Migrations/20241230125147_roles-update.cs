using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketStationMVC.Migrations
{
    /// <inheritdoc />
    public partial class rolesupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "ordinaryuser" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "RegisteredOn" },
                values: new object[] { "$2a$11$Pmz5VbruYIhTcYM6ScT/qun3z8GjhFcNkqmGdJ6685.uyIpvl7tcW", new DateTime(2024, 12, 30, 14, 51, 46, 870, DateTimeKind.Local).AddTicks(9428) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "RegisteredOn" },
                values: new object[] { "$2a$11$tq1Ip.w50MCHYwfr50Ft5.HSq78Qc50P4UlFVUD530/LZQMRVFCCW", new DateTime(2024, 12, 29, 15, 57, 48, 725, DateTimeKind.Local).AddTicks(9973) });
        }
    }
}
