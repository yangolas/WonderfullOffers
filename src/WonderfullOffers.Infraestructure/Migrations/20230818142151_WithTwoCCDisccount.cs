using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderfullOffers.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class WithTwoCCDisccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceWithoutDiscount",
                table: "Zalando",
                newName: "PriceWithoutDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDiscount",
                table: "Zalando",
                newName: "PriceWithinDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDiscount",
                table: "Womensecret",
                newName: "PriceWithoutDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDiscount",
                table: "Womensecret",
                newName: "PriceWithinDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDiscount",
                table: "Stradivarius",
                newName: "PriceWithoutDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDiscount",
                table: "Stradivarius",
                newName: "PriceWithinDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDiscount",
                table: "Springfield",
                newName: "PriceWithoutDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDiscount",
                table: "Springfield",
                newName: "PriceWithinDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDiscount",
                table: "Shein",
                newName: "PriceWithoutDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDiscount",
                table: "Shein",
                newName: "PriceWithinDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDiscount",
                table: "Primor",
                newName: "PriceWithoutDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDiscount",
                table: "Primor",
                newName: "PriceWithinDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDiscount",
                table: "Maquillalia",
                newName: "PriceWithoutDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDiscount",
                table: "Maquillalia",
                newName: "PriceWithinDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDiscount",
                table: "Druni",
                newName: "PriceWithoutDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDiscount",
                table: "Druni",
                newName: "PriceWithinDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDiscount",
                table: "Douglas",
                newName: "PriceWithoutDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDiscount",
                table: "Douglas",
                newName: "PriceWithinDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDiscount",
                table: "Amazon",
                newName: "PriceWithoutDisccount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDiscount",
                table: "Amazon",
                newName: "PriceWithinDisccount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceWithoutDisccount",
                table: "Zalando",
                newName: "PriceWithoutDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDisccount",
                table: "Zalando",
                newName: "PriceWithinDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDisccount",
                table: "Womensecret",
                newName: "PriceWithoutDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDisccount",
                table: "Womensecret",
                newName: "PriceWithinDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDisccount",
                table: "Stradivarius",
                newName: "PriceWithoutDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDisccount",
                table: "Stradivarius",
                newName: "PriceWithinDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDisccount",
                table: "Springfield",
                newName: "PriceWithoutDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDisccount",
                table: "Springfield",
                newName: "PriceWithinDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDisccount",
                table: "Shein",
                newName: "PriceWithoutDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDisccount",
                table: "Shein",
                newName: "PriceWithinDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDisccount",
                table: "Primor",
                newName: "PriceWithoutDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDisccount",
                table: "Primor",
                newName: "PriceWithinDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDisccount",
                table: "Maquillalia",
                newName: "PriceWithoutDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDisccount",
                table: "Maquillalia",
                newName: "PriceWithinDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDisccount",
                table: "Druni",
                newName: "PriceWithoutDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDisccount",
                table: "Druni",
                newName: "PriceWithinDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDisccount",
                table: "Douglas",
                newName: "PriceWithoutDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDisccount",
                table: "Douglas",
                newName: "PriceWithinDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDisccount",
                table: "Amazon",
                newName: "PriceWithoutDiscount");

            migrationBuilder.RenameColumn(
                name: "PriceWithinDisccount",
                table: "Amazon",
                newName: "PriceWithinDiscount");
        }
    }
}
