using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Service.Idp;
using Service.Idp.Infrastructure.Persistence;
using Service.Idp.Models;

namespace Service.Idp
{
    public class DbInitializer : IDbInitializer
    {
        #region Fileds

        private readonly SqlServerApplicationDb context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbInitializer(SqlServerApplicationDb context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public DbInitializer()
        {
            
        }

        #endregion
        public void Initialize()
        {
            if (roleManager.FindByNameAsync(SD.Admin).Result is null)
            {
                roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }
            else return;

            #region  admin Shop

            ApplicationUser administratorUser = new()
            {
                UserName = "info@incodity.ir",
                Email = "info@incodity.ir",
                EmailConfirmed = true,
                PhoneNumber = "+989339966330",
                FirstName = "Ali",
                LastName = "Majid"
            };
            userManager.CreateAsync(administratorUser, "InCodity123*").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(administratorUser, SD.Admin).GetAwaiter().GetResult();

            var temp1 = userManager.AddClaimsAsync(administratorUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name,administratorUser.FirstName + " " + administratorUser.LastName),
                new Claim(JwtClaimTypes.GivenName,administratorUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName,administratorUser.LastName),
                new Claim(JwtClaimTypes.Role,SD.Admin)
            }).Result;

            #endregion

            #region Customer User

            ApplicationUser customerUser = new()
            {
                UserName = "Customer@incodity.ir",
                Email = "Customer@incodity.ir",
                EmailConfirmed = true,
                FirstName = "Masoud",
                LastName = "Talebi"
            };
            userManager.CreateAsync(customerUser, "Cus123*").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

            var temp2 = userManager.AddClaimsAsync(customerUser, new Claim[]
{
                new Claim(JwtClaimTypes.Name,customerUser.FirstName + " " + customerUser.LastName),
                new Claim(JwtClaimTypes.GivenName,customerUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName,customerUser.LastName),
                new Claim(JwtClaimTypes.Role,SD.Customer)
}).Result;

            #endregion 

        }

    }
}