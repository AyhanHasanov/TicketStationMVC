using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketStationMVC.Migrations
{
    /// <inheritdoc />
    public partial class rolesupdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "RegisteredOn" },
                values: new object[] { "$2a$11$DcBu.KaSBRLBS1OpOFztYeyszh3BxPSLmNnhmN3eWElN7q3E/EXba", new DateTime(2024, 12, 30, 15, 43, 50, 191, DateTimeKind.Local).AddTicks(2184) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "RegisteredOn" },
                values: new object[] { "$2a$11$Pmz5VbruYIhTcYM6ScT/qun3z8GjhFcNkqmGdJ6685.uyIpvl7tcW", new DateTime(2024, 12, 30, 14, 51, 46, 870, DateTimeKind.Local).AddTicks(9428) });
        }
    }
}
