using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

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
            var ordersClient = _client.CreateClient();

            var response = await ordersClient.GetAsync("https://localhost:5001/order/getOrders");

            return $"These are my orders: {await response.Content.ReadAsStringAsync()}";
        }
    }
}
