using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arcas.Server.Migrations
{
    /// <inheritdoc />
    public partial class AbstructUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "category",
                table: "AbstractBook",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "AbstractBook");
        }
    }
}
