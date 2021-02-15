using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Truck_API.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    Break = table.Column<string>(nullable: true),
                    M = table.Column<string>(nullable: true),
                    KmStand = table.Column<string>(nullable: true),
                    Privat = table.Column<string>(nullable: true),
                    Fuel = table.Column<string>(nullable: true),
                    AdBlue = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayNo = table.Column<string>(nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    DateTimeChange = table.Column<DateTime>(nullable: false),
                    FieldEnum = table.Column<int>(nullable: false),
                    TimeSheetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditFiles_TimeSheets_TimeSheetId",
                        column: x => x.TimeSheetId,
                        principalTable: "TimeSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditFiles_TimeSheetId",
                table: "AuditFiles",
                column: "TimeSheetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditFiles");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "TimeSheets");
        }
    }
}
