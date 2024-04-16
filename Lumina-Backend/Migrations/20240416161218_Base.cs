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
                    { -9, null, null, new DateTime(2024, 4, 16, 16, 12, 18, 202, DateTimeKind.Utc).AddTicks(3793), null, new DateTime(2024, 4, 16, 16, 12, 18, 202, DateTimeKind.Utc).AddTicks(3797), "mabel8750_@example.com", "Mabel Ceballos", "$2a$11$b9Yj0oFU037ZnFzkgzdNHe9WzMMTEcdesj0NnUMJWrjVWzy1cKF8W", null, null, "Unverified", "mabel8750_" },
                    { -8, null, null, new DateTime(2024, 4, 16, 16, 12, 18, 83, DateTimeKind.Utc).AddTicks(3648), null, new DateTime(2024, 4, 16, 16, 12, 18, 83, DateTimeKind.Utc).AddTicks(3651), "karla6524@example.com", "Karla Chavez", "$2a$11$4AXjRfgLoNQZ2cYimg/aRu/NcCbJwMoN51w5LDr9On/L40gw.NFFK", null, null, "Unverified", "karla6524" },
                    { -7, null, null, new DateTime(2024, 4, 16, 16, 12, 17, 961, DateTimeKind.Utc).AddTicks(2858), null, new DateTime(2024, 4, 16, 16, 12, 17, 961, DateTimeKind.Utc).AddTicks(2862), "giolucc@example.com", "Giovanni Lucchetta", "$2a$11$d6WywWN3LwUjybHsdkTnnOA/VbCskFoEz3jHvCoYZnN5QyhvtyuP2", null, null, "Unverified", "giolucc" },
                    { -6, null, null, new DateTime(2024, 4, 16, 16, 12, 17, 843, DateTimeKind.Utc).AddTicks(1194), null, new DateTime(2024, 4, 16, 16, 12, 17, 843, DateTimeKind.Utc).AddTicks(1199), "facu597@example.com", "Facundo Castro", "$2a$11$b4SKB9OtwM3mEogZ3HFeEOiNciNFgwHsE5Y.DOEdGva2OZa9CFopS", null, null, "Unverified", "facu597" },
                    { -5, null, null, new DateTime(2024, 4, 16, 16, 12, 17, 720, DateTimeKind.Utc).AddTicks(8451), null, new DateTime(2024, 4, 16, 16, 12, 17, 720, DateTimeKind.Utc).AddTicks(8456), "ezealeguzman@example.com", "Ezequiel Guzman", "$2a$11$3yS42NIMEOxCajtvP9DM/.Cg.otrj/uCnqE/Y2ZitBS2pa6fHzaRa", null, null, "Unverified", "ezealeguzman" },
                    { -4, null, null, new DateTime(2024, 4, 16, 16, 12, 17, 601, DateTimeKind.Utc).AddTicks(3024), null, new DateTime(2024, 4, 16, 16, 12, 17, 601, DateTimeKind.Utc).AddTicks(3028), "ema_ramirez@example.com", "Emanuel Ramirez", "$2a$11$DRxNIeJzHlvwlciYDRAXo.NqU4f5PcqknodrvsTBV3.GTIedZU35i", null, null, "Unverified", "ema_ramirez" },
                    { -3, null, null, new DateTime(2024, 4, 16, 16, 12, 17, 478, DateTimeKind.Utc).AddTicks(5811), null, new DateTime(2024, 4, 16, 16, 12, 17, 478, DateTimeKind.Utc).AddTicks(5815), "4rnol@example.com", "Arnol Flores", "$2a$11$m7/EMJzEZCIY4YYO6jM4SevLJ0/wGE9YYQl2V09Q2afh/5QnTxFL2", null, null, "Unverified", "4rnol" },
                    { -2, null, null, new DateTime(2024, 4, 16, 16, 12, 17, 356, DateTimeKind.Utc).AddTicks(7186), null, new DateTime(2024, 4, 16, 16, 12, 17, 356, DateTimeKind.Utc).AddTicks(7191), "alexanderfrt@example.com", "Alexander Flores", "$2a$11$8qWWfAnwnaEzP7cRjAHhNOo.w18W1D8uxcZRQzv63RpL9XI577ZrG", null, null, "Unverified", "AlexanderFRT" },
                    { -1, null, null, new DateTime(2024, 4, 16, 16, 12, 17, 238, DateTimeKind.Utc).AddTicks(9687), null, new DateTime(2024, 4, 16, 16, 12, 17, 238, DateTimeKind.Utc).AddTicks(9690), "ajruiz2204@example.com", "Alejandro Ruíz", "$2a$11$EwmJZ3iZZgJkhKfM/wLc.e0G3EXjRUV0MzNRyDHVSCoV1chrDACpm", null, null, "Unverified", "ajruiz2204" }
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
