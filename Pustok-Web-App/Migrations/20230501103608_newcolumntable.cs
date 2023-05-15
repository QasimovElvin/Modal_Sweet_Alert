using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pustok_Web_App.Migrations
{
    public partial class newcolumntable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "BookImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "BookImages");
        }
    }
}
