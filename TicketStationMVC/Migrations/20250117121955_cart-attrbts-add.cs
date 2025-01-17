using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketStationMVC.Migrations
{
    /// <inheritdoc />
    public partial class cartattrbtsadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedToCart",
                table: "CartItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CartVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartVM_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItemVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AddedToCart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CartVMId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItemVM_CartVM_CartVMId",
                        column: x => x.CartVMId,
                        principalTable: "CartVM",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7386), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7388) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7392), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7394) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7397), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7399) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7402), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7404) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7431), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7433) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7438), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7443), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7465) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7468), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7470) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7472), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7474) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7477), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7479) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7562), new DateTime(2025, 2, 21, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7543), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7564) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7572), new DateTime(2025, 5, 2, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7569), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7574) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7586), new DateTime(2025, 5, 22, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7577), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7633) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7639), new DateTime(2025, 2, 11, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7636), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7641) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7646), new DateTime(2025, 11, 8, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7644), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(7648) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 613, DateTimeKind.Local).AddTicks(4858), new DateTime(2025, 1, 17, 14, 19, 54, 613, DateTimeKind.Local).AddTicks(4925) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 613, DateTimeKind.Local).AddTicks(4928), new DateTime(2025, 1, 17, 14, 19, 54, 613, DateTimeKind.Local).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt", "Password", "RegisteredOn" },
                values: new object[] { new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(6918), new DateTime(2025, 1, 17, 14, 19, 54, 735, DateTimeKind.Local).AddTicks(6998), "$2a$11$HLca8QudF2TGKgvi/JdiwuPO6PsVqJ57.q5X4oGWdqIHnh0ehzYUS", new DateTime(2025, 1, 17, 14, 19, 54, 613, DateTimeKind.Local).AddTicks(5054) });

            migrationBuilder.CreateIndex(
                name: "IX_CartItemVM_CartVMId",
                table: "CartItemVM",
                column: "CartVMId");

            migrationBuilder.CreateIndex(
                name: "IX_CartVM_OwnerId",
                table: "CartVM",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemVM");

            migrationBuilder.DropTable(
                name: "CartVM");

            migrationBuilder.DropColumn(
                name: "AddedToCart",
                table: "CartItems");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2652), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2655) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2658), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2664), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2666) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2668), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2681), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2683) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2685), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2687) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2689), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2705) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2707), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2709) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2711), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2713) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2715), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2717) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2790), new DateTime(2025, 2, 19, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2772), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2792) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2798), new DateTime(2025, 4, 30, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2795), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2800) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2810), new DateTime(2025, 5, 20, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2803), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2833) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2839), new DateTime(2025, 2, 9, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2836), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2841) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2846), new DateTime(2025, 11, 6, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2843), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2848) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 543, DateTimeKind.Local).AddTicks(2832), new DateTime(2025, 1, 15, 21, 32, 20, 543, DateTimeKind.Local).AddTicks(2898) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 543, DateTimeKind.Local).AddTicks(2901), new DateTime(2025, 1, 15, 21, 32, 20, 543, DateTimeKind.Local).AddTicks(2902) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt", "Password", "RegisteredOn" },
                values: new object[] { new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2055), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2133), "$2a$11$SUPakUCNybEAv/4HsuwwG.tYszPhN.6iSZ8Sj4X3zhZNy6gCqf4Q2", new DateTime(2025, 1, 15, 21, 32, 20, 543, DateTimeKind.Local).AddTicks(3015) });
        }
    }
}
