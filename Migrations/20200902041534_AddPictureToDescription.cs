using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SysExp.Migrations
{
    public partial class AddPictureToDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Description",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Description");
        }
    }
}
