using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Service.Idp.Models;

namespace Service.Idp.Services
{
    public class ProfileService : IProfileService
    {
        #region Fileds

        private readonly IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ProfileService(IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        #endregion
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await userManager.FindByIdAsync(sub);
            ClaimsPrincipal userClaims = await userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(w => context.RequestedClaimTypes.Contains(w.Type)).ToList();
            claims.Add(new Claim(JwtClaimTypes.GivenName,user.FirstName));
            claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));


            if (userManager.SupportsUserRole)
            {
                IList<string> roles = await userManager.GetRolesAsync(user);
                foreach (string rolename in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, rolename));
                    if (roleManager.SupportsRoleClaims)
                    {
                        IdentityRole role = await roleManager.FindByNameAsync(rolename);
                        if(role is not null)
                            claims.AddRange(await roleManager.GetClaimsAsync(role));
                    }
                }
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
