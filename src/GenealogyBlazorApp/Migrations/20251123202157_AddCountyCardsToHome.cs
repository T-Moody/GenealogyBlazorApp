using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenealogyBlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCountyCardsToHome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HuronImagePath",
                table: "Home",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HuronTitle",
                table: "Home",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SanilacImagePath",
                table: "Home",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SanilacTitle",
                table: "Home",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TuscolaImagePath",
                table: "Home",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TuscolaTitle",
                table: "Home",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HuronImagePath",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "HuronTitle",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "SanilacImagePath",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "SanilacTitle",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "TuscolaImagePath",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "TuscolaTitle",
                table: "Home");
        }
    }
}
