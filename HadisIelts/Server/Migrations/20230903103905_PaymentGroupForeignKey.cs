using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HadisIelts.Server.Migrations
{
    /// <inheritdoc />
    public partial class PaymentGroupForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaymentGroupID",
                table: "SubmittedWritingCorrectionFiles",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadDateTime",
                table: "PaymentGroups",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 3, 10, 39, 4, 929, DateTimeKind.Utc).AddTicks(6858),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 8, 29, 7, 11, 51, 79, DateTimeKind.Utc).AddTicks(6072));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaymentGroupID",
                table: "SubmittedWritingCorrectionFiles",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadDateTime",
                table: "PaymentGroups",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 29, 7, 11, 51, 79, DateTimeKind.Utc).AddTicks(6072),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 9, 3, 10, 39, 4, 929, DateTimeKind.Utc).AddTicks(6858));
        }
    }
}
