using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Authorization.Users.Api.Controllers
{
    //[Route("controller")]
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _client;

        public UserController(IHttpClientFactory client)
        {
            _client = client;
        }
        //[Route("action")]
        public IActionResult Index()
        {
            return View();
        }
        //[Route("action")]
        public async Task<string> GetOrders()
        {
            //----------------------------------------------------------------------------------------------

            //INITIAL LOGIC MACHINE TO MACHINE WITHOUT IDENTITY SERVER:
            //var ordersClient = _client.CreateClient();

            //var response = await ordersClient.GetAsync("https://localhost:5001/order/getOrders");

            //return $"These are my orders: {await response.Content.ReadAsStringAsync()}";


            //----------------------------------------------------------------------------------------------

            //we need to go to identity server and get token from there
            //retrieve to Identity4Server
            var authClient = _client.CreateClient(); //create our first client
            //за счет пакета IdentityModel = выбираешь то, что ты получил на странице по запросу localhost: 10001/.well-known/openid-configuration
            var discoveryDocument = await authClient.GetDiscoveryDocumentAsync("https://localhost:10001"); //try to understand where to go then

            //create token 
            //this token is generated here: 

            var tokenResponse = await authClient.RequestClientCredentialsTokenAsync(//this authClient goes and takes parameters
                                                                                    //see the model in  namespace Authorization.IdentityServer for Configuration class
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint, // which endpoint I gonna go, TokenEndpoint, RegistrationEndpoint, AuthorizeEndpoint... Policy, etc.

                    ClientId = "client_id",
                    ClientSecret = "client_secret",
                    Scope = "OrdersAPI" // where I gonna go by this client
                }
            );

            //-------------------------------------------------

            //create second http client
            var ordersClient = _client.CreateClient();

            ordersClient.SetBearerToken(tokenResponse.AccessToken);// in token response token access and then it in auth header

            //here we get return from Authorization.Orders.Api.Controllers where public string GetSecret() [Authorize]
            var response = await ordersClient.GetAsync("https://localhost:5001/order/getOrders");

            if (!response.IsSuccessStatusCode)
            {
                return response.StatusCode.ToString() + ": unauthorized"; //Unauthorized
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
