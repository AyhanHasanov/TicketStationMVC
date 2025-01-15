using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketStationMVC.Migrations
{
    /// <inheritdoc />
    public partial class datainsert1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventCreateVM");

            migrationBuilder.DropTable(
                name: "EventDetailedVM");

            migrationBuilder.DropTable(
                name: "EventViewVM");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "CartItems");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2652), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2655), "Music Concert" },
                    { 2, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2658), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2660), "Sport" },
                    { 3, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2664), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2666), "Theater" },
                    { 4, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2668), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2670), "Art" },
                    { 5, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2681), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2683), "Festival" },
                    { 6, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2685), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2687), "Workshop" },
                    { 7, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2689), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2705), "Charity" },
                    { 8, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2707), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2709), "Food & Drinks" },
                    { 9, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2711), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2713), "Trade show" },
                    { 10, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2715), new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2717), "Family & kids" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "DateOfEvent", "Description", "ImageURL", "ModifiedAt", "ModifiedById", "Name", "Price", "Quantity", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2790), 1, new DateTime(2025, 2, 19, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2772), "A high-energy music festival featuring top rock bands from around the world. Enjoy live performances, food stalls, and exciting activities for all ages. Come for the music, stay for the unforgettable atmosphere!", "/image/rock.jpg", new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2792), 1, "Music Concert: \"Summer Rock Fest\"", 60m, 250, true },
                    { 2, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2798), 1, new DateTime(2025, 4, 30, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2795), "Witness the most thrilling basketball match of the season as the two best teams in the city battle it out for the championship title. Come support your local team and experience the adrenaline rush of the final game!", "/image/football.jpg", new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2800), 1, "Basketball Game: \"City Championship Finals\"", 120m, 400, true },
                    { 3, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2810), 1, new DateTime(2025, 5, 20, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2803), "An outdoor performance of one of Shakespeare's greatest plays, \"A Midsummer Night's Dream.\" Enjoy a magical evening under the stars, with actors performing in a beautiful outdoor setting surrounded by nature.", "/image/william.jpeg", new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2833), 1, "Theater Play: \"Shakespeare Under the Stars\"", 90m, 50, true },
                    { 4, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2839), 1, new DateTime(2025, 2, 9, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2836), "Join industry leaders, innovators, and tech enthusiasts at the Future of AI Summit. This conference covers the latest advancements in artificial intelligence, machine learning, and their impact on industries like healthcare, finance, and entertainment.", "/image/ai.jpg", new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2841), 1, "Tech Conference: \"Future of AI Summit\"", 20m, 70, true },
                    { 5, new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2846), 1, new DateTime(2025, 11, 6, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2843), "A day of family-friendly activities, games, and entertainment at the Magic Kingdom. Kids can enjoy carnival rides, face painting, storytelling sessions, and more. Perfect for families looking to spend quality time together.", "/image/disney.jpg", new DateTime(2025, 1, 15, 21, 32, 20, 810, DateTimeKind.Local).AddTicks(2848), 1, "Family Fun Day: \"Magic Kingdom Adventure\"", 200m, 700, true }
                });

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

            migrationBuilder.InsertData(
                table: "EventCategories",
                columns: new[] { "CategoryId", "EventId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 3 },
                    { 6, 4 },
                    { 7, 4 },
                    { 8, 5 },
                    { 10, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 10, 5 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CartItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "CartItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "EventCreateVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
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
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
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
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
    }
}
