using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiberarySystem.Migrations
{
    /// <inheritdoc />
    public partial class Designing_admin_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorID",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Author_User_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Pubblisher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pubblisher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pubblisher_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Tipology",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipology", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tipology_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TipologyId = table.Column<int>(type: "int", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    PubblisherId = table.Column<int>(type: "int", nullable: false),
                    PubblicDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBorrowed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Pubblisher_PubblisherId",
                        column: x => x.PubblisherId,
                        principalTable: "Pubblisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Tipology_TipologyId",
                        column: x => x.TipologyId,
                        principalTable: "Tipology",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_User_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatorID",
                table: "User",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Author_CreatorID",
                table: "Author",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CreatorID",
                table: "Book",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_PubblisherId",
                table: "Book",
                column: "PubblisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_TipologyId",
                table: "Book",
                column: "TipologyId");

            migrationBuilder.CreateIndex(
                name: "IX_Pubblisher_UserID",
                table: "Pubblisher",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tipology_UserID",
                table: "Tipology",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_CreatorID",
                table: "User",
                column: "CreatorID",
                principalTable: "User",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_User_CreatorID",
                table: "User");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Pubblisher");

            migrationBuilder.DropTable(
                name: "Tipology");

            migrationBuilder.DropIndex(
                name: "IX_User_CreatorID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "User");
        }
    }
}
