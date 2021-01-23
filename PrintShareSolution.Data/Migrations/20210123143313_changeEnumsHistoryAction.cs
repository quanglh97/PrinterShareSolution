using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class changeEnumsHistoryAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "088403e4-00bc-4d84-859a-2908db823523");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fbdab5b6-9325-46a7-a8ec-b96341e1aa55", "AQAAAAEAACcQAAAAEJvWD/jsiE0oP7oUezlCk/y8XkbSdjB+8QM8tHLHFYp/SB6p1RKJ/bG2nbx+DxUL0A==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 23, 21, 33, 12, 633, DateTimeKind.Local).AddTicks(7411));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
