using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiberarySystem.Migrations
{
    /// <inheritdoc />
    public partial class DeleteISBN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Book");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
