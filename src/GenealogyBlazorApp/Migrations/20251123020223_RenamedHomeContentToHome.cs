using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenealogyBlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class RenamedHomeContentToHome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeContent");

            migrationBuilder.CreateTable(
                name: "Home",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SiteTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Tagline = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    HeroImagePath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    AboutContent = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileImagePath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    SidebarLinks = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Home", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Home");

            migrationBuilder.CreateTable(
                name: "HomeContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AboutContent = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    HeroImagePath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProfileImagePath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    SidebarLinks = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    SiteTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Tagline = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeContent", x => x.Id);
                });
        }
    }
}
