using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HadisIelts.Server.Migrations
{
    /// <inheritdoc />
    public partial class UploadPaymentDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadDateTime",
                table: "PaymentGroups");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDateTime",
                table: "PaymentPictures",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDateTime",
                table: "PaymentGroups",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 9, 13, 29, 44, 992, DateTimeKind.Utc).AddTicks(5025));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadDateTime",
                table: "PaymentPictures");

            migrationBuilder.DropColumn(
                name: "LastUpdateDateTime",
                table: "PaymentGroups");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDateTime",
                table: "PaymentGroups",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 3, 10, 39, 4, 929, DateTimeKind.Utc).AddTicks(6858));
        }
    }
}
