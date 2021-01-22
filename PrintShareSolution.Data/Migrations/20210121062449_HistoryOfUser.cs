using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class HistoryOfUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryOfUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrinterId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActionHistory = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryOfUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryOfUsers_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryOfUsers_Printers_PrinterId",
                        column: x => x.PrinterId,
                        principalTable: "Printers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "HistoryOfUsers",
                columns: new[] { "Id", "ActionHistory", "DateTime", "FileName", "PrinterId", "UserId" },
                values: new object[] { 1, 0, new DateTime(2021, 1, 21, 13, 24, 47, 926, DateTimeKind.Local).AddTicks(86), "C://xxx.docx", 1, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOfUsers_PrinterId",
                table: "HistoryOfUsers",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOfUsers_UserId",
                table: "HistoryOfUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryOfUsers");

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
        }
    }
}
