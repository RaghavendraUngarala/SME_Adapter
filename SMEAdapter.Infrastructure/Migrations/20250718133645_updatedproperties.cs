using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMEAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAssetSpecificProperties");

            migrationBuilder.DropTable(
                name: "ProductMarkings");

            migrationBuilder.DropColumn(
                name: "ProductId_CountryOfOrigin",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId_DateOfManufacture",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId_FirmwareVersion",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId_HardwareVersion",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId_SoftwareVersion",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId_URI",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId_UniqueFacilityIdentifier",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId_YearOfConstruction",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductId_SerialNumber",
                table: "Products",
                newName: "SerialNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SerialNumber",
                table: "Products",
                newName: "ProductId_SerialNumber");

            migrationBuilder.AddColumn<string>(
                name: "ProductId_CountryOfOrigin",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductId_DateOfManufacture",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId_FirmwareVersion",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId_HardwareVersion",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId_SoftwareVersion",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId_URI",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId_UniqueFacilityIdentifier",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId_YearOfConstruction",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductAssetSpecificProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
