using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class addResultHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderPrintFileId",
                table: "HistoryOfUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderSendFileId",
                table: "HistoryOfUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Result",
                table: "HistoryOfUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "524215dc-52c5-4f06-93df-a106b42005e8");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6aefbb6b-523a-4196-b922-3994b2ae55e7", "AQAAAAEAACcQAAAAEK0C6Y6Jw91Mn/ICnFn7uK8AGnel4JWmadRpX/6URu/BaLpx1l21/DbLAivCwXgaUw==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 2, 1, 3, 4, 5, 494, DateTimeKind.Local).AddTicks(2833));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderPrintFileId",
                table: "HistoryOfUsers");

            migrationBuilder.DropColumn(
                name: "OrderSendFileId",
                table: "HistoryOfUsers");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "HistoryOfUsers");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "8c513fe6-90a0-4197-89c9-0cea95bb5e5e");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5c9cfc11-2f03-4fcf-b7bf-da0c1bab1423", "AQAAAAEAACcQAAAAEGBzhyxo0hBaX9CMZQZTrX4GGK67oAGX34eHdqTzfBj6gI10y7bhA9h5O7YiCCDKzA==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 31, 22, 33, 49, 722, DateTimeKind.Local).AddTicks(9967));
        }
    }
}
