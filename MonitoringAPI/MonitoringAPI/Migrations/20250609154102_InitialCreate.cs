using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MonitoringAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cameras",
                columns: new[] { "Id", "Latitude", "Longitude", "Naam", "Postcode" },
                values: new object[,]
                {
                    { 2, 51.922499999999999, 4.4790000000000001, "Camera Zuid", "3011AB" },
                    { 3, 51.923999999999999, 4.4699999999999998, "Camera Oost", "3011AC" },
                    { 4, 51.924999999999997, 4.468, "Camera West", "3011AD" }
                });

            migrationBuilder.InsertData(
                table: "Trash",
                columns: new[] { "Id", "CameraId", "Confidence", "DagCategorie", "DateCollected", "Temperatuur", "TypeAfval", "WeerOmschrijving", "WindRichting" },
                values: new object[,]
                {
                    { 9, 1, 0.81999999999999995, "Werkdag", new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.0, "Snoeiafval", "Regenachtig", "NO" },
                    { 13, 1, 0.85999999999999999, "Werkdag", new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 19.0, "Groente- en fruitafval", "Zonnig", "Z" },
                    { 17, 1, 0.88, "Werkdag", new DateTime(2025, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 17.0, "Grofvuil en hout", "Licht bewolkt", "NW" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "admin@example.com", "$2a$11$GpJBsgSFzsGoZTP3.VmLLuiKNzh335fAM7HRx7BLzawZbzVFuQI9e" });

            migrationBuilder.InsertData(
                table: "Trash",
                columns: new[] { "Id", "CameraId", "Confidence", "DagCategorie", "DateCollected", "Temperatuur", "TypeAfval", "WeerOmschrijving", "WindRichting" },
                values: new object[,]
                {
                    { 3, 2, 0.90000000000000002, "Werkdag", new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.0, "Papier", "Zonnig", "Z" },
                    { 4, 2, 0.88, "Weekend", new DateTime(2025, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.5, "Glas", "Bewolkt", "O" },
                    { 5, 3, 0.80000000000000004, "Werkdag", new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 16.0, "Grofvuil", "Regenachtig", "NW" },
                    { 6, 3, 0.94999999999999996, "Weekend", new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 21.0, "Textiel", "Zonnig", "Z" },
                    { 7, 4, 0.87, "Werkdag", new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 19.0, "Kruidenierswaren", "Licht bewolkt", "O" },
                    { 8, 4, 0.93000000000000005, "Weekend", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 22.0, "Hout", "Zonnig", "ZW" },
                    { 10, 2, 0.89000000000000001, "Weekend", new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.5, "Batterijen", "Licht bewolkt", "Z" },
                    { 11, 3, 0.91000000000000003, "Werkdag", new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 17.5, "Elektronica", "Zonnig", "O" },
                    { 12, 4, 0.83999999999999997, "Weekend", new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 16.5, "Kunststof", "Bewolkt", "NW" },
                    { 14, 2, 0.93999999999999995, "Weekend", new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 21.5, "Restafval", "Licht bewolkt", "NO" },
                    { 15, 3, 0.82999999999999996, "Werkdag", new DateTime(2025, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.0, "Papier en karton", "Regenachtig", "ZW" },
                    { 16, 4, 0.90000000000000002, "Weekend", new DateTime(2025, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.0, "Metaal en kunststof", "Zonnig", "O" },
                    { 18, 2, 0.92000000000000004, "Weekend", new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 19.5, "Textiel en kleding", "Zonnig", "Z" },
                    { 19, 3, 0.84999999999999998, "Werkdag", new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.5, "Snoeiafval en takken", "Regenachtig", "O" },
                    { 20, 4, 0.93000000000000005, "Weekend", new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.5, "Batterijen en accu's", "Licht bewolkt", "ZW" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Trash",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Cameras",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cameras",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cameras",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "test@example.com", "hashedpassword123" });
        }
    }
}
