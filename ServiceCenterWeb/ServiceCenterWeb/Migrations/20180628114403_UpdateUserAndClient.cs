using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceCenterWeb.Migrations
{
    public partial class UpdateUserAndClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientGuid",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClientGuid",
                table: "AspNetUsers");
        }
    }
}
