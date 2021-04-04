using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class again2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserNameReceive",
                table: "OrderSendFiles",
                newName: "ReceiveId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiveId",
                table: "OrderSendFiles",
                newName: "UserNameReceive");

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
                table: "HistoryOfUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 1, 28, 17, 28, 33, 161, DateTimeKind.Local).AddTicks(3538));
        }
    }
}
