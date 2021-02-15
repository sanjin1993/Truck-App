using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Truck_API.Migrations
{
    public partial class UpdateTimeSheetToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTimeSheet",
                table: "TimeSheets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTimeSheet",
                table: "TimeSheets");
        }
    }
}
