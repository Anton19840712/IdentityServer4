using Microsoft.AspNetCore.Mvc;

namespace Authorization.Server.Controllers
{
    public class ServerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
