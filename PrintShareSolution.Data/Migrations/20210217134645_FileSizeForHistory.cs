using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class FileSizeForHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "HistoryOfUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "d42ab596-150f-4591-9a34-0aca5b75367e");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3d9ab39d-2ec3-4697-9a8b-b5d085d5ad88", "AQAAAAEAACcQAAAAEKjbONRrnB/g9AJVg4/q6ZdD7zwNrYq8MoWK+2p7CEdgw9BaKPjdpsEOgbQqqW0ZaA==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 2, 17, 20, 46, 43, 924, DateTimeKind.Local).AddTicks(3179));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "HistoryOfUsers");

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
    }
}
