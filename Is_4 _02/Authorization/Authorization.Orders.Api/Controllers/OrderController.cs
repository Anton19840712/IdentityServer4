using Microsoft.AspNetCore.Mvc;

namespace Authorization.Orders.Api.Controllers
{
    //[Route("controller")]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //[Route("action")]
        public string GetOrders()
        {
            return "orders from the order controller were returned successfully.";
        }
    }
}

