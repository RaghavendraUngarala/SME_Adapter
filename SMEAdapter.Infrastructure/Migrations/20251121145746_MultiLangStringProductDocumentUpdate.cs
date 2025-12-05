using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMEAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MultiLangStringProductDocumentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version_State",
                table: "ProductDocuments");

            migrationBuilder.DropColumn(
                name: "Version_StateDate",
                table: "ProductDocuments");

            migrationBuilder.RenameColumn(
                name: "Version_Summary",
                table: "ProductDocuments",
                newName: "Version_SubTitle");

            migrationBuilder.AlterColumn<string>(
                name: "Version_Title",
                table: "ProductDocuments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Version_Description",
                table: "ProductDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Version_StatusSetDate",
                table: "ProductDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Version_StatusValue",
                table: "ProductDocuments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version_Description",
                table: "ProductDocuments");

            migrationBuilder.DropColumn(
                name: "Version_StatusSetDate",
                table: "ProductDocuments");

            migrationBuilder.DropColumn(
                name: "Version_StatusValue",
                table: "ProductDocuments");

            migrationBuilder.RenameColumn(
                name: "Version_SubTitle",
                table: "ProductDocuments",
                newName: "Version_Summary");

            migrationBuilder.AlterColumn<string>(
                name: "Version_Title",
                table: "ProductDocuments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Version_State",
                table: "ProductDocuments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Version_StateDate",
                table: "ProductDocuments",
                type: "datetime2",
                nullable: true);
        }
    }
}
