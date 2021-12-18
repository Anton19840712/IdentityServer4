using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Authorization.Client.Mvc.ViewModels;
using Microsoft.AspNetCore.Authentication;
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
        [Route("[action]")]
        public async Task<IActionResult> ClientAsync()
        {
            var jsonToken = await HttpContext.GetTokenAsync("access_token");

            var token = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(jsonToken);

            var model = new ClaimManager(HttpContext, User);

            return View(model);

        }
    }
}
