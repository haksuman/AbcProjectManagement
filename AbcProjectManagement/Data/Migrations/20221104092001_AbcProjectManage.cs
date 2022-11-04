using Microsoft.EntityFrameworkCore.Migrations;

namespace AbcProjectManagement.Data.Migrations
{
    public partial class AbcProjectManage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Progress",
                table: "ProjectModel",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Progress",
                table: "ProjectModel");
        }
    }
}
