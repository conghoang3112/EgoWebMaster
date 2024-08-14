using Microsoft.EntityFrameworkCore.Migrations;

namespace TLS.DataProvider.Migrations
{
    public partial class dbv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "contacts");
        }
    }
}
