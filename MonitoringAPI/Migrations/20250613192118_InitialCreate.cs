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
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cameras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trash",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCollected = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DagCategorie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeAfval = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WindRichting = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Temperatuur = table.Column<double>(type: "float", nullable: false),
                    WeerOmschrijving = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Confidence = table.Column<double>(type: "float", nullable: false),
                    CameraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trash", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trash_Cameras_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Cameras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cameras",
                columns: new[] { "Id", "Latitude", "Longitude", "Naam", "Postcode" },
                values: new object[,]
                {
                    { 1, 51.923900000000003, 4.4699, "Camera Noord", "3011AA" },
                    { 2, 51.922499999999999, 4.4790000000000001, "Camera Zuid", "3011AB" },
                    { 3, 51.923999999999999, 4.4699999999999998, "Camera Oost", "3011AC" },
                    { 4, 51.924999999999997, 4.468, "Camera West", "3011AD" }
                });

            migrationBuilder.InsertData(
                table: "Trash",
                columns: new[] { "Id", "CameraId", "Confidence", "DagCategorie", "DateCollected", "Temperatuur", "TypeAfval", "WeerOmschrijving", "WindRichting" },
                values: new object[,]
                {
                    { 1, 1, 0.84999999999999998, "Werkdag", new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 17.199999999999999, "Plastic", "Licht bewolkt", "NO" },
                    { 2, 1, 0.92000000000000004, "Weekend", new DateTime(2025, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 19.5, "Metaal", "Zonnig", "ZW" },
                    { 3, 2, 0.90000000000000002, "Werkdag", new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.0, "Papier", "Zonnig", "Z" },
                    { 4, 2, 0.88, "Weekend", new DateTime(2025, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.5, "Glas", "Bewolkt", "O" },
                    { 5, 3, 0.80000000000000004, "Werkdag", new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 16.0, "Grofvuil", "Regenachtig", "NW" },
                    { 6, 3, 0.94999999999999996, "Weekend", new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 21.0, "Textiel", "Zonnig", "Z" },
                    { 7, 4, 0.87, "Werkdag", new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 19.0, "Kruidenierswaren", "Licht bewolkt", "O" },
                    { 8, 4, 0.93000000000000005, "Weekend", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 22.0, "Hout", "Zonnig", "ZW" },
                    { 9, 1, 0.81999999999999995, "Werkdag", new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.0, "Snoeiafval", "Regenachtig", "NO" },
                    { 10, 2, 0.89000000000000001, "Weekend", new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.5, "Batterijen", "Licht bewolkt", "Z" },
                    { 11, 3, 0.91000000000000003, "Werkdag", new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 17.5, "Elektronica", "Zonnig", "O" },
                    { 12, 4, 0.83999999999999997, "Weekend", new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 16.5, "Kunststof", "Bewolkt", "NW" },
                    { 13, 1, 0.85999999999999999, "Werkdag", new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 19.0, "Groente- en fruitafval", "Zonnig", "Z" },
                    { 14, 2, 0.93999999999999995, "Weekend", new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 21.5, "Restafval", "Licht bewolkt", "NO" },
                    { 15, 3, 0.82999999999999996, "Werkdag", new DateTime(2025, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.0, "Papier en karton", "Regenachtig", "ZW" },
                    { 16, 4, 0.90000000000000002, "Weekend", new DateTime(2025, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.0, "Metaal en kunststof", "Zonnig", "O" },
                    { 17, 1, 0.88, "Werkdag", new DateTime(2025, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 17.0, "Grofvuil en hout", "Licht bewolkt", "NW" },
                    { 18, 2, 0.92000000000000004, "Weekend", new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 19.5, "Textiel en kleding", "Zonnig", "Z" },
                    { 19, 3, 0.84999999999999998, "Werkdag", new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.5, "Snoeiafval en takken", "Regenachtig", "O" },
                    { 20, 4, 0.93000000000000005, "Weekend", new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.5, "Batterijen en accu's", "Licht bewolkt", "ZW" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Trash_CameraId",
                table: "Trash",
                column: "CameraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Trash");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cameras");
        }
    }
}
