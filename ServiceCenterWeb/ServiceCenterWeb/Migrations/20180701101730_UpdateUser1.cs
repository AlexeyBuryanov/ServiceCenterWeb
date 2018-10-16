using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceCenterWeb.Migrations
{
    public partial class UpdateUser1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMaster",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMaster",
                table: "AspNetUsers");
        }
    }
}
