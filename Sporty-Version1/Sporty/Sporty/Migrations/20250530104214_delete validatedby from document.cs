using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sporty.Migrations
{
    /// <inheritdoc />
    public partial class deletevalidatedbyfromdocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AspNetUsers_ValidatedByID",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ValidatedByID",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ValidatedByID",
                table: "Documents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ValidatedByID",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ValidatedByID",
                table: "Documents",
                column: "ValidatedByID");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AspNetUsers_ValidatedByID",
                table: "Documents",
                column: "ValidatedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
