using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMEAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TechnicalDataTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechnicalDataTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdtaSubmodelId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalDataTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTechnicalData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTechnicalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTechnicalData_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTechnicalData_TechnicalDataTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TechnicalDataTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemplateSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SemanticId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateSections_TechnicalDataTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TechnicalDataTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SemanticId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateProperties_TemplateSections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "TemplateSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTechnicalDataProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductTechnicalDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplatePropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsCustomProperty = table.Column<bool>(type: "bit", nullable: false),
                    CustomPropertyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomSemanticId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTechnicalDataProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTechnicalDataProperties_ProductTechnicalData_ProductTechnicalDataId",
                        column: x => x.ProductTechnicalDataId,
                        principalTable: "ProductTechnicalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTechnicalDataProperties_TemplateProperties_TemplatePropertyId",
                        column: x => x.TemplatePropertyId,
                        principalTable: "TemplateProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTechnicalData_ProductId",
                table: "ProductTechnicalData",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTechnicalData_TemplateId",
                table: "ProductTechnicalData",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTechnicalDataProperties_ProductTechnicalDataId",
                table: "ProductTechnicalDataProperties",
                column: "ProductTechnicalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTechnicalDataProperties_TemplatePropertyId",
                table: "ProductTechnicalDataProperties",
                column: "TemplatePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateProperties_SectionId",
                table: "TemplateProperties",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateSections_TemplateId",
                table: "TemplateSections",
                column: "TemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTechnicalDataProperties");

            migrationBuilder.DropTable(
                name: "ProductTechnicalData");

            migrationBuilder.DropTable(
                name: "TemplateProperties");

            migrationBuilder.DropTable(
                name: "TemplateSections");

            migrationBuilder.DropTable(
                name: "TechnicalDataTemplates");
        }
    }
}
