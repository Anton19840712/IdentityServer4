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
            new Client //1вот этому клиенту
            {
                ClientId = "client_id",
                ClientSecrets = { new Secret("client_secret".ToSha256()) },

                //по какому принципу будет авторизовываться клиент
                AllowedGrantTypes = GrantTypes.ClientCredentials, // по наиболее примитивному ClientCredentials
                //добавили только скоуп клиента, работающего с ордерами

                AllowedScopes =
                {
                    "OrdersAPI"
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