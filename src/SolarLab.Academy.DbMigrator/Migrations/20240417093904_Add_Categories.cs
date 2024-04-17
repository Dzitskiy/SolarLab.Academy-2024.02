using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SolarLab.Academy.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class Add_Categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("10946739-9f9e-4958-81f8-66c52dba6bea"), "Недвижимость", null },
                    { new Guid("2e51c519-79c5-4468-a1ee-478ca0c2b8ff"), "Транспорт", null },
                    { new Guid("3aad0e0f-c16e-47e7-a223-b638cc5a48f6"), "Одежда", null },
                    { new Guid("3b0beb72-e73b-473d-9804-720e656cb243"), "Детская одежда", new Guid("3aad0e0f-c16e-47e7-a223-b638cc5a48f6") },
                    { new Guid("6737fa84-51b2-4e20-8603-02be4dcdc9ac"), "Квартиры", new Guid("10946739-9f9e-4958-81f8-66c52dba6bea") },
                    { new Guid("9797b747-3432-4f37-8725-d97a3978ab49"), "Одежда для взрослых", new Guid("3aad0e0f-c16e-47e7-a223-b638cc5a48f6") },
                    { new Guid("bb805e10-d6c0-4ce5-b8ae-2d2554b516b9"), "Велосипеды", new Guid("2e51c519-79c5-4468-a1ee-478ca0c2b8ff") },
                    { new Guid("cb66c03d-fcf8-4e9e-9a37-bfe63c07d5fc"), "Автомобили", new Guid("2e51c519-79c5-4468-a1ee-478ca0c2b8ff") },
                    { new Guid("ec698cde-0525-42c5-8b10-37179adb3540"), "Дома", new Guid("10946739-9f9e-4958-81f8-66c52dba6bea") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
