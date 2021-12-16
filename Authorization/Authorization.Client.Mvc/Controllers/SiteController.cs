using Authorization.Client.Mvc.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Authorization.Client.Mvc.Controllers
{
    [Route("[controller]")]
    public class SiteController : Controller
    {
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("[action]")]
        public IActionResult Secret()
        {
            //var jsonToken = await HttpContext.GetTokenAsync("access_token");

            //var token = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(jsonToken);

            var model = new ClaimManager(HttpContext, User);

            return View(model);
        }
    }
}
