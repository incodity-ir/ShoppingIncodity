using Duende.IdentityServer.Models;

namespace Service.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("shopincodity","Shop Incodity"),
                new ApiScope("read",displayName:"just read data"),
                new ApiScope("write",displayName:"just write data"),
                new ApiScope("delete",displayName:"just delete data")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "shopIncodity",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "http://localhost:45824/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:45824/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        "openid","email","profile","shopincodity"
                    }
                }
            };
    }
}
