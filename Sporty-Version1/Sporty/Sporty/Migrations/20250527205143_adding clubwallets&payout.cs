using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sporty.Migrations
{
    /// <inheritdoc />
    public partial class addingclubwalletspayout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClubWallets",
                columns: table => new
                {
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    TotalEarned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPaidOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubWallets", x => x.ClubId);
                    table.ForeignKey(
                        name: "FK_ClubWallets_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payout",
                columns: table => new
                {
                    PayoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payout", x => x.PayoutId);
                    table.ForeignKey(
                        name: "FK_Payout_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payout_ClubId",
                table: "Payout",
                column: "ClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubWallets");

            migrationBuilder.DropTable(
                name: "Payout");
        }
    }
}
