using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Authorization.Users.Api.Controllers
{
    [Route("[controller]")]
    public class SiteController : Controller
    {
        private IHttpClientFactory _httpClientFactory;

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
            var ordersClient = _httpClientFactory.CreateClient();

            var response = await ordersClient.GetAsync("https://localhost:5001/site/getSecret");

            var message = await response.Content.ReadAsStringAsync();

            ViewBag.Message = message;

            return View();
        }
    }
}