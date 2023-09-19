using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace HadisIelts.Server.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedWriting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectedWritingFileID",
                table: "WritingCorrectionFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrected",
                table: "SubmittedWritingCorrectionFiles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CorrectedWritingFiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Data = table.Column<string>(type: "longtext", nullable: false),
                    WritingCorrectionFileID = table.Column<int>(type: "int", nullable: false),
                    CorrectorID = table.Column<string>(type: "longtext", nullable: false),
                    UploadDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectedWritingFiles", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectedWritingFiles");

            migrationBuilder.DropColumn(
                name: "CorrectedWritingFileID",
                table: "WritingCorrectionFiles");

            migrationBuilder.DropColumn(
                name: "IsCorrected",
                table: "SubmittedWritingCorrectionFiles");
        }
    }
}
