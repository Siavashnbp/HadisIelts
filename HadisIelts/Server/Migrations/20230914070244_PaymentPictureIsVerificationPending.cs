using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HadisIelts.Server.Migrations
{
    /// <inheritdoc />
    public partial class PaymentPictureIsVerificationPending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerificationPending",
                table: "PaymentPictures",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDateTime",
                table: "PaymentGroups",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 9, 12, 6, 21, 3, 703, DateTimeKind.Utc).AddTicks(4899));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerificationPending",
                table: "PaymentPictures");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDateTime",
                table: "PaymentGroups",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 6, 21, 3, 703, DateTimeKind.Utc).AddTicks(4899),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }
    }
}
