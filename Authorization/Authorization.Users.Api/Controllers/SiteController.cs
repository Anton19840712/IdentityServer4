using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Authorization.Users.Api.Controllers
{
    [Route("[controller]")]
    public class SiteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SiteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }

        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("[action]")]
        public async Task<IActionResult> GetOrders()
        {
            //we need to go to identity server and get token from there
            //retrieve to Identity4Server
            var authClient = _httpClientFactory.CreateClient(); //create our first client
            //за счет пакета IdentityModel = выбираешь то, что ты получил на странице по запросу localhost: 10001/.well-known/openid-configuration
            var discoveryDocument = await authClient.GetDiscoveryDocumentAsync("https://localhost:10001"); //try to understand where to go then

            //create token 
            var tokenResponse = await authClient.RequestClientCredentialsTokenAsync(//this authClient goes and takes parameters
                                                                                    //see the model in  namespace Authorization.IdentityServer for Configuration class
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint, // which endpoint I gonna go, TokenEndpoint, RegistrationEndpoint, AuthorizeEndpoint... Policy, etc.

                    ClientId = "client_id",
                    ClientSecret = "client_secret",
                    Scope = "OrdersAPI" // where I gonna go
                }
            );

            //-------------------------------------------------

            //retrieve to orders api
            var ordersClient = _httpClientFactory.CreateClient();

            ordersClient.SetBearerToken(tokenResponse.AccessToken);// in token response token access and then it in auth header

            //here we get return from Authorization.Orders.Api.Controllers where public string GetSecret() [Authorize]
            var response = await ordersClient.GetAsync("https://localhost:5001/site/getSecret");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Message = response.StatusCode.ToString(); //Unauthorized
                return View();
            }

            var message = await response.Content.ReadAsStringAsync();

            ViewBag.Message = message;

            return View();
        }
    }
}