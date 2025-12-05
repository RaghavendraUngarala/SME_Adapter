using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMEAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ownedtypeproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                            UPDATE ProductDocuments
                            SET Version_StatusSetDate = NULL
                            WHERE TRY_CONVERT(datetime2, Version_StatusSetDate) IS NULL;
            ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version_StatusSetDate",
                table: "ProductDocuments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductDocuments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "OwnershipType",
                table: "ProductDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnershipType",
                table: "ProductDocuments");

            migrationBuilder.AlterColumn<string>(
                name: "Version_StatusSetDate",
                table: "ProductDocuments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductDocuments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
