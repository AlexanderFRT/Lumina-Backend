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
                    { -6, null, null, new DateTime(2024, 5, 1, 20, 25, 31, 558, DateTimeKind.Utc).AddTicks(5640), null, new DateTime(2024, 5, 1, 20, 25, 31, 558, DateTimeKind.Utc).AddTicks(5644), "mabel8750_@example.com", "Mabel Ceballos", "$2a$11$4Dkesh9F3O/egy9V8G3cCuA.CzOc5Mggzl0hDGAE1Ca6icLypnwg2", null, null, "Unverified", "mabel8750_" },
                    { -5, null, null, new DateTime(2024, 5, 1, 20, 25, 31, 432, DateTimeKind.Utc).AddTicks(5799), null, new DateTime(2024, 5, 1, 20, 25, 31, 432, DateTimeKind.Utc).AddTicks(5803), "karla6524@example.com", "Karla Chavez", "$2a$11$.0Kc5qA/xb5pvKBu1RFXa.w/LjKuszyBMpPvjKGiX5udtT2FhtZ8K", null, null, "Unverified", "karla6524" },
                    { -4, null, null, new DateTime(2024, 5, 1, 20, 25, 31, 301, DateTimeKind.Utc).AddTicks(6609), null, new DateTime(2024, 5, 1, 20, 25, 31, 301, DateTimeKind.Utc).AddTicks(6613), "facu597@example.com", "Facundo Castro", "$2a$11$Mm1cuR86P/gMJfgon84GEOeP9/JA5CzBSjLu4mXHZyY9zd0STMnK2", null, null, "Unverified", "facu597" },
                    { -3, null, null, new DateTime(2024, 5, 1, 20, 25, 31, 175, DateTimeKind.Utc).AddTicks(2698), null, new DateTime(2024, 5, 1, 20, 25, 31, 175, DateTimeKind.Utc).AddTicks(2703), "ezealeguzman@example.com", "Ezequiel Guzman", "$2a$11$DIauooOGsf7vKAVrolsKR.z/gI8RqWF7kUvredYDaL6RmmRDlNSaS", null, null, "Unverified", "ezealeguzman" },
                    { -2, null, null, new DateTime(2024, 5, 1, 20, 25, 31, 42, DateTimeKind.Utc).AddTicks(4607), null, new DateTime(2024, 5, 1, 20, 25, 31, 42, DateTimeKind.Utc).AddTicks(4611), "alexanderfrt@example.com", "Alexander Flores", "$2a$11$acWjE3hiMXsUDdMP6Accfu2frshw8y92zwg9RQrbpC8i/TnJ/l7ry", null, null, "Unverified", "AlexanderFRT" },
                    { -1, null, null, new DateTime(2024, 5, 1, 20, 25, 30, 908, DateTimeKind.Utc).AddTicks(8448), null, new DateTime(2024, 5, 1, 20, 25, 30, 908, DateTimeKind.Utc).AddTicks(8451), "ajruiz2204@example.com", "Alejandro Ruíz", "$2a$11$vutuXe.txmwo6pMxRxXbiO847TBA5Gr32niuz4u93p.UyCmBmAmSy", null, null, "Unverified", "ajruiz2204" }
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
