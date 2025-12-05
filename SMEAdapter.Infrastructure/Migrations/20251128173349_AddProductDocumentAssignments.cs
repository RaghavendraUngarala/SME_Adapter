using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMEAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductDocumentAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductDocumentAssignments",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDocumentAssignments", x => new { x.ProductId, x.ProductDocumentId });
                    table.ForeignKey(
                        name: "FK_ProductDocumentAssignments_ProductDocuments_ProductDocumentId",
                        column: x => x.ProductDocumentId,
                        principalTable: "ProductDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductDocumentAssignments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocumentAssignments_ProductDocumentId",
                table: "ProductDocumentAssignments",
                column: "ProductDocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDocumentAssignments");
        }
    }
}
