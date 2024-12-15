using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sqlTest.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddBookEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Books",
                newName: "ownerId");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Books",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ownerId",
                table: "Books",
                column: "ownerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_ownerId",
                table: "Books",
                column: "ownerId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_ownerId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ownerId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "ownerId",
                table: "Books",
                newName: "userId");
        }
    }
}
