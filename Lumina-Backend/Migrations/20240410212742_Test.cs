using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lumina_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SessionToken",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImage",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DNI",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DNI", "DateAdded", "DateOfBirth", "DateUpdated", "Email", "FullName", "Password", "ProfileImage", "SessionToken", "Status", "UserName" },
                values: new object[,]
                {
                    { -5, null, null, new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7113), new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7113), new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7113), "giolucc@example.com", null, "123456", null, null, 0, "giolucc" },
                    { -4, null, null, new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7111), new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7112), new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7112), "ezealeguzman@example.com", null, "123456", null, null, 0, "ezealeguzman" },
                    { -3, null, null, new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7110), new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7110), new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7110), "alexanderfrt@example.com", null, "123456", null, null, 0, "AlexanderFRT" },
                    { -2, null, null, new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7108), new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7108), new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7108), "4rnol@example.com", null, "123456", null, null, 0, "4rnol" },
                    { -1, null, null, new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7097), new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7100), new DateTime(2024, 4, 10, 21, 27, 42, 391, DateTimeKind.Utc).AddTicks(7100), "ajruiz2204@example.com", null, "123456", null, null, 0, "ajruiz2204" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.AlterColumn<string>(
                name: "SessionToken",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImage",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DNI",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
