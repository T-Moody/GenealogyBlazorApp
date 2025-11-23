using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenealogyBlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class AddHomeCaptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutSectionTitle",
                table: "Home",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageCaption",
                table: "Home",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutSectionTitle",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "ProfileImageCaption",
                table: "Home");
        }
    }
}
