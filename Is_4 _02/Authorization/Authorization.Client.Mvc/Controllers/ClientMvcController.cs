using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Authorization.Client.Mvc.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Client.Mvc.Controllers
{
    public class ClientMvcController : Controller
    {
        private readonly IHttpClientFactory _client;

        public ClientMvcController(IHttpClientFactory client)
        {
            _client = client;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> Client()
        {
            //var jsonToken = await HttpContext.GetTokenAsync("access_token");

            //var token = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(jsonToken);


            var model = new ClaimManager(HttpContext, User);

            try
            {
                var client = _client.CreateClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.AccessToken);

                var stringAsync = await client.GetStringAsync("https://localhost:5001/order/getOrders");

                ViewBag.Message = stringAsync;
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }

            return View(model);

        }

        [Authorize(Policy = "HasDateOfBirth")]// You should manage policy in startup of the current client for instance like
        //[Route("[action]")]//builder.RequireClaim(ClaimTypes.DateOfBirth)
        public IActionResult Test()
        {

            var model = new ClaimManager(HttpContext, User);

            return View(model);

        }

        [Authorize(Policy = "OlderThan")]// You should manage policy in startup of the current client for instance like
        //[Route("[action]")]//builder.RequireClaim(ClaimTypes.DateOfBirth)
        public IActionResult Element()
        {

            var model = new ClaimManager(HttpContext, User);

            return View(model);

        }
    }
}
