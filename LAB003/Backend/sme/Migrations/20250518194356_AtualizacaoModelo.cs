using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sme.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_allocation_date",
                table: "professor",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_allocation_date",
                table: "professor");
        }
    }
}
