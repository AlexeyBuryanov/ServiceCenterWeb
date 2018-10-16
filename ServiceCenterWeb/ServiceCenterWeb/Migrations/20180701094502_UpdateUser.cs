using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceCenterWeb.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientGuid",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientGuid",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
