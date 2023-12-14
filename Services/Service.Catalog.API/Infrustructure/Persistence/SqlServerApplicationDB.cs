using Microsoft.EntityFrameworkCore;
using Service.Catalog.API.Models;

namespace Service.Catalog.API.Infrustructure.Persistence
{
    public class SqlServerApplicationDB:DbContext
    {
        public SqlServerApplicationDB(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Product> Products {get; set;}
    }
}