using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAdminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Discriminator", "Email", "Endereco", "Nome", "SenhaHash" },
                values: new object[] { 1, "ADMIN", "admin@admin.com", null, "Admin Seed", "$2a$11$z22flnDB6OGuDpfeM8t4f.TKM/Q5PXIaXD.im.RHplNfj2LBdEMzm" });
        }
    }
}
