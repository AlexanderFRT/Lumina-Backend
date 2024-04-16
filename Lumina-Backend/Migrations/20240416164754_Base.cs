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
                    { -9, null, null, new DateTime(2024, 4, 16, 16, 47, 54, 129, DateTimeKind.Utc).AddTicks(8612), null, new DateTime(2024, 4, 16, 16, 47, 54, 129, DateTimeKind.Utc).AddTicks(8617), "mabel8750_@example.com", "Mabel Ceballos", "$2a$11$Y1bSqAXhMhAr1IcLduoZW.I8Rq1URxUfUGd9KTsojka.GwsbL9Gei", null, null, "Unverified", "mabel8750_" },
                    { -8, null, null, new DateTime(2024, 4, 16, 16, 47, 54, 14, DateTimeKind.Utc).AddTicks(400), null, new DateTime(2024, 4, 16, 16, 47, 54, 14, DateTimeKind.Utc).AddTicks(404), "karla6524@example.com", "Karla Chavez", "$2a$11$zSeH32RcoW6wBoI77Liw2e5JDfYfc2QePKGVZwtfKgd/BzUuj/l96", null, null, "Unverified", "karla6524" },
                    { -7, null, null, new DateTime(2024, 4, 16, 16, 47, 53, 897, DateTimeKind.Utc).AddTicks(2636), null, new DateTime(2024, 4, 16, 16, 47, 53, 897, DateTimeKind.Utc).AddTicks(2639), "giolucc@example.com", "Giovanni Lucchetta", "$2a$11$86YO7MPaCmi0cLRDx3LA/OMhCz5L8bNYFZwtadMn9WuVth5lyvMNu", null, null, "Unverified", "giolucc" },
                    { -6, null, null, new DateTime(2024, 4, 16, 16, 47, 53, 781, DateTimeKind.Utc).AddTicks(3148), null, new DateTime(2024, 4, 16, 16, 47, 53, 781, DateTimeKind.Utc).AddTicks(3156), "facu597@example.com", "Facundo Castro", "$2a$11$Uv/f9eR4yxaGFiZHIC0MkeKq9LsxLyCLoirL/thSDyKmhPeY1AioK", null, null, "Unverified", "facu597" },
                    { -5, null, null, new DateTime(2024, 4, 16, 16, 47, 53, 660, DateTimeKind.Utc).AddTicks(7302), null, new DateTime(2024, 4, 16, 16, 47, 53, 660, DateTimeKind.Utc).AddTicks(7306), "ezealeguzman@example.com", "Ezequiel Guzman", "$2a$11$Gb7NtGIspAnvK6IpAU1iD.IkLq4Mtte.g7ntaJ74i00ppnLbVtLZG", null, null, "Unverified", "ezealeguzman" },
                    { -4, null, null, new DateTime(2024, 4, 16, 16, 47, 53, 541, DateTimeKind.Utc).AddTicks(6846), null, new DateTime(2024, 4, 16, 16, 47, 53, 541, DateTimeKind.Utc).AddTicks(6849), "ema_ramirez@example.com", "Emanuel Ramirez", "$2a$11$KeB9enSHQPL4nKKbes7Sj.an4BSroNJsbtnL4JxaBSSBTkVnB.vKi", null, null, "Unverified", "ema_ramirez" },
                    { -3, null, null, new DateTime(2024, 4, 16, 16, 47, 53, 422, DateTimeKind.Utc).AddTicks(2746), null, new DateTime(2024, 4, 16, 16, 47, 53, 422, DateTimeKind.Utc).AddTicks(2752), "4rnol@example.com", "Arnol Flores", "$2a$11$ZGzVYGF39NOP/0VFSbn2D.dPW.lFG4s3wz.fuANLgIh9cmYcrHExm", null, null, "Unverified", "4rnol" },
                    { -2, null, null, new DateTime(2024, 4, 16, 16, 47, 53, 294, DateTimeKind.Utc).AddTicks(4109), null, new DateTime(2024, 4, 16, 16, 47, 53, 294, DateTimeKind.Utc).AddTicks(4114), "alexanderfrt@example.com", "Alexander Flores", "$2a$11$9u2GdVuHcfbcnvsP7xfR1u5s/P8sqY6plv2mD20vnHIuui0H0HJpu", null, null, "Unverified", "AlexanderFRT" },
                    { -1, null, null, new DateTime(2024, 4, 16, 16, 47, 53, 172, DateTimeKind.Utc).AddTicks(7497), null, new DateTime(2024, 4, 16, 16, 47, 53, 172, DateTimeKind.Utc).AddTicks(7499), "ajruiz2204@example.com", "Alejandro Ruíz", "$2a$11$cOwRns/AVg34HpYY0WM/gOhJPwJr9F5GU1FwxkaaSUJY9B2t5sJoW", null, null, "Unverified", "ajruiz2204" }
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
