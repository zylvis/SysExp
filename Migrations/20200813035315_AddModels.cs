using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SysExp.Migrations
{
    public partial class AddModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Strategy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Picture = table.Column<byte[]>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strategy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Strategy_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Portfolio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    StrategyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portfolio_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Portfolio_Strategy_StrategyId",
                        column: x => x.StrategyId,
                        principalTable: "Strategy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ChartData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    DateToString = table.Column<string>(nullable: true),
                    Equity = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    StrategyId = table.Column<int>(nullable: false),
                    PortfolioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChartData_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ChartData_Portfolio_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ChartData_Strategy_StrategyId",
                        column: x => x.StrategyId,
                        principalTable: "Strategy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChartData_CategoryId",
                table: "ChartData",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartData_PortfolioId",
                table: "ChartData",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartData_StrategyId",
                table: "ChartData",
                column: "StrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_CategoryId",
                table: "Portfolio",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_StrategyId",
                table: "Portfolio",
                column: "StrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_Strategy_CategoryId",
                table: "Strategy",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChartData");

            migrationBuilder.DropTable(
                name: "Portfolio");

            migrationBuilder.DropTable(
                name: "Strategy");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
