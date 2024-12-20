using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arcas.Server.Migrations
{
    /// <inheritdoc />
    public partial class NullableTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AbstractBook",
                columns: table => new
                {
                    ISBN = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category = table.Column<int>(type: "int", nullable: false),
                    author = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    publishHouse = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbstractBook", x => x.ISBN);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nickname = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telephone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    token = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.userID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConcreteBook",
                columns: table => new
                {
                    bookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ISBN = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    bookState = table.Column<int>(type: "int", nullable: false),
                    cover = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcreteBook", x => x.bookID);
                    table.ForeignKey(
                        name: "FK_ConcreteBook_AbstractBook_ISBN",
                        column: x => x.ISBN,
                        principalTable: "AbstractBook",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConcreteBook_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    commentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ISBN = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bookID = table.Column<int>(type: "int", nullable: false),
                    likeNum = table.Column<int>(type: "int", nullable: true),
                    commentatorID = table.Column<int>(type: "int", nullable: false),
                    commentContent = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AbstractBookISBN = table.Column<string>(type: "varchar(13)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcreteBookbookID = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.commentID);
                    table.ForeignKey(
                        name: "FK_Comment_AbstractBook_AbstractBookISBN",
                        column: x => x.AbstractBookISBN,
                        principalTable: "AbstractBook",
                        principalColumn: "ISBN");
                    table.ForeignKey(
                        name: "FK_Comment_ConcreteBook_ConcreteBookbookID",
                        column: x => x.ConcreteBookbookID,
                        principalTable: "ConcreteBook",
                        principalColumn: "bookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExchangeDetail",
                columns: table => new
                {
                    exchangeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IDA = table.Column<int>(type: "int", nullable: false),
                    IDB = table.Column<int>(type: "int", nullable: false),
                    bookID = table.Column<int>(type: "int", nullable: false),
                    trackingID = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telephone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isApproved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    content = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    detailsA = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    detailsB = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcreteBookbookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeDetail", x => x.exchangeID);
                    table.ForeignKey(
                        name: "FK_ExchangeDetail_ConcreteBook_ConcreteBookbookID",
                        column: x => x.ConcreteBookbookID,
                        principalTable: "ConcreteBook",
                        principalColumn: "bookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeDetail_User_IDA",
                        column: x => x.IDA,
                        principalTable: "User",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeDetail_User_IDB",
                        column: x => x.IDB,
                        principalTable: "User",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AbstractBookISBN",
                table: "Comment",
                column: "AbstractBookISBN");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ConcreteBookbookID",
                table: "Comment",
                column: "ConcreteBookbookID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_userID",
                table: "Comment",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_ConcreteBook_ISBN",
                table: "ConcreteBook",
                column: "ISBN");

            migrationBuilder.CreateIndex(
                name: "IX_ConcreteBook_userID",
                table: "ConcreteBook",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeDetail_ConcreteBookbookID",
                table: "ExchangeDetail",
                column: "ConcreteBookbookID");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeDetail_IDA",
                table: "ExchangeDetail",
                column: "IDA");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeDetail_IDB",
                table: "ExchangeDetail",
                column: "IDB");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "ExchangeDetail");

            migrationBuilder.DropTable(
                name: "ConcreteBook");

            migrationBuilder.DropTable(
                name: "AbstractBook");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
