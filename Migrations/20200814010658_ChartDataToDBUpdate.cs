using Microsoft.EntityFrameworkCore.Migrations;

namespace SysExp.Migrations
{
    public partial class ChartDataToDBUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "ChartData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChartData_PortfolioId",
                table: "ChartData",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartData_Portfolio_PortfolioId",
                table: "ChartData",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartData_Portfolio_PortfolioId",
                table: "ChartData");

            migrationBuilder.DropIndex(
                name: "IX_ChartData_PortfolioId",
                table: "ChartData");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "ChartData");
        }
    }
}
