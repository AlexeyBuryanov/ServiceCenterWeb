using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceCenterWeb.Migrations
{
    public partial class UpdateTechnicsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeTechnicId",
                table: "Technics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypeTechnic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTechnic", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Technics_TypeTechnicId",
                table: "Technics",
                column: "TypeTechnicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technics_TypeTechnic_TypeTechnicId",
                table: "Technics",
                column: "TypeTechnicId",
                principalTable: "TypeTechnic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technics_TypeTechnic_TypeTechnicId",
                table: "Technics");

            migrationBuilder.DropTable(
                name: "TypeTechnic");

            migrationBuilder.DropIndex(
                name: "IX_Technics_TypeTechnicId",
                table: "Technics");

            migrationBuilder.DropColumn(
                name: "TypeTechnicId",
                table: "Technics");
        }
    }
}
