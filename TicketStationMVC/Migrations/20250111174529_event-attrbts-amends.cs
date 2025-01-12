using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketStationMVC.Migrations
{
    /// <inheritdoc />
    public partial class eventattrbtsamends : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 11, 19, 45, 27, 333, DateTimeKind.Local).AddTicks(7462), new DateTime(2025, 1, 11, 19, 45, 27, 333, DateTimeKind.Local).AddTicks(7594) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 11, 19, 45, 27, 333, DateTimeKind.Local).AddTicks(7597), new DateTime(2025, 1, 11, 19, 45, 27, 333, DateTimeKind.Local).AddTicks(7599) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt", "Password", "RegisteredOn" },
                values: new object[] { new DateTime(2025, 1, 11, 19, 45, 27, 800, DateTimeKind.Local).AddTicks(6654), new DateTime(2025, 1, 11, 19, 45, 27, 800, DateTimeKind.Local).AddTicks(6787), "$2a$11$gds2a.7Ul5KS9CpYeEWhSuhWZWAAermNEDUqFb7Tlr96m5RA5R/sC", new DateTime(2025, 1, 11, 19, 45, 27, 333, DateTimeKind.Local).AddTicks(7823) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 6, 17, 2, 31, 158, DateTimeKind.Local).AddTicks(8876), new DateTime(2025, 1, 6, 17, 2, 31, 158, DateTimeKind.Local).AddTicks(8945) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 6, 17, 2, 31, 158, DateTimeKind.Local).AddTicks(8948), new DateTime(2025, 1, 6, 17, 2, 31, 158, DateTimeKind.Local).AddTicks(8949) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt", "Password", "RegisteredOn" },
                values: new object[] { new DateTime(2025, 1, 6, 17, 2, 31, 280, DateTimeKind.Local).AddTicks(9187), new DateTime(2025, 1, 6, 17, 2, 31, 280, DateTimeKind.Local).AddTicks(9292), "$2a$11$O5TADILl/.eE19eIIG/k5.OAabN4z3BlP2YhOTTQmhNRw1WuvYLbu", new DateTime(2025, 1, 6, 17, 2, 31, 158, DateTimeKind.Local).AddTicks(9091) });
        }
    }
}
