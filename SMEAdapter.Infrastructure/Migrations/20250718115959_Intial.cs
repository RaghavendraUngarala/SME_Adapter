using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMEAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManufacturerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProductInfo_ProductDesignation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductInfo_ProductRoot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductInfo_ProductFamily = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductInfo_ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductInfo_OrderCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductInfo_ArticleNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId_URI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId_YearOfConstruction = table.Column<int>(type: "int", nullable: false),
                    ProductId_DateOfManufacture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId_HardwareVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId_FirmwareVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId_SoftwareVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId_CountryOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId_UniqueFacilityIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyLogo = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAssetSpecificProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAssetSpecificProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAssetSpecificProperties_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMarkings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMarkings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMarkings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAssetSpecificProperties_ProductId",
                table: "ProductAssetSpecificProperties",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMarkings_ProductId",
                table: "ProductMarkings",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAssetSpecificProperties");

            migrationBuilder.DropTable(
                name: "ProductMarkings");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
