using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SyncMe.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CONTENT",
                columns: table => new
                {
                    ID_CONTENT = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_TITLE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DS_SUMMARY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DS_MEDIA_URL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DT_PUBLISH = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TP_DIFFICULTY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NM_CATEGORY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CONTENT", x => x.ID_CONTENT);
                });

            migrationBuilder.InsertData(
                table: "TB_CONTENT",
                columns: new[] { "ID_CONTENT", "NM_CATEGORY", "TP_DIFFICULTY", "DS_MEDIA_URL", "DT_PUBLISH", "DS_SUMMARY", "NM_TITLE" },
                values: new object[,]
                {
                    { 1, "Productivity", 0, "https://youtube.com/...", new DateTime(2025, 11, 13, 13, 47, 36, 330, DateTimeKind.Local).AddTicks(4488), "Learn to manage your time effectively.", "Pomodoro Technique" },
                    { 2, "Mental Health", 0, "https://youtube.com/...", new DateTime(2025, 11, 13, 13, 47, 36, 330, DateTimeKind.Local).AddTicks(4568), "Reduce stress with breathing exercises.", "Workplace Mindfulness" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CONTENT");
        }
    }
}
