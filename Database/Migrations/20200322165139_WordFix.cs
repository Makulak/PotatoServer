using Microsoft.EntityFrameworkCore.Migrations;

namespace PotatoServer.Migrations
{
    public partial class WordFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CleverWord_Word");

            migrationBuilder.AddColumn<string>(
                name: "Definition",
                table: "CleverWord_Word",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Definition",
                table: "CleverWord_Word");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CleverWord_Word",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
