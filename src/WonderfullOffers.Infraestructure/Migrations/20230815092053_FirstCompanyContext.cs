using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderfullOffers.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstCompanyContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amazon",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Coupon = table.Column<int>(type: "int", nullable: true),
                    TimeSpan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disccount = table.Column<int>(type: "int", nullable: false),
                    PriceWithinDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceWithoutDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amazon", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "Douglas",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeSpan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disccount = table.Column<int>(type: "int", nullable: false),
                    PriceWithinDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceWithoutDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Douglas", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "Druni",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeSpan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disccount = table.Column<int>(type: "int", nullable: false),
                    PriceWithinDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceWithoutDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Druni", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "Maquillalia",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeSpan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disccount = table.Column<int>(type: "int", nullable: false),
                    PriceWithinDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceWithoutDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquillalia", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "Primor",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeSpan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disccount = table.Column<int>(type: "int", nullable: false),
                    PriceWithinDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceWithoutDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Primor", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "Shein",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeSpan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disccount = table.Column<int>(type: "int", nullable: false),
                    PriceWithinDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceWithoutDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shein", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "Springfield",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeSpan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disccount = table.Column<int>(type: "int", nullable: false),
                    PriceWithinDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceWithoutDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Springfield", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "Stradivarius",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeSpan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disccount = table.Column<int>(type: "int", nullable: false),
                    PriceWithinDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceWithoutDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stradivarius", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "Womensecret",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeSpan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disccount = table.Column<int>(type: "int", nullable: false),
                    PriceWithinDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceWithoutDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Womensecret", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "Zalando",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeSpan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disccount = table.Column<int>(type: "int", nullable: false),
                    PriceWithinDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceWithoutDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zalando", x => x.Title);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amazon");

            migrationBuilder.DropTable(
                name: "Douglas");

            migrationBuilder.DropTable(
                name: "Druni");

            migrationBuilder.DropTable(
                name: "Maquillalia");

            migrationBuilder.DropTable(
                name: "Primor");

            migrationBuilder.DropTable(
                name: "Shein");

            migrationBuilder.DropTable(
                name: "Springfield");

            migrationBuilder.DropTable(
                name: "Stradivarius");

            migrationBuilder.DropTable(
                name: "Womensecret");

            migrationBuilder.DropTable(
                name: "Zalando");
        }
    }
}
