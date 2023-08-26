using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HadisIelts.Server.Migrations
{
    /// <inheritdoc />
    public partial class WritingCorrectionFileTableModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PriceName",
                table: "WritingCorrectionFiles",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<uint>(
                name: "TotalPrice",
                table: "SubmittedWritingCorrectionFiles",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceName",
                table: "WritingCorrectionFiles");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "SubmittedWritingCorrectionFiles");
        }
    }
}
