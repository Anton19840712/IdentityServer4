using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityModel;

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

                AllowedScopes =
                {
                    "OrdersAPI",
                    "ClientMVC" //the name could be any, but should be the same as in api method.
                }
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
                new IdentityResources.OpenId()
            };
    }
}