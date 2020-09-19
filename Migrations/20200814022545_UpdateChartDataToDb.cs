using Microsoft.EntityFrameworkCore.Migrations;

namespace SysExp.Migrations
{
    public partial class UpdateChartDataToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StrategyId",
                table: "ChartData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChartData_StrategyId",
                table: "ChartData",
                column: "StrategyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartData_Strategy_StrategyId",
                table: "ChartData",
                column: "StrategyId",
                principalTable: "Strategy",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartData_Strategy_StrategyId",
                table: "ChartData");

            migrationBuilder.DropIndex(
                name: "IX_ChartData_StrategyId",
                table: "ChartData");

            migrationBuilder.DropColumn(
                name: "StrategyId",
                table: "ChartData");
        }
    }
}
