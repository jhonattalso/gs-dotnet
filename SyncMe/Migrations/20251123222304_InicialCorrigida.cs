using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SyncMe.Migrations
{
    /// <inheritdoc />
    public partial class InicialCorrigida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_GS_CATEGORY",
                columns: table => new
                {
                    ID_CATEGORY = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_NAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GS_CATEGORY", x => x.ID_CATEGORY);
                });

            migrationBuilder.CreateTable(
                name: "TB_GS_TRACK",
                columns: table => new
                {
                    ID_TRACK = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_TITLE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DS_DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GS_TRACK", x => x.ID_TRACK);
                });

            migrationBuilder.CreateTable(
                name: "TB_GS_CONTENT",
                columns: table => new
                {
                    ID_CONTENT = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_TITLE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DS_SUMMARY = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: false),
                    DS_ARTICLE_BODY = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    DS_COVER_IMAGE_URL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DS_MEDIA_URL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DT_PUBLISH = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TP_DIFFICULTY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_CATEGORY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_TRACK = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GS_CONTENT", x => x.ID_CONTENT);
                    table.ForeignKey(
                        name: "FK_TB_GS_CONTENT_TB_GS_CATEGORY_ID_CATEGORY",
                        column: x => x.ID_CATEGORY,
                        principalTable: "TB_GS_CATEGORY",
                        principalColumn: "ID_CATEGORY",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_GS_CONTENT_TB_GS_TRACK_ID_TRACK",
                        column: x => x.ID_TRACK,
                        principalTable: "TB_GS_TRACK",
                        principalColumn: "ID_TRACK");
                });

            migrationBuilder.InsertData(
                table: "TB_GS_CATEGORY",
                columns: new[] { "ID_CATEGORY", "NM_NAME" },
                values: new object[,]
                {
                    { 1, "Saúde Mental" },
                    { 2, "Produtividade" },
                    { 3, "Soft Skills" }
                });

            migrationBuilder.InsertData(
                table: "TB_GS_TRACK",
                columns: new[] { "ID_TRACK", "DS_DESCRIPTION", "NM_TITLE" },
                values: new object[,]
                {
                    { 1, "Guia prático para reduzir a ansiedade no trabalho.", "Semana Sem Stress" },
                    { 2, "Como gerir equipes remotas.", "Liderança 4.0" }
                });

            migrationBuilder.InsertData(
                table: "TB_GS_CONTENT",
                columns: new[] { "ID_CONTENT", "DS_ARTICLE_BODY", "ID_CATEGORY", "DS_COVER_IMAGE_URL", "TP_DIFFICULTY", "DS_MEDIA_URL", "DT_PUBLISH", "DS_SUMMARY", "NM_TITLE", "ID_TRACK" },
                values: new object[,]
                {
                    { 100, null, 2, null, 0, "https://www.youtube.com/watch?v=hfxfJ7Qa4sg&t=3s", new DateTime(2025, 11, 23, 19, 23, 3, 694, DateTimeKind.Local).AddTicks(2099), "Aprenda a gerenciar seu tempo com pausas estratégicas.", "Técnica Pomodoro", 1 },
                    { 101, null, 1, null, 0, "https://www.youtube.com/watch?v=mLOCYir6bnI", new DateTime(2025, 11, 23, 19, 23, 3, 694, DateTimeKind.Local).AddTicks(2408), "Exercícios rápidos de respiração.", "Mindfulness no Trabalho", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_GS_CONTENT_ID_CATEGORY",
                table: "TB_GS_CONTENT",
                column: "ID_CATEGORY");

            migrationBuilder.CreateIndex(
                name: "IX_TB_GS_CONTENT_ID_TRACK",
                table: "TB_GS_CONTENT",
                column: "ID_TRACK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_GS_CONTENT");

            migrationBuilder.DropTable(
                name: "TB_GS_CATEGORY");

            migrationBuilder.DropTable(
                name: "TB_GS_TRACK");
        }
    }
}
