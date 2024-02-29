using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Shopping.Web
{
    public static class Extensions
    {
        public static string GetUserId(this HttpContext context)
        {
            var claimsIdentity = context.User.Identity as ClaimsIdentity;
            var userIdValue = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            return userIdValue;
        }

        public async static Task<string> GetToken(this HttpContext context)
        {
            return await context.GetTokenAsync("access_token");
        }
    }
}
