using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Authorization.IdentityServer
{
    //Create clients on this server
    public static class Configuration
    {
        public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client_id",

                ClientSecrets = {new Secret("client_secret".ToSha256())},

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                AllowedScopes =
                {
                    "OrdersAPI"
                }
            },

            new Client
            {
                ClientId = "client_id_mvc",
                ClientSecrets = {new Secret("client_secret_mvc".ToSha256())},
                AllowedGrantTypes = GrantTypes.Code, 
                AllowedScopes = 
                {
                    "OrdersAPI",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },

                RedirectUris = {"https://localhost:2001/signin-oidc"},

                RequireConsent = false,

                //AlwaysIncludeUserClaimsInIdToken = true
                //comment, because add in startup 
                //config.GetClaimsFromUserInfoEndpoint = true;
                //this adds claims from profile service only in access token.
        }

     };

    internal static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
            new ApiResource("OrdersAPI")
        };

    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource> {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}