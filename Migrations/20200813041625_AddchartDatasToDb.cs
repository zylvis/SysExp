using Microsoft.EntityFrameworkCore.Migrations;

namespace SysExp.Migrations
{
    public partial class AddchartDatasToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartData_Category_CategoryId",
                table: "ChartData");

            migrationBuilder.DropForeignKey(
                name: "FK_ChartData_Portfolio_PortfolioId",
                table: "ChartData");

            migrationBuilder.DropForeignKey(
                name: "FK_ChartData_Strategy_StrategyId",
                table: "ChartData");

            migrationBuilder.DropIndex(
                name: "IX_ChartData_CategoryId",
                table: "ChartData");

            migrationBuilder.DropIndex(
                name: "IX_ChartData_PortfolioId",
                table: "ChartData");

            migrationBuilder.DropIndex(
                name: "IX_ChartData_StrategyId",
                table: "ChartData");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ChartData");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "ChartData");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "ChartData");

            migrationBuilder.DropColumn(
                name: "StrategyId",
                table: "ChartData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ChartData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "ChartData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "ChartData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StrategyId",
                table: "ChartData",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ChartData_Category_CategoryId",
                table: "ChartData",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ChartData_Portfolio_PortfolioId",
                table: "ChartData",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ChartData_Strategy_StrategyId",
                table: "ChartData",
                column: "StrategyId",
                principalTable: "Strategy",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
