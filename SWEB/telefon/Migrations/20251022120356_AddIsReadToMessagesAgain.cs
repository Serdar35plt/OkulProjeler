using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace telefon.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReadToMessagesAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsTrue",
                table: "contacts",
                newName: "IsRead");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "contacts",
                newName: "IsTrue");
        }
    }
}
