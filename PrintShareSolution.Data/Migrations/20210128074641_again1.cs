using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class again1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryOfUsers_Printers_PrinterId",
                table: "HistoryOfUsers");

            migrationBuilder.DropIndex(
                name: "IX_HistoryOfUsers_PrinterId",
                table: "HistoryOfUsers");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "50b3ed4d-ede2-482a-a9f4-006c8a1d6a2f");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6b805687-ad2d-4bd5-b580-1109eb525e5c", "AQAAAAEAACcQAAAAEPaJlPghtBdSBgS6hEUbCq2Ddx0iDHgVEU9R4KgGe8FNFo51psJ1Nq0HMeKYQAgbRw==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 28, 14, 46, 40, 41, DateTimeKind.Local).AddTicks(6655));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "b4d10036-38bb-48b4-b787-291a5f2ebeb9");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d62430c8-7696-4c3b-8743-89f882201d7e", "AQAAAAEAACcQAAAAEAA1qa1dvDQnX+iAjJRuph49sxx5V0mr+jACwvjz1z/Aln3XJOcKA5LlSah3dLDLLw==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 28, 14, 40, 42, 283, DateTimeKind.Local).AddTicks(5557));

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOfUsers_PrinterId",
                table: "HistoryOfUsers",
                column: "PrinterId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryOfUsers_Printers_PrinterId",
                table: "HistoryOfUsers",
                column: "PrinterId",
                principalTable: "Printers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
