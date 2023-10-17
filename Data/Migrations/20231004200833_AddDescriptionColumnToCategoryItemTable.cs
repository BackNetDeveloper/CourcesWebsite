using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnMoreApp.Data.Migrations
{
    public partial class AddDescriptionColumnToCategoryItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CategoryItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CategoryItems");
        }
    }
}
