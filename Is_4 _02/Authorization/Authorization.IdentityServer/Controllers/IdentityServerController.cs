using Microsoft.AspNetCore.Mvc;

namespace Authorization.IdentityServer.Controllers
{
    public class IdentityServerController : Controller
    {
        public string Index()
        {
            return "Identity server is active.";
        }
    }
}
