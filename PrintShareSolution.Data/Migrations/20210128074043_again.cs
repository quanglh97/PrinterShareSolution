using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintShareSolution.Data.Migrations
{
    public partial class again : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppConfigs",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfigs", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Printers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlockLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserBlockedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlackListFilePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockLists_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderSendFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserNameReceive = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSendFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSendFiles_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryOfUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserReceive = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "ListPrinterOfUser",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrinterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListPrinterOfUser", x => new { x.UserId, x.PrinterId });
                    table.ForeignKey(
                        name: "FK_ListPrinterOfUser_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListPrinterOfUser_Printers_PrinterId",
                        column: x => x.PrinterId,
                        principalTable: "Printers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderPrintFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrinterId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPrintFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPrintFiles_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPrintFiles_Printers_PrinterId",
                        column: x => x.PrinterId,
                        principalTable: "Printers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppConfigs",
                columns: new[] { "Key", "Value" },
                values: new object[,]
                {
                    { "HomeTitle", "This is home page of PrinterShareSolution" },
                    { "HomeKeyword", "This is keyword of PrinterShareSolution" },
                    { "HomeDescription", "This is description of PrinterShareSolution" }
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "b4d10036-38bb-48b4-b787-291a5f2ebeb9", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, "d62430c8-7696-4c3b-8743-89f882201d7e", "quanglehoi@gmail.com", true, "Lê Hội Quang", false, null, "quanglehoi@gmail.com", "admin", "AQAAAAEAACcQAAAAEAA1qa1dvDQnX+iAjJRuph49sxx5V0mr+jACwvjz1z/Aln3XJOcKA5LlSah3dLDLLw==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "Printers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "P1" },
                    { 2, "P2" }
                });

            migrationBuilder.InsertData(
                table: "BlockLists",
                columns: new[] { "Id", "BlackListFilePath", "UserBlockedId", "UserId" },
                values: new object[] { 1, "C://BackList.txt", new Guid("69bd714f-9576-45ba-b5b7-f00649be0044"), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "HistoryOfUsers",
                columns: new[] { "Id", "ActionHistory", "DateTime", "FileName", "PrinterId", "UserId", "UserReceive" },
                values: new object[] { 1, 0, new DateTime(2021, 1, 28, 14, 40, 42, 283, DateTimeKind.Local).AddTicks(5557), "C://xxx.docx", 1, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), null });

            migrationBuilder.InsertData(
                table: "ListPrinterOfUser",
                columns: new[] { "PrinterId", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") },
                    { 2, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") }
                });

            migrationBuilder.InsertData(
                table: "OrderPrintFiles",
                columns: new[] { "Id", "DateTime", "FileName", "FilePath", "FileSize", "PrinterId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx.docx", "C://xxx.docx", 0L, 1, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx.docx", "C://xxx.docx", 0L, 2, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") }
                });

            migrationBuilder.InsertData(
                table: "OrderSendFiles",
                columns: new[] { "Id", "DateTime", "FileName", "FilePath", "FileSize", "UserId", "UserNameReceive" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx.docx", "C://xxx.docx", 0L, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), "KhaiTb" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx.docx", "C://xxx.docx", 0L, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), "KhaiTb" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockLists_UserId",
                table: "BlockLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOfUsers_PrinterId",
                table: "HistoryOfUsers",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOfUsers_UserId",
                table: "HistoryOfUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListPrinterOfUser_PrinterId",
                table: "ListPrinterOfUser",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPrintFiles_PrinterId",
                table: "OrderPrintFiles",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPrintFiles_UserId",
                table: "OrderPrintFiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSendFiles_UserId",
                table: "OrderSendFiles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfigs");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "BlockLists");

            migrationBuilder.DropTable(
                name: "HistoryOfUsers");

            migrationBuilder.DropTable(
                name: "ListPrinterOfUser");

            migrationBuilder.DropTable(
                name: "OrderPrintFiles");

            migrationBuilder.DropTable(
                name: "OrderSendFiles");

            migrationBuilder.DropTable(
                name: "Printers");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
