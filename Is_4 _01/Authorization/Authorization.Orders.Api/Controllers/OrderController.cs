using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

