using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sporty.Migrations
{
    /// <inheritdoc />
    public partial class addImagetoEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Events");
        }
    }
}
