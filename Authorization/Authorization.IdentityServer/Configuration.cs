using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;

namespace Authorization.IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            //1вот этому клиенту
            new Client
            {
                ClientId = "client_id",
                ClientSecrets = {new Secret("client_secret".ToSha256())},

                //по какому принципу будет авторизовываться клиент
                AllowedGrantTypes = GrantTypes.ClientCredentials, // по наиболее примитивному ClientCredentials
                //добавили только скоуп клиента, работающего с ордерами

                AllowedScopes =
                {
                    "OrdersAPI"
                }
            },
            //1вот этому клиенту
            new Client
            {
                ClientId = "client_id_mvc",
                ClientSecrets = {new Secret("client_secret_mvc".ToSha256())},

                //по какому принципу будет авторизовываться клиент
                AllowedGrantTypes = GrantTypes.Code, // по наиболее примитивному ClientCredentials// Grant type - it s a type of interaction of server4 and clients
                //добавили только скоуп клиента, работающего с ордерами

                AllowedScopes = // where this client can go
                {
                    "OrdersAPI",
                    //"UsersAPI", //the name could be any, but should be the same as in api method.
                    //"openid",
                    //"profile",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },

                RedirectUris = {"https://localhost:2001/signin-oidc"}, 

                RequireConsent = false // this allows not to load page http://localhost:10001/consent? - this page appears after login, something like user agreement page
            }
        };

        internal static IEnumerable<ApiResource> GetApiResources() => //2можно ходить на этот ресурс
                    new List<ApiResource> {

                new ApiResource("OrdersAPI")
                };

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource> {
                //new IdentityResources.Address(),
                //new IdentityResources.Phone(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
    }
}