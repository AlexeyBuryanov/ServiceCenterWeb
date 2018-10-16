using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceCenterWeb.Migrations
{
    public partial class CheckpointModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technics_TypeTechnic_TypeTechnicId",
                table: "Technics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeTechnic",
                table: "TypeTechnic");

            migrationBuilder.RenameTable(
                name: "TypeTechnic",
                newName: "TypeTechnics");

            migrationBuilder.AddColumn<int>(
                name: "TypeWorkId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeTechnics",
                table: "TypeTechnics",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TypeWorks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeWorks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TypeWorkId",
                table: "Orders",
                column: "TypeWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_TypeWorks_TypeWorkId",
                table: "Orders",
                column: "TypeWorkId",
                principalTable: "TypeWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Technics_TypeTechnics_TypeTechnicId",
                table: "Technics",
                column: "TypeTechnicId",
                principalTable: "TypeTechnics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_TypeWorks_TypeWorkId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Technics_TypeTechnics_TypeTechnicId",
                table: "Technics");

            migrationBuilder.DropTable(
                name: "TypeWorks");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TypeWorkId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeTechnics",
                table: "TypeTechnics");

            migrationBuilder.DropColumn(
                name: "TypeWorkId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "TypeTechnics",
                newName: "TypeTechnic");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeTechnic",
                table: "TypeTechnic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Technics_TypeTechnic_TypeTechnicId",
                table: "Technics",
                column: "TypeTechnicId",
                principalTable: "TypeTechnic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
