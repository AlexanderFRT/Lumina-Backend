using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumina_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_UserId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Securities_Users_UserId",
                table: "Securities");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountID",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 4, 11, 17, 17, 2, 740, DateTimeKind.Utc).AddTicks(52), new DateTime(2024, 4, 11, 17, 17, 2, 740, DateTimeKind.Utc).AddTicks(52) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 4, 11, 17, 17, 2, 740, DateTimeKind.Utc).AddTicks(51), new DateTime(2024, 4, 11, 17, 17, 2, 740, DateTimeKind.Utc).AddTicks(51) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 4, 11, 17, 17, 2, 740, DateTimeKind.Utc).AddTicks(50), new DateTime(2024, 4, 11, 17, 17, 2, 740, DateTimeKind.Utc).AddTicks(50) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 4, 11, 17, 17, 2, 740, DateTimeKind.Utc).AddTicks(48), new DateTime(2024, 4, 11, 17, 17, 2, 740, DateTimeKind.Utc).AddTicks(48) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 4, 11, 17, 17, 2, 740, DateTimeKind.Utc).AddTicks(41), new DateTime(2024, 4, 11, 17, 17, 2, 740, DateTimeKind.Utc).AddTicks(44) });

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserID",
                table: "Accounts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_UserId",
                table: "Logs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Securities_Users_UserId",
                table: "Securities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountID",
                table: "Transactions",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_UserId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Securities_Users_UserId",
                table: "Securities");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountID",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 4, 11, 16, 58, 46, 494, DateTimeKind.Utc).AddTicks(5817), new DateTime(2024, 4, 11, 16, 58, 46, 494, DateTimeKind.Utc).AddTicks(5817) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 4, 11, 16, 58, 46, 494, DateTimeKind.Utc).AddTicks(5815), new DateTime(2024, 4, 11, 16, 58, 46, 494, DateTimeKind.Utc).AddTicks(5816) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 4, 11, 16, 58, 46, 494, DateTimeKind.Utc).AddTicks(5814), new DateTime(2024, 4, 11, 16, 58, 46, 494, DateTimeKind.Utc).AddTicks(5814) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 4, 11, 16, 58, 46, 494, DateTimeKind.Utc).AddTicks(5813), new DateTime(2024, 4, 11, 16, 58, 46, 494, DateTimeKind.Utc).AddTicks(5813) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTime(2024, 4, 11, 16, 58, 46, 494, DateTimeKind.Utc).AddTicks(5808), new DateTime(2024, 4, 11, 16, 58, 46, 494, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserID",
                table: "Accounts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_UserId",
                table: "Logs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Securities_Users_UserId",
                table: "Securities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountID",
                table: "Transactions",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
