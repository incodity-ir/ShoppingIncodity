using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Service.Identity.DbContexts;
using Service.Identity.Models;

namespace Service.Identity
{
    public class DbInitializer:IDbInitializer
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void Initialize()
        {
            if (roleManager.FindByNameAsync(SD.Admin).Result is null)
            {
                roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }else return;

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "info@incodity.ir",
                Email = "info@incodity.ir",
                EmailConfirmed = true,
                PhoneNumber = "+989339966330",
                FirstName = "Ali",
                LastName = "Majidi"
            };

            userManager.CreateAsync(adminUser,"Incodity123*").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

            var temp1 = userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Admin)

            }).Result;


            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "masoud@incodity.ir",
                Email = "masoud@incodity.ir",
                EmailConfirmed = true,
                PhoneNumber = "+989339966330",
                FirstName = "Maousd",
                LastName = "Talebi"
            };

            userManager.CreateAsync(customerUser, "Inc123*").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

            var temp2 = userManager.AddClaimsAsync(customerUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, customerUser.FirstName + " " + adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Customer)

            }).Result;
        }


    }
}
