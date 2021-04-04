using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class AddPages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Pages",
                table: "OrderPrintFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pages",
                table: "HistoryOfUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pages",
                table: "OrderPrintFiles");

            migrationBuilder.DropColumn(
                name: "Pages",
                table: "HistoryOfUsers");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "52c8d680-b379-4a4c-aa44-2020a84ea3a0");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3c059ad4-d5e5-4382-a87a-9b52048078a4", "AQAAAAEAACcQAAAAEBhif0/6HEsXMyYHDMGupnKiJUaT++VldFJr6k3f0cW8V5epu/iBi1chQTWL4p/AJw==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 28, 21, 46, 34, 784, DateTimeKind.Local).AddTicks(664));
        }
    }
}
