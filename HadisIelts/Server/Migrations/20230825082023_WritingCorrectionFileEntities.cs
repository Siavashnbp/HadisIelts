using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace HadisIelts.Server.Migrations
{
    /// <inheritdoc />
    public partial class WritingCorrectionFileEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<uint>(
                name: "Price",
                table: "WritingCorrectionServicePrices",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "SubmittedWritingCorrectionFiles",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(255)", nullable: false),
                    UserID = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmittedWritingCorrectionFiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubmittedWritingCorrectionFiles_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WritingCorrectionFiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Data = table.Column<string>(type: "longtext", nullable: false),
                    WordCount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<uint>(type: "int unsigned", nullable: false),
                    ApplicationWritingTypeID = table.Column<int>(type: "int", nullable: false),
                    SubmittedWritingCorecionFilesID = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingCorrectionFiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WritingCorrectionFiles_SubmittedWritingCorrectionFiles_Submi~",
                        column: x => x.SubmittedWritingCorecionFilesID,
                        principalTable: "SubmittedWritingCorrectionFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WritingCorrectionFiles_WritingTypes_ApplicationWritingTypeID",
                        column: x => x.ApplicationWritingTypeID,
                        principalTable: "WritingTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WritingPaymentPictures",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(255)", nullable: false),
                    Data = table.Column<string>(type: "longtext", nullable: false),
                    SubmitedWritingCorrectionFilesID = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingPaymentPictures", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WritingPaymentPictures_SubmittedWritingCorrectionFiles_Submi~",
                        column: x => x.SubmitedWritingCorrectionFilesID,
                        principalTable: "SubmittedWritingCorrectionFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedWritingCorrectionFiles_UserID",
                table: "SubmittedWritingCorrectionFiles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WritingCorrectionFiles_ApplicationWritingTypeID",
                table: "WritingCorrectionFiles",
                column: "ApplicationWritingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_WritingCorrectionFiles_SubmittedWritingCorecionFilesID",
                table: "WritingCorrectionFiles",
                column: "SubmittedWritingCorecionFilesID");

            migrationBuilder.CreateIndex(
                name: "IX_WritingPaymentPictures_SubmitedWritingCorrectionFilesID",
                table: "WritingPaymentPictures",
                column: "SubmitedWritingCorrectionFilesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WritingCorrectionFiles");

            migrationBuilder.DropTable(
                name: "WritingPaymentPictures");

            migrationBuilder.DropTable(
                name: "SubmittedWritingCorrectionFiles");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "WritingCorrectionServicePrices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned");
        }
    }
}
