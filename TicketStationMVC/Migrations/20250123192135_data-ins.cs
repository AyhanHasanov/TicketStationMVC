using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketStationMVC.Migrations
{
    /// <inheritdoc />
    public partial class datains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2797), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2799) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2802), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2803) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2805), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2807) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2808), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2810) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2812), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2815) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2817), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2818) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2820), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2824) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2825), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2827) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2828), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2832) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2834), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2835) });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { 11, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2837), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2839), "Gaming tournament" },
                    { 12, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2842), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2843), "Fashion show" },
                    { 13, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2845), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2846), "Bussines networking" },
                    { 14, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2848), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2849), "Health and wellness" },
                    { 15, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2851), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2853), "Comedy" }
                });

            migrationBuilder.InsertData(
                table: "EventCategories",
                columns: new[] { "CategoryId", "EventId" },
                values: new object[,]
                {
                    { 5, 1 },
                    { 10, 2 },
                    { 9, 4 }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DateOfEvent", "ImageURL", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2927), new DateTime(2025, 2, 27, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2917), "/image/rock-music.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2928) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DateOfEvent", "Description", "ImageURL", "ModifiedAt", "Name", "Price", "Quantity" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2933), new DateTime(2025, 3, 14, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2931), "An elegant evening of dinner, live entertainment, and fundraising for a noble cause. Join us to make a difference in the lives of those in need.", "/image/charity", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2934), "Charity Gala: \"Hope for Tomorrow\"", 100m, 300 });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DateOfEvent", "Description", "ImageURL", "ModifiedAt", "Name", "Price", "Quantity" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2938), new DateTime(2025, 2, 7, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2936), "Explore the great outdoors with an exhilarating mountain hike. Perfect for nature enthusiasts and adventure seekers.", "/image/outdoor-adventure", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2940), "Outdoor Adventure: \"Mountain Hike Experience\"", 20m, 150 });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "DateOfEvent", "Description", "ImageURL", "ModifiedAt", "Name", "Price", "Quantity" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2944), new DateTime(2025, 3, 24, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2942), "Join industry leaders and tech enthusiasts to discuss the latest trends in technology and innovation.", "/image/tech-conference.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2945), "Tech Conference: \"Innovators Unite 2025\"", 75m, 200 });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "DateOfEvent", "Description", "ImageURL", "ModifiedAt", "Name", "Price", "Quantity" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2949), new DateTime(2025, 2, 17, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2947), "A culinary journey featuring dishes from around the world. Come hungry and explore a variety of international cuisines.", "/image/food-festival.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2950), "Food Festival: \"Taste the World\"", 40m, 500 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "DateOfEvent", "Description", "ImageURL", "ModifiedAt", "ModifiedById", "Name", "Price", "Quantity", "Status" },
                values: new object[,]
                {
                    { 6, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2954), 1, new DateTime(2025, 2, 2, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2952), "An evening of side-splitting comedy featuring top stand-up comedians. Prepare to laugh until it hurts!", "/image/comedy-night.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2956), 1, "Comedy Night: \"Laugh Out Loud\"", 50m, 100, true },
                    { 7, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2964), 1, new DateTime(2025, 3, 4, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2958), "Discover stunning works of art by contemporary artists. A must-visit for art lovers and collectors.", "/image/art-exhibition.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2966), 1, "Art Exhibition: \"Modern Visions\"", 25m, 200, true },
                    { 8, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2971), 1, new DateTime(2025, 4, 3, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2969), "Compete with the best gamers in a thrilling tournament. Cash prizes and bragging rights await the victors!", "/image/gaming-tournament.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2973), 1, "Gaming Tournament: \"Battle Royale Championship\"", 30m, 300, true },
                    { 9, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2977), 1, new DateTime(2025, 2, 12, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2975), "Learn to dance like a pro in this fun and engaging salsa workshop. No experience needed!", "/image/dancing.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2978), 1, "Dance Workshop: \"Salsa Night\"", 35m, 50, true },
                    { 10, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2982), 1, new DateTime(2025, 5, 3, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2980), "A fun-filled day for the whole family at the adventure park. Activities for kids and adults alike.", "/image/family.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2983), 1, "Family Fun Day: \"Adventure Park\"", 60m, 600, true },
                    { 11, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2988), 1, new DateTime(2025, 4, 18, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2986), "Join bestselling authors for an intimate evening of book readings and discussions.", "/image/book-reading.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2989), 1, "Book Reading: \"Authors' Evening\"", 15m, 80, true },
                    { 12, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2993), 1, new DateTime(2025, 2, 22, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2991), "Connect with industry leaders and like-minded entrepreneurs at this exclusive networking event.", "/image/bussines.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2994), 1, "Business Networking: \"Entrepreneur Meet 2025\"", 50m, 150, true },
                    { 13, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2998), 1, new DateTime(2025, 3, 9, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2996), "Recharge your mind and body with a relaxing yoga retreat in a serene location.", "yoga.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2999), 1, "Health & Wellness: \"Yoga Retreat\"", 100m, 40, true },
                    { 14, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(3004), 1, new DateTime(2025, 4, 28, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(3002), "Experience the richness of culture and traditions at the annual heritage festival.", "/image/cultural-event.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(3005), 1, "Cultural Festival: \"Heritage Celebrations\"", 20m, 400, true },
                    { 15, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(3010), 1, new DateTime(2025, 5, 13, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(3008), "Watch the premiere of a highly anticipated indie movie, followed by a Q&A with the director.", "/image/film-screening.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(3012), 1, "Film Screening: \"Indie Movie Premiere\"", 25m, 200, true },
                    { 16, new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(3017), 1, new DateTime(2025, 6, 2, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(3015), "A glamorous evening featuring the latest collections from top fashion designers.", "/image/fashion.jpg", new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(3018), 1, "Fashion Show: \"Style Icons 2025\"", 75m, 150, true }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 285, DateTimeKind.Local).AddTicks(2749), new DateTime(2025, 1, 23, 21, 21, 35, 285, DateTimeKind.Local).AddTicks(2809) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 285, DateTimeKind.Local).AddTicks(2812), new DateTime(2025, 1, 23, 21, 21, 35, 285, DateTimeKind.Local).AddTicks(2813) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt", "Password", "RegisteredOn" },
                values: new object[] { new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2156), new DateTime(2025, 1, 23, 21, 21, 35, 401, DateTimeKind.Local).AddTicks(2225), "$2a$11$/G.3Q2h5UcerJaZznNQgseuQu1wQM75say3LtexLg.EYwkI63B1/u", new DateTime(2025, 1, 23, 21, 21, 35, 285, DateTimeKind.Local).AddTicks(2965) });

            migrationBuilder.InsertData(
                table: "EventCategories",
                columns: new[] { "CategoryId", "EventId" },
                values: new object[,]
                {
                    { 13, 4 },
                    { 5, 6 },
                    { 11, 6 },
                    { 9, 7 },
                    { 14, 7 },
                    { 4, 8 },
                    { 12, 8 },
                    { 8, 9 },
                    { 15, 9 },
                    { 4, 10 },
                    { 6, 10 },
                    { 1, 11 },
                    { 5, 11 },
                    { 7, 12 },
                    { 8, 12 },
                    { 6, 13 },
                    { 13, 13 },
                    { 5, 14 },
                    { 10, 14 },
                    { 5, 15 },
                    { 8, 15 },
                    { 2, 16 },
                    { 10, 16 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 9, 4 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 13, 4 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 11, 6 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 9, 7 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 14, 7 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 12, 8 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 8, 9 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 15, 9 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 4, 10 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 6, 10 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 5, 11 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 7, 12 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 8, 12 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 6, 13 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 13, 13 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 5, 14 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 10, 14 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 5, 15 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 8, 15 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 2, 16 });

            migrationBuilder.DeleteData(
                table: "EventCategories",
                keyColumns: new[] { "CategoryId", "EventId" },
                keyValues: new object[] { 10, 16 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 16);

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

            migrationBuilder.InsertData(
                table: "EventCategories",
                columns: new[] { "CategoryId", "EventId" },
                values: new object[] { 7, 4 });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DateOfEvent", "ImageURL", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4029), new DateTime(2025, 2, 27, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4013), "/image/rock.jpg", new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DateOfEvent", "Description", "ImageURL", "ModifiedAt", "Name", "Price", "Quantity" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4037), new DateTime(2025, 5, 8, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4034), "Witness the most thrilling basketball match of the season as the two best teams in the city battle it out for the championship title. Come support your local team and experience the adrenaline rush of the final game!", "/image/football.jpg", new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4038), "Basketball Game: \"City Championship Finals\"", 120m, 400 });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DateOfEvent", "Description", "ImageURL", "ModifiedAt", "Name", "Price", "Quantity" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4043), new DateTime(2025, 5, 28, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4041), "An outdoor performance of one of Shakespeare's greatest plays, \"A Midsummer Night's Dream.\" Enjoy a magical evening under the stars, with actors performing in a beautiful outdoor setting surrounded by nature.", "/image/william.jpeg", new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4045), "Theater Play: \"Shakespeare Under the Stars\"", 90m, 50 });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "DateOfEvent", "Description", "ImageURL", "ModifiedAt", "Name", "Price", "Quantity" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4050), new DateTime(2025, 2, 17, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4047), "Join industry leaders, innovators, and tech enthusiasts at the Future of AI Summit. This conference covers the latest advancements in artificial intelligence, machine learning, and their impact on industries like healthcare, finance, and entertainment.", "/image/ai.jpg", new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4051), "Tech Conference: \"Future of AI Summit\"", 20m, 70 });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "DateOfEvent", "Description", "ImageURL", "ModifiedAt", "Name", "Price", "Quantity" },
                values: new object[] { new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4056), new DateTime(2025, 11, 14, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4054), "A day of family-friendly activities, games, and entertainment at the Magic Kingdom. Kids can enjoy carnival rides, face painting, storytelling sessions, and more. Perfect for families looking to spend quality time together.", "/image/disney.jpg", new DateTime(2025, 1, 23, 20, 45, 0, 369, DateTimeKind.Local).AddTicks(4057), "Family Fun Day: \"Magic Kingdom Adventure\"", 200m, 700 });

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
        }
    }
}
