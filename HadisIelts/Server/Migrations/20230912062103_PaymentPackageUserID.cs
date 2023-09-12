using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HadisIelts.Server.Migrations
{
    /// <inheritdoc />
    public partial class PaymentPackageUserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDateTime",
                table: "PaymentGroups",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 12, 6, 21, 3, 703, DateTimeKind.Utc).AddTicks(4899),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 9, 10, 14, 34, 29, 494, DateTimeKind.Utc).AddTicks(1157));

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "PaymentGroups",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "PaymentGroups");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDateTime",
                table: "PaymentGroups",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 10, 14, 34, 29, 494, DateTimeKind.Utc).AddTicks(1157),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 9, 12, 6, 21, 3, 703, DateTimeKind.Utc).AddTicks(4899));
        }
    }
}
