using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceCenterWeb.Migrations
{
    public partial class SetIdentityInsert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //SET IDENTITY_INSERT TypeWorks ON;
            //GO
            //    SET IDENTITY_INSERT Clients ON;
            //GO
            //    SET IDENTITY_INSERT Manufacturers ON;
            //GO
            //    SET IDENTITY_INSERT TypeTechnics ON;
            //GO
            //    SET IDENTITY_INSERT Technics ON;
            //GO
            //    SET IDENTITY_INSERT Masters ON;
            //GO
            //    SET IDENTITY_INSERT Orders ON;
            //GO
            //migrationBuilder.Sql(@"
                
            //");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //SET IDENTITY_INSERT TypeWorks OFF;
            //GO
            //    SET IDENTITY_INSERT Clients OFF;
            //GO
            //    SET IDENTITY_INSERT Manufacturers OFF;
            //GO
            //    SET IDENTITY_INSERT TypeTechnics OFF;
            //GO
            //    SET IDENTITY_INSERT Technics OFF;
            //GO
            //    SET IDENTITY_INSERT Masters OFF;
            //GO
            //    SET IDENTITY_INSERT Orders OFF;
            //GO
            //migrationBuilder.Sql(@"
                
            //");
        }
    }
}
