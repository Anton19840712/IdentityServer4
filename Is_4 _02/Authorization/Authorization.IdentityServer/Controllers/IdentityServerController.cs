using Microsoft.AspNetCore.Mvc;

namespace Authorization.IdentityServer.Controllers
{
    public class IdentityServerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
