using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lumina_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Base : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    SessionToken = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    DNI = table.Column<string>(type: "text", nullable: true),
                    ProfileImage = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AccountNumber = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Action = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Securities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SecurityQuestion = table.Column<string>(type: "text", nullable: false),
                    SecurityAnswer = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Securities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Securities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountNumber = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionDescription = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DNI", "DateAdded", "DateOfBirth", "DateUpdated", "Email", "FullName", "Password", "ProfileImage", "SessionToken", "Status", "UserName" },
                values: new object[,]
                {
                    { -9, null, null, new DateTime(2024, 4, 16, 9, 31, 49, 100, DateTimeKind.Utc).AddTicks(245), null, new DateTime(2024, 4, 16, 9, 31, 49, 100, DateTimeKind.Utc).AddTicks(250), "mabel8750_@example.com", "Mabel Ceballos", "$2a$11$0.9MoZTHNFENbwkQSvUKEu8pYhiEjlskgOwRyX1p0icPGB/qA6ZsO", null, null, 0, "mabel8750_" },
                    { -8, null, null, new DateTime(2024, 4, 16, 9, 31, 48, 981, DateTimeKind.Utc).AddTicks(4724), null, new DateTime(2024, 4, 16, 9, 31, 48, 981, DateTimeKind.Utc).AddTicks(4728), "karla6524@example.com", "Karla Chavez", "$2a$11$m9Xwd4.i75APLKWj7eYfh.4nT2noIQ.v5RUH62eyrjoT/qFa81QZa", null, null, 0, "karla6524" },
                    { -7, null, null, new DateTime(2024, 4, 16, 9, 31, 48, 861, DateTimeKind.Utc).AddTicks(9994), null, new DateTime(2024, 4, 16, 9, 31, 48, 862, DateTimeKind.Utc).AddTicks(31), "giolucc@example.com", "Giovanni Lucchetta", "$2a$11$ZXH5ERbRJ8g9EuzePTc5u../9it.Uk2Dymm3HdtZKujnL3Em.SNTK", null, null, 0, "giolucc" },
                    { -6, null, null, new DateTime(2024, 4, 16, 9, 31, 48, 741, DateTimeKind.Utc).AddTicks(3982), null, new DateTime(2024, 4, 16, 9, 31, 48, 741, DateTimeKind.Utc).AddTicks(3988), "facu597@example.com", "Facundo Castro", "$2a$11$SyZnLRF2HIwXX7gbzfChbes3w3M4id5bL1AQUf.TFHTEOacPuWMnW", null, null, 0, "facu597" },
                    { -5, null, null, new DateTime(2024, 4, 16, 9, 31, 48, 622, DateTimeKind.Utc).AddTicks(6799), null, new DateTime(2024, 4, 16, 9, 31, 48, 622, DateTimeKind.Utc).AddTicks(6804), "ezealeguzman@example.com", "Ezequiel Guzman", "$2a$11$V4bLjiCzzhpShjgWMBfji.a87BdvrSXSWvxNnTdGRZeVNn0iR187a", null, null, 0, "ezealeguzman" },
                    { -4, null, null, new DateTime(2024, 4, 16, 9, 31, 48, 501, DateTimeKind.Utc).AddTicks(6184), null, new DateTime(2024, 4, 16, 9, 31, 48, 501, DateTimeKind.Utc).AddTicks(6188), "ema_ramirez@example.com", "Emanuel Ramirez", "$2a$11$fJpIxRGPBg004nFlJ6VeU.Z5GTzCrsbc7hlKponpEjoE9e9OQtAj2", null, null, 0, "ema_ramirez" },
                    { -3, null, null, new DateTime(2024, 4, 16, 9, 31, 48, 375, DateTimeKind.Utc).AddTicks(7530), null, new DateTime(2024, 4, 16, 9, 31, 48, 375, DateTimeKind.Utc).AddTicks(7534), "4rnol@example.com", "Arnol Flores", "$2a$11$T0hvx6cDyDj/97QHq7ZPheWKEy22VFA1EclxcMQqXICsehWUulPzy", null, null, 0, "4rnol" },
                    { -2, null, null, new DateTime(2024, 4, 16, 9, 31, 48, 251, DateTimeKind.Utc).AddTicks(1892), null, new DateTime(2024, 4, 16, 9, 31, 48, 251, DateTimeKind.Utc).AddTicks(1898), "alexanderfrt@example.com", "Alexander Flores", "$2a$11$WqvaMnKjbYlgxyiIeG0NM.jLblU4er.zgJ2Ju74tn2XZmxd4o0rCi", null, null, 0, "AlexanderFRT" },
                    { -1, null, null, new DateTime(2024, 4, 16, 9, 31, 48, 132, DateTimeKind.Utc).AddTicks(1033), null, new DateTime(2024, 4, 16, 9, 31, 48, 132, DateTimeKind.Utc).AddTicks(1037), "ajruiz2204@example.com", "Alejandro Ruíz", "$2a$11$IZ8Ml9S/F8.j4u5qgNfPJ.QXyglpTtUkmOVAfQipPWJ88rtcGFzOK", null, null, 0, "ajruiz2204" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserId",
                table: "Logs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Securities_UserId",
                table: "Securities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountNumber",
                table: "Transactions",
                column: "AccountNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Securities");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
