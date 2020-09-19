using Microsoft.EntityFrameworkCore.Migrations;

namespace SysExp.Migrations
{
    public partial class updatessmth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Description_Category_CategoryId",
                table: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Description",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Description_Category_CategoryId",
                table: "Description",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Description_Category_CategoryId",
                table: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Description",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Description_Category_CategoryId",
                table: "Description",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
