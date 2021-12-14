using Authorization.IdentityServer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.IdentityServer.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        public AuthController()
        {
            
        }

        [Route("action")]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(LoginViewModel model)
        {
            return View();
        }
    }
}
