namespace Service.Identity
{
    public static class SD
    {
        //? const

        #region Roles 
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        #endregion

        public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("InCodity","InCodity Project"),
                new ApiScope(name:"read", displayName : "Read your data"),
                new ApiScope(name:"write", displayName : "write your data"),
                new ApiScope(name:"delete", displayName : "delete your data"),
            };


        public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId="ShoppingIncodity",
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris ={"http://localhost:20236/signin-oidc"},
                PostLogoutRedirectUris={"http://localhost:20236/signout-callback-oidc"},
                AllowedScopes = new List<string>
                {
                    "InCodity",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile,


                }
            }
        };
    }
}