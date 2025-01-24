using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketStationMVC.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_OwnerId",
                table: "Carts");

            migrationBuilder.DropTable(
                name: "CartItemVM");

            migrationBuilder.DropTable(
                name: "CartVM");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3899), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3902) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3906), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3907) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3910), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3911) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3913), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3915) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3917), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3919) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3921), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3922) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3924), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3926) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3928), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3929) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3931), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3933) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3935), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3936) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4029), new DateTime(2025, 2, 27, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4013), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4037), new DateTime(2025, 5, 8, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4034), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4038) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4043), new DateTime(2025, 5, 28, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4041), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4045) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4050), new DateTime(2025, 2, 17, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4047), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4051) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "DateOfEvent", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4056), new DateTime(2025, 11, 14, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4054), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4057) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 31, DateTimeKind.Local).AddTicks(2139), new DateTime(2025, 1, 23, 20, 45, 0, 31, DateTimeKind.Local).AddTicks(2207) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 31, DateTimeKind.Local).AddTicks(2211), new DateTime(2025, 1, 23, 20, 45, 0, 31, DateTimeKind.Local).AddTicks(2213) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt", "Password", "RegisteredOn" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(2958), new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(3028), "$2a$11$BpKxPpqmAzpwmOVXf9zLzOQJHIeoFkIy9zs9Qrc2Pqy1TTfDiOdzy", new DateTime(2025, 1, 23, 20, 45, 0, 31, DateTimeKind.Local).AddTicks(2380) });

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_OwnerId",
                table: "Carts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_OwnerId",
                table: "Carts");

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
                    AddedToCart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CartVMId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_OwnerId",
                table: "Carts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
