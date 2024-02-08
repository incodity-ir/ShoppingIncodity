using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Idp.Models;

namespace Service.Idp.Pages.Account.Register
{
    public class IndexModel : PageModel
    {
        #region Fileds
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

        #endregion

        public async Task<IActionResult> OnGet(string returnUrl)
        {
            List<string> roles = new()
            {
                SD.Admin,
                SD.Customer
            };
            ViewData["roles_message"] = roles;

            Input = new()
            {
                ReturnUrl = returnUrl
            };

            return Page();
        }

        public async Task<IActionResult> OnPost(string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    UserName = Input.Username,
                    Email = Input.Email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user,Input.Password);
                if (result.Succeeded)
                {
                    //if (!roleManager.RoleExistsAsync(Input.RoleName).GetAwaiter().GetResult())
                    //{
                    //    IdentityRole role = new IdentityRole()
                    //    {
                    //        Name = Input.RoleName
                    //    };
                    //    await roleManager.CreateAsync(role);
                    //}

                    await userManager.AddToRoleAsync(user, SD.Customer);

                    await userManager.AddClaimsAsync(user, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name,user.FirstName + " " + user.LastName),
                        new Claim(JwtClaimTypes.GivenName,user.FirstName),
                        new Claim(JwtClaimTypes.FamilyName,user.LastName),
                        new Claim(JwtClaimTypes.Email,user.Email),
                        new Claim(JwtClaimTypes.Role,SD.Customer)
                    });

                    var loginResult =
                        await signInManager.PasswordSignInAsync(user, Input.Password, false, lockoutOnFailure: true);
                    if (loginResult.Succeeded)
                    {
                        if (Url.IsLocalUrl(Input.ReturnUrl))
                        {
                            return Redirect(Input.ReturnUrl);
                        }
                        else if (string.IsNullOrEmpty(Input.ReturnUrl))
                        {
                            return Redirect("~/");
                        }
                        else
                        {
                            throw new Exception("Invalid return Url");
                        }
                        
                    }
                }
            }
            return Page();
        }
    }
}
