using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class again3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserReceive",
                table: "HistoryOfUsers",
                newName: "ReceiveId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiveId",
                table: "HistoryOfUsers",
                newName: "UserReceive");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5acc1605-ece9-4c62-8a54-56907ded2628");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "82f577ec-f587-429a-933c-859f74eeffdd", "AQAAAAEAACcQAAAAEHVHRIyRxRY48w+qU7FK1/vd4lSO4f0cSK2MQlJ6BExocIdPfIQDFRnZu8TuE2GKNQ==" });

            migrationBuilder.UpdateData(
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 28, 20, 45, 40, 52, DateTimeKind.Local).AddTicks(9544));
        }
    }
}
