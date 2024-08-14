using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TLS.DataProvider.Migrations
{
    public partial class dbv6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "surveys",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Createddate",
                table: "planexecutioninfos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "surveys");

            migrationBuilder.DropColumn(
                name: "Createddate",
                table: "planexecutioninfos");
        }
    }
}
