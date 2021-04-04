using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class changeSomething : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserBlockedId",
                table: "BlockLists");

            migrationBuilder.AlterColumn<string>(
                name: "BlackListFilePath",
                table: "BlockLists",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "UserBlocked",
                table: "BlockLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "c423d2db-fb72-4eb5-8d58-68c47fa4b66f");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2fa9f919-3177-4054-a7c7-052f21e9f494", "AQAAAAEAACcQAAAAEEZnBelQLZ/O1HBkbMwYSSEFx+Xc4eVxNh1qkH+VgHSWNOqWnfJ1Sj7DDQDOH9u8Mw==" });

            migrationBuilder.UpdateData(
                table: "BlockLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserBlocked",
                value: "DKFAJ56");

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 28, 17, 28, 33, 161, DateTimeKind.Local).AddTicks(3538));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserBlocked",
                table: "BlockLists");

            migrationBuilder.AlterColumn<string>(
                name: "BlackListFilePath",
                table: "BlockLists",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserBlockedId",
                table: "BlockLists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                table: "BlockLists",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserBlockedId",
                value: new Guid("69bd714f-9576-45ba-b5b7-f00649be0044"));

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 28, 14, 46, 40, 41, DateTimeKind.Local).AddTicks(6655));
        }
    }
}
