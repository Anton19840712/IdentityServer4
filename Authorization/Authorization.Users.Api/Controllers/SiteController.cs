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
            //retrieve to Identity4Server
            var authClient = _httpClientFactory.CreateClient();

            //за счет пакета IdentityModel = выбираешь то, что ты получил на странице по запросу localhost: 10001/.well-known/openid-configuration

            var discoveryDocument = await authClient.GetDiscoveryDocumentAsync("https://localhost:10001");

            //retrieve to orders api
            var ordersClient = _httpClientFactory.CreateClient();

            var response = await ordersClient.GetAsync("https://localhost:5001/site/getSecret");

            var message = await response.Content.ReadAsStringAsync();

            ViewBag.Message = message;

            return View();
        }
    }
}