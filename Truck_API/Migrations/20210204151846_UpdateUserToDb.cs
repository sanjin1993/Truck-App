using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Truck_API.Migrations
{
    public partial class UpdateUserToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FM",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FM",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "User");
        }
    }
}
