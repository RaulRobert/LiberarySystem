using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiberarySystem.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase_token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Authentication",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Authentication");
        }
    }
}
