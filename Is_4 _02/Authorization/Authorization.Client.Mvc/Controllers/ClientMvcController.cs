using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Client.Mvc.Controllers
{
    public class ClientMvcController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Client()
        {
            return View();
        }
    }
}
