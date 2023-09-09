using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace HadisIelts.Server.Migrations
{
    /// <inheritdoc />
    public partial class writingCorrectionPaymentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubmittedWritingCorrectionFiles",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(255)", nullable: false),
                    UserID = table.Column<string>(type: "varchar(255)", nullable: false),
                    TotalPrice = table.Column<uint>(type: "int unsigned", nullable: false),
                    PaymentGroupID = table.Column<string>(type: "longtext", nullable: false)
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
                name: "WritingTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingTypes", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaymentGroups",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(255)", nullable: false),
                    SubmittedServiceID = table.Column<string>(type: "longtext", nullable: false),
                    UploadDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2023, 8, 29, 7, 8, 10, 251, DateTimeKind.Utc).AddTicks(110)),
                    IsPaymentApproved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsPaymentCheckPending = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Message = table.Column<string>(type: "longtext", nullable: false),
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentGroups_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ID",
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
                    PriceName = table.Column<string>(type: "longtext", nullable: false),
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
                name: "WritingCorrectionServicePrices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    WritingTypeID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<uint>(type: "int unsigned", nullable: false),
                    WordCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingCorrectionServicePrices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WritingCorrectionServicePrices_WritingTypes_WritingTypeID",
                        column: x => x.WritingTypeID,
                        principalTable: "WritingTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaymentPictures",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Data = table.Column<string>(type: "longtext", nullable: false),
                    FileSuffix = table.Column<string>(type: "longtext", nullable: false),
                    IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Message = table.Column<string>(type: "longtext", nullable: false),
                    PaymentGroupID = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPictures", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentPictures_PaymentGroups_PaymentGroupID",
                        column: x => x.PaymentGroupID,
                        principalTable: "PaymentGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentGroups_ServiceID",
                table: "PaymentGroups",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPictures_PaymentGroupID",
                table: "PaymentPictures",
                column: "PaymentGroupID");

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
                name: "IX_WritingCorrectionServicePrices_WritingTypeID",
                table: "WritingCorrectionServicePrices",
                column: "WritingTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentPictures");

            migrationBuilder.DropTable(
                name: "WritingCorrectionFiles");

            migrationBuilder.DropTable(
                name: "WritingCorrectionServicePrices");

            migrationBuilder.DropTable(
                name: "PaymentGroups");

            migrationBuilder.DropTable(
                name: "SubmittedWritingCorrectionFiles");

            migrationBuilder.DropTable(
                name: "WritingTypes");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
