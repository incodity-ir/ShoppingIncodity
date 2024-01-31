namespace Service.Identity.Infrastructure.Persistence
{
    public class SqlServerApplicationDb:IdentityDbContext<ApplicationUser>
    {
        
        public SqlServerApplicationDb(DbContextOptions<SqlServerApplicationDb> options):base(options)
        {
            
        }
        
    }
}