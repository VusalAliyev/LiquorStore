using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquorStoreFinalProject.Migrations
{
    /// <inheritdoc />
    public partial class someChangesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Blogs",
                newName: "ImageURL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Blogs",
                newName: "Image");
        }
    }
}
