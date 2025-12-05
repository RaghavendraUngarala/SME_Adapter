using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMEAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProductDomainAndCommands : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Products");
        }
    }
}
