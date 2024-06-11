using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolokwium.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    PK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    current_weig = table.Column<int>(type: "int", nullable: false),
                    max_weight = table.Column<int>(type: "int", nullable: false),
                    money = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.PK);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    PK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    weig = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.PK);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    PK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.PK);
                });

            migrationBuilder.CreateTable(
                name: "Backpack_Slots",
                columns: table => new
                {
                    PK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_item = table.Column<int>(type: "int", nullable: false),
                    FK_character = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backpack_Slots", x => x.PK);
                    table.ForeignKey(
                        name: "FK_Backpack_Slots_Characters_FK_character",
                        column: x => x.FK_character,
                        principalTable: "Characters",
                        principalColumn: "PK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Backpack_Slots_Items_FK_item",
                        column: x => x.FK_item,
                        principalTable: "Items",
                        principalColumn: "PK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Character_Titles",
                columns: table => new
                {
                    FK_charact = table.Column<int>(type: "int", nullable: false),
                    FK_title = table.Column<int>(type: "int", nullable: false),
                    aquire_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character_Titles", x => new { x.FK_charact, x.FK_title });
                    table.ForeignKey(
                        name: "FK_Character_Titles_Characters_FK_charact",
                        column: x => x.FK_charact,
                        principalTable: "Characters",
                        principalColumn: "PK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Character_Titles_Titles_FK_title",
                        column: x => x.FK_title,
                        principalTable: "Titles",
                        principalColumn: "PK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Backpack_Slots_FK_character",
                table: "Backpack_Slots",
                column: "FK_character");

            migrationBuilder.CreateIndex(
                name: "IX_Backpack_Slots_FK_item",
                table: "Backpack_Slots",
                column: "FK_item");

            migrationBuilder.CreateIndex(
                name: "IX_Character_Titles_FK_title",
                table: "Character_Titles",
                column: "FK_title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Backpack_Slots");

            migrationBuilder.DropTable(
                name: "Character_Titles");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Titles");
        }
    }
}
