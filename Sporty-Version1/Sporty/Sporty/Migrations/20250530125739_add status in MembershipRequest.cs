using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sporty.Migrations
{
    /// <inheritdoc />
    public partial class addstatusinMembershipRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "MembershipRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "MembershipRequests");
        }
    }
}
