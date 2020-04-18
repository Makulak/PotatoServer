using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PotatoServer.Migrations
{
    public partial class IBaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "CleverWord_Word",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "CleverWord_Word",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "Camasutra_Position",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Camasutra_Position",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "Camasutra_Category",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Camasutra_Category",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Changed",
                table: "CleverWord_Word");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "CleverWord_Word");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "Camasutra_Position");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Camasutra_Position");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "Camasutra_Category");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Camasutra_Category");
        }
    }
}
