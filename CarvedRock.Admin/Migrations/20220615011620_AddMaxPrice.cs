using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarvedRock.Admin.Migrations
{
    public partial class AddMaxPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MaxPrice",
                table: "Categories",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPrice",
                table: "Categories");
        }
    }
}
