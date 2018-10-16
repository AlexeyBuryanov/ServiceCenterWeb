using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ServiceCenterWeb.Models.Database
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<TypeWork> TypeWorks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<TypeTechnic> TypeTechnics { get; set; }
        public DbSet<Technic> Technics { get; set; }
        public DbSet<Master> Masters { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
