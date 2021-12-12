using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Orders.Api.Controllers
{
    public class SiteController : Controller
    {
        public IActionResult Index() => View();

        public string GetSecrets() => "Secret string from Orders API";
    }
}
