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
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    SessionToken = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "Date", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    DNI = table.Column<string>(type: "text", nullable: true),
                    ProfileImage = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "varchar(24)", nullable: false),
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
                    Type = table.Column<string>(type: "varchar(24)", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "varchar(24)", nullable: false),
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
                    Action = table.Column<string>(type: "varchar(24)", nullable: false),
                    Status = table.Column<string>(type: "varchar(24)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    Status = table.Column<string>(type: "varchar(24)", nullable: false),
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
                    Type = table.Column<string>(type: "varchar(24)", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionDescription = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "varchar(24)", nullable: false),
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
                    { -9, null, null, new DateTime(2024, 4, 21, 23, 34, 50, 521, DateTimeKind.Utc).AddTicks(196), null, new DateTime(2024, 4, 21, 23, 34, 50, 521, DateTimeKind.Utc).AddTicks(199), "mabel8750_@example.com", "Mabel Ceballos", "$2a$11$tPVtFLamiFjGDIMSR9dvBedVUbuElSw0oKAY.5E4CkHUiHIAIBXhO", null, null, "Unverified", "mabel8750_" },
                    { -8, null, null, new DateTime(2024, 4, 21, 23, 34, 50, 404, DateTimeKind.Utc).AddTicks(9572), null, new DateTime(2024, 4, 21, 23, 34, 50, 404, DateTimeKind.Utc).AddTicks(9576), "karla6524@example.com", "Karla Chavez", "$2a$11$xHXAwH9jIz3utBTCf4be4ObHsnR1hRCRQnREhkVwFqPS8IOWG6UiS", null, null, "Unverified", "karla6524" },
                    { -7, null, null, new DateTime(2024, 4, 21, 23, 34, 50, 287, DateTimeKind.Utc).AddTicks(2087), null, new DateTime(2024, 4, 21, 23, 34, 50, 287, DateTimeKind.Utc).AddTicks(2091), "giolucc@example.com", "Giovanni Lucchetta", "$2a$11$fC13mUsQqRvi2Plth1QpqeZhMkfkJrfBWJwuCp8L.L/v4bJJuERMO", null, null, "Unverified", "giolucc" },
                    { -6, null, null, new DateTime(2024, 4, 21, 23, 34, 50, 167, DateTimeKind.Utc).AddTicks(6635), null, new DateTime(2024, 4, 21, 23, 34, 50, 167, DateTimeKind.Utc).AddTicks(6640), "facu597@example.com", "Facundo Castro", "$2a$11$ZVJbHHGVIYx1Ohwl.M0U3eeC/6kqfjRCv7WjD4OrfKJy2IcDgDHcq", null, null, "Unverified", "facu597" },
                    { -5, null, null, new DateTime(2024, 4, 21, 23, 34, 50, 42, DateTimeKind.Utc).AddTicks(2956), null, new DateTime(2024, 4, 21, 23, 34, 50, 42, DateTimeKind.Utc).AddTicks(2959), "ezealeguzman@example.com", "Ezequiel Guzman", "$2a$11$tCj8dO9fuiEnvi43eYSRY.c7OPuL5qlqndJqrIMPsPeosZtaPenEa", null, null, "Unverified", "ezealeguzman" },
                    { -4, null, null, new DateTime(2024, 4, 21, 23, 34, 49, 924, DateTimeKind.Utc).AddTicks(8399), null, new DateTime(2024, 4, 21, 23, 34, 49, 924, DateTimeKind.Utc).AddTicks(8403), "ema_ramirez@example.com", "Emanuel Ramirez", "$2a$11$bNRM4R0dPdKL7YywTBHz7.nHfAO5E4wbge4BLEfZU99EeJ3Cgm8rO", null, null, "Unverified", "ema_ramirez" },
                    { -3, null, null, new DateTime(2024, 4, 21, 23, 34, 49, 805, DateTimeKind.Utc).AddTicks(3030), null, new DateTime(2024, 4, 21, 23, 34, 49, 805, DateTimeKind.Utc).AddTicks(3034), "4rnol@example.com", "Arnol Flores", "$2a$11$/uJ073oAhWMsT0sZYg0ps.2NfK.NOIFY7kKvkFWTQaDsc1U2ZQYaC", null, null, "Unverified", "4rnol" },
                    { -2, null, null, new DateTime(2024, 4, 21, 23, 34, 49, 685, DateTimeKind.Utc).AddTicks(4831), null, new DateTime(2024, 4, 21, 23, 34, 49, 685, DateTimeKind.Utc).AddTicks(4835), "alexanderfrt@example.com", "Alexander Flores", "$2a$11$/Q46ZB8iGyOjl8HeTkXkf.k.Eta4T1GQWyBR.brDPOKnLxonOeyxC", null, null, "Unverified", "AlexanderFRT" },
                    { -1, null, null, new DateTime(2024, 4, 21, 23, 34, 49, 570, DateTimeKind.Utc).AddTicks(2559), null, new DateTime(2024, 4, 21, 23, 34, 49, 570, DateTimeKind.Utc).AddTicks(2561), "ajruiz2204@example.com", "Alejandro Ruíz", "$2a$11$Y0fp3sgSejmaNgif/bLBU.4O5MsShaNE7wVjcyHMNJiplYubBOdCW", null, null, "Unverified", "ajruiz2204" }
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
