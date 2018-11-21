using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CalorieCounter.Api.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Kcal = table.Column<int>(nullable: false),
                    Carbohydrates = table.Column<double>(nullable: false),
                    Proteins = table.Column<double>(nullable: false),
                    Fats = table.Column<double>(nullable: false),
                    ServeSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
