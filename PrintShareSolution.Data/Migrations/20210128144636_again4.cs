using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class again4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastRequestTime",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastRequestTime",
                table: "AppUsers");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "9c089b8f-1e84-4571-974a-3b2e2c45bc8b");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "373e0576-1636-4abd-ab8b-41e6f34547f6", "AQAAAAEAACcQAAAAEPGyVm1kk5KvZ0UYeegmVb3F3iV/sTuTeyfnorEctSgTsRv0SK8EkzB0H5yWuEsxsQ==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 28, 20, 58, 24, 422, DateTimeKind.Local).AddTicks(9533));
        }
    }
}
