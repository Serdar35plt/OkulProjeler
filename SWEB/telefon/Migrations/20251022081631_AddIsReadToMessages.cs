using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace telefon.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReadToMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTrue",
                table: "contacts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTrue",
                table: "contacts");
        }
    }
}
