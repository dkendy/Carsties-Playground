using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Changefieldsnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReservedPrice",
                table: "Auctions",
                newName: "ReservePrice");

            migrationBuilder.RenameColumn(
                name: "ActionEnd",
                table: "Auctions",
                newName: "AuctionEnd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReservePrice",
                table: "Auctions",
                newName: "ReservedPrice");

            migrationBuilder.RenameColumn(
                name: "AuctionEnd",
                table: "Auctions",
                newName: "ActionEnd");
        }
    }
}
