using Microsoft.EntityFrameworkCore.Migrations;

namespace PotatoServer.Migrations
{
    public partial class hccmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppearanceRating",
                table: "Statistics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComfortRating",
                table: "Statistics",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppearanceRating",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "ComfortRating",
                table: "Statistics");
        }
    }
}
