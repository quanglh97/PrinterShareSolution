using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class addFileSizeForOrderPrintFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "OrderPrintFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "cc36e4aa-31e3-4384-90e8-fc795f702434");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a5a3ae48-7d47-41c7-8e33-aafbc7ff65d4", "AQAAAAEAACcQAAAAENTlQv+O8KIHuc2iNpfC+WIoLSRMaiOF040khO9f5LUq47rlGX7n/opguyzXpUWkLg==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 23, 1, 17, 0, 402, DateTimeKind.Local).AddTicks(6087));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "OrderPrintFiles");

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
    }
}
