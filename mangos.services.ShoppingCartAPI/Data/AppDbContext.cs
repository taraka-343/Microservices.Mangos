using mangos.services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace mangos.services.ShoppingCartAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                    
        }

        public DbSet<CartHeader> cartHeaders { get; set; }
        public DbSet<CartDetails> cartDeatails { get; set; }



    }
}
    
