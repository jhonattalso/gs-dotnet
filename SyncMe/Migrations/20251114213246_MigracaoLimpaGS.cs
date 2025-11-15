using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SyncMe.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoLimpaGS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CATEGORY",
                columns: table => new
                {
                    ID_CATEGORY = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_NAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CATEGORY", x => x.ID_CATEGORY);
                });

            migrationBuilder.CreateTable(
                name: "TB_TRACK",
                columns: table => new
                {
                    ID_TRACK = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_TITLE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DS_DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TRACK", x => x.ID_TRACK);
                });

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
                    ID_CATEGORY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_TRACK = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CONTENT", x => x.ID_CONTENT);
                    table.ForeignKey(
                        name: "FK_TB_CONTENT_TB_CATEGORY_ID_CATEGORY",
                        column: x => x.ID_CATEGORY,
                        principalTable: "TB_CATEGORY",
                        principalColumn: "ID_CATEGORY",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_CONTENT_TB_TRACK_ID_TRACK",
                        column: x => x.ID_TRACK,
                        principalTable: "TB_TRACK",
                        principalColumn: "ID_TRACK");
                });

            migrationBuilder.InsertData(
                table: "TB_CATEGORY",
                columns: new[] { "ID_CATEGORY", "NM_NAME" },
                values: new object[,]
                {
                    { 1, "Saúde Mental" },
                    { 2, "Produtividade" },
                    { 3, "Soft Skills" }
                });

            migrationBuilder.InsertData(
                table: "TB_TRACK",
                columns: new[] { "ID_TRACK", "DS_DESCRIPTION", "NM_TITLE" },
                values: new object[,]
                {
                    { 1, "Guia prático para reduzir a ansiedade no trabalho.", "Semana Sem Stress" },
                    { 2, "Como gerir equipes remotas.", "Liderança 4.0" }
                });

            migrationBuilder.InsertData(
                table: "TB_CONTENT",
                columns: new[] { "ID_CONTENT", "ID_CATEGORY", "TP_DIFFICULTY", "DS_MEDIA_URL", "DT_PUBLISH", "DS_SUMMARY", "NM_TITLE", "ID_TRACK" },
                values: new object[,]
                {
                    { 1, 2, 0, "https://www.youtube.com/watch?v=123", new DateTime(2025, 11, 14, 18, 32, 45, 250, DateTimeKind.Local).AddTicks(7923), "Aprenda a gerenciar seu tempo com pausas estratégicas.", "Técnica Pomodoro", 1 },
                    { 2, 1, 0, "https://www.youtube.com/watch?v=456", new DateTime(2025, 11, 14, 18, 32, 45, 250, DateTimeKind.Local).AddTicks(8409), "Exercícios rápidos de respiração.", "Mindfulness no Trabalho", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CONTENT_ID_CATEGORY",
                table: "TB_CONTENT",
                column: "ID_CATEGORY");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CONTENT_ID_TRACK",
                table: "TB_CONTENT",
                column: "ID_TRACK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CONTENT");

            migrationBuilder.DropTable(
                name: "TB_CATEGORY");

            migrationBuilder.DropTable(
                name: "TB_TRACK");
        }
    }
}
