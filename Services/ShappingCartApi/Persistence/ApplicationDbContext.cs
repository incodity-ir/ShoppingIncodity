using Microsoft.EntityFrameworkCore;
using ShappingCartApi.Models;
using ShappingCartApi.Persistence.MapConfig;

namespace ShappingCartApi.Persistence
{
    public class ApplicationDbContext:DbContext,IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
    }
}
