using mangos.services.CoupanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace mangos.services.CoupanAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                    
        }
        public DbSet<coupan> Coupans { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<coupan>().HasData(new coupan
            {
                coupanId = 1,
                coupanCode = "10OFF",
                discountAmount = 10,
                minAmount = 20
            });
            modelBuilder.Entity<coupan>().HasData(new coupan
            {
                coupanId = 2,
                coupanCode = "20OFF",
                discountAmount = 20,
                minAmount = 40
            });
        }


    }
}
    
