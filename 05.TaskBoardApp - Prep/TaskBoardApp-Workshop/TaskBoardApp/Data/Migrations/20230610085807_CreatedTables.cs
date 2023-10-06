using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class CreatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "538b5de5-8ab4-4776-bea8-24e85c146195", 0, "0fd4b8c9-a0a3-4101-89f6-b2ef8b20b976", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAENSQZjAJ7NrIYYZdUaZ2A3/21eWJrS87GUbTZYpjuSxrcYXeZLqKSWXdyaiqHRfKgQ==", null, false, "27a9d6dd-8385-4eac-a1e1-e10fe46b3cda", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 11, 22, 11, 58, 7, 415, DateTimeKind.Local).AddTicks(2524), "Implement better styling for all public pages", "538b5de5-8ab4-4776-bea8-24e85c146195", "Improve CSS styles", null },
                    { 2, 1, new DateTime(2023, 1, 10, 11, 58, 7, 415, DateTimeKind.Local).AddTicks(2573), "Create Android client app for the RESTful API", "538b5de5-8ab4-4776-bea8-24e85c146195", "Android Client App", null },
                    { 3, 1, new DateTime(2023, 5, 10, 11, 58, 7, 415, DateTimeKind.Local).AddTicks(2579), "Create Windows Forms desktop client app for the RESTful API", "538b5de5-8ab4-4776-bea8-24e85c146195", "Desktop Client App", null },
                    { 4, 1, new DateTime(2022, 6, 10, 11, 58, 7, 415, DateTimeKind.Local).AddTicks(2583), "Implement [Create Tasks] page for adding new tasks", "538b5de5-8ab4-4776-bea8-24e85c146195", "Create Tasks", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "538b5de5-8ab4-4776-bea8-24e85c146195");
        }
    }
}
