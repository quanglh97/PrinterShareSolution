using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class changeTableListPrinterOfUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "db2787e1-16c6-44eb-b1b7-3a7dbce61ace");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d4818ee7-bb1d-4418-84fa-274da5ffcfea", "AQAAAAEAACcQAAAAED+ig1QaTxrxT3Xck9QpV/N/vGfL5WNxmPgunukMMe28/leIes1pjgb6h2nOq1KxYA==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 22, 15, 20, 55, 185, DateTimeKind.Local).AddTicks(2627));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "7ef06d3a-73d2-46aa-b9b1-b5e22fa14f17");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0abf5e19-48f6-44e2-868b-036fe4ffe333", "AQAAAAEAACcQAAAAENENSUcR9BUN0IZ8IxcuaJPyz4hd++jSZbOE2GQFUKCvChycNW6iyW1fRbtNEkAtxA==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 22, 15, 18, 8, 875, DateTimeKind.Local).AddTicks(2680));
        }
    }
}
