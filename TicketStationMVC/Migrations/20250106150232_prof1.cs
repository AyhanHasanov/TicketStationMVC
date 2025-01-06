using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketStationMVC.Migrations
{
    /// <inheritdoc />
    public partial class prof1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventCreateVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCreateVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventDetailedVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetailedVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventViewVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventViewVM", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventCreateVM");

            migrationBuilder.DropTable(
                name: "EventDetailedVM");

            migrationBuilder.DropTable(
                name: "EventViewVM");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 12, 30, 15, 47, 9, 738, DateTimeKind.Local).AddTicks(4933), new DateTime(2024, 12, 30, 15, 47, 9, 738, DateTimeKind.Local).AddTicks(5003) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 12, 30, 15, 47, 9, 738, DateTimeKind.Local).AddTicks(5007), new DateTime(2024, 12, 30, 15, 47, 9, 738, DateTimeKind.Local).AddTicks(5008) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt", "Password", "RegisteredOn" },
                values: new object[] { new DateTime(2024, 12, 30, 15, 47, 9, 973, DateTimeKind.Local).AddTicks(9407), new DateTime(2024, 12, 30, 15, 47, 9, 973, DateTimeKind.Local).AddTicks(9492), "$2a$11$XmM0gYuzxjFhDWebO9DSh.yWeIKptrk6hVpTMueLvkEPIRldHJsuK", new DateTime(2024, 12, 30, 15, 47, 9, 738, DateTimeKind.Local).AddTicks(5158) });
        }
    }
}
