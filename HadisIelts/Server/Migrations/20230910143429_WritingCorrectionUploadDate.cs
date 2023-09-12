using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HadisIelts.Server.Migrations
{
    /// <inheritdoc />
    public partial class WritingCorrectionUploadDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDateTime",
                table: "SubmittedWritingCorrectionFiles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDateTime",
                table: "PaymentGroups",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 10, 14, 34, 29, 494, DateTimeKind.Utc).AddTicks(1157),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 9, 9, 13, 29, 44, 992, DateTimeKind.Utc).AddTicks(5025));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmissionDateTime",
                table: "SubmittedWritingCorrectionFiles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDateTime",
                table: "PaymentGroups",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 9, 13, 29, 44, 992, DateTimeKind.Utc).AddTicks(5025),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 9, 10, 14, 34, 29, 494, DateTimeKind.Utc).AddTicks(1157));
        }
    }
}
