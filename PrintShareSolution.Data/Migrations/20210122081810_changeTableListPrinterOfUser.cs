using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class changeTableListPrinterOfUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ListPrinterOfUser",
                table: "ListPrinterOfUser");

            migrationBuilder.DropIndex(
                name: "IX_ListPrinterOfUser_UserId",
                table: "ListPrinterOfUser");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ListPrinterOfUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListPrinterOfUser",
                table: "ListPrinterOfUser",
                columns: new[] { "UserId", "PrinterId" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ListPrinterOfUser",
                table: "ListPrinterOfUser");

            migrationBuilder.DeleteData(
                table: "ListPrinterOfUser",
                keyColumns: new[] { "PrinterId", "UserId" },
                keyValues: new object[] { 1, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.DeleteData(
                table: "ListPrinterOfUser",
                keyColumns: new[] { "PrinterId", "UserId" },
                keyValues: new object[] { 2, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ListPrinterOfUser",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListPrinterOfUser",
                table: "ListPrinterOfUser",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f732d98f-d3e9-422e-9070-dab4c635289a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "50f28690-7103-4279-a12a-ca634d3e1871", "AQAAAAEAACcQAAAAEPKr1YQaUsSKnxuHdkc86Jc/ysFEH6blP+Iqt2lVlmU2xKgceIz8EP2GRAkgzz1gfQ==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 21, 13, 24, 47, 926, DateTimeKind.Local).AddTicks(86));

            migrationBuilder.InsertData(
                table: "ListPrinterOfUser",
                columns: new[] { "Id", "PrinterId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") },
                    { 2, 2, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListPrinterOfUser_UserId",
                table: "ListPrinterOfUser",
                column: "UserId");
        }
    }
}
