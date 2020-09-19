using Microsoft.EntityFrameworkCore.Migrations;

namespace SysExp.Migrations
{
    public partial class AddModelsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartData_Portfolio_PortfolioId",
                table: "ChartData");

            migrationBuilder.DropForeignKey(
                name: "FK_ChartData_Strategy_StrategyId",
                table: "ChartData");

            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_Category_CategoryId",
                table: "Portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_Strategy_StrategyId",
                table: "Portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_Strategy_Category_CategoryId",
                table: "Strategy");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartData_Portfolio_PortfolioId",
                table: "ChartData",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChartData_Strategy_StrategyId",
                table: "ChartData",
                column: "StrategyId",
                principalTable: "Strategy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_Category_CategoryId",
                table: "Portfolio",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_Strategy_StrategyId",
                table: "Portfolio",
                column: "StrategyId",
                principalTable: "Strategy",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Strategy_Category_CategoryId",
                table: "Strategy",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartData_Portfolio_PortfolioId",
                table: "ChartData");

            migrationBuilder.DropForeignKey(
                name: "FK_ChartData_Strategy_StrategyId",
                table: "ChartData");

            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_Category_CategoryId",
                table: "Portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_Strategy_StrategyId",
                table: "Portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_Strategy_Category_CategoryId",
                table: "Strategy");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartData_Portfolio_PortfolioId",
                table: "ChartData",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartData_Strategy_StrategyId",
                table: "ChartData",
                column: "StrategyId",
                principalTable: "Strategy",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_Category_CategoryId",
                table: "Portfolio",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_Strategy_StrategyId",
                table: "Portfolio",
                column: "StrategyId",
                principalTable: "Strategy",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Strategy_Category_CategoryId",
                table: "Strategy",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
