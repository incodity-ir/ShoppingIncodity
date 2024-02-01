using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Service.Idp.Models;

namespace Service.Idp.Infrastructure.Persistence
{
    public class SqlServerApplicationDb:IdentityDbContext<ApplicationUser>
    {
        
        public SqlServerApplicationDb(DbContextOptions<SqlServerApplicationDb> options):base(options)
        {
            
        }
        
    }
}