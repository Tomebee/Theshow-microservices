using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace TheShow.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("api-core")
            {
                Scopes = new List<string>()
                {
                    "api-core.read", "api-core.write"
                },
                ApiSecrets = new List<Secret>()
                {
                    new Secret("b8d20c09-2cca-47a5-9145-81229ce3ba47".Sha256())
                }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api-core.read","TheShow.Core API queries"),
                new ApiScope("api-core.write","TheShow.Core API commands"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {

                // Frontend React app
                new Client
                {
                    ClientId = "reactapp",
                    ClientName = "Frontend react.js app",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =           { "http://theshow.local/oauth2-callback" },
                    PostLogoutRedirectUris = { "http://theshow.local" },
                    AllowedCorsOrigins =     { "http://theshow.local" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api-core.read",
                        "api-core.write"
                    },
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    //AllowedGrantTypes = GrantTypes.Code,

                    //RedirectUris = { "http://localhost:3000/signin-oidc" },
                    //FrontChannelLogoutUri = "http://localhost:3000/signout-oidc",
                    //PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    //AllowOfflineAccess = true,
                    //AllowedScopes = { "openid", "profile", "scope2" }
                },
                new Client
                {
                    ClientId = "postman",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new []{ new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                }
            };
    }
}
