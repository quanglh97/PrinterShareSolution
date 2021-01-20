using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class ActionOrderToOrderPrintFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionOrder",
                table: "OrderPrintFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1337e323-8b12-4497-a39e-7e8444bd6f04");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "93fc9737-72f8-45a4-8adc-2052db985b5a", "AQAAAAEAACcQAAAAEDT1JGnnGan57K1uITq6C4nJyvdh6BpsEyyyz2fqE/kTMqbBPGZE2yMgWvUhYacePw==" });

            migrationBuilder.UpdateData(
                table: "OrderPrintFiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ActionOrder",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionOrder",
                table: "OrderPrintFiles");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "cbd6da60-bd4f-4062-8368-14d4ec790c99");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "614a6c93-0e5e-4bf3-b63b-c9a1dcad90a4", "AQAAAAEAACcQAAAAEHqNAom+jzQpjQMwex03ZrK93b55N6fjd/4XkCHY3bYvtu8/nDOrJb0D6I79wuVq1Q==" });
        }
    }
}
