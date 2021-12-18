using Authorization.IdentityServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Authorization.IdentityServer.Controllers
{
    //[Route("[controller]")] - this makes controller unavailable
    public class AuthController : Controller
    {
        //private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly UserManager<IdentityUser> _userManager;

        //public AuthController(
        //    SignInManager<IdentityUser> signInManager,
        //    UserManager<IdentityUser> userManager)
        //{
        //    _signInManager = signInManager;
        //    _userManager = userManager;
        //}
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            return View();
        }


        //[Authorize]
        //[Route("[action]")]
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var user = await _userManager.FindByNameAsync(model.UserName);

            //if (user == null)
            //{
            //    ModelState.AddModelError("UserName", "User not found");
            //    return View(model);
            //}

            //var signinResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            //if (signinResult.Succeeded)
            //{
            //    return Redirect(model.ReturnUrl);
            //}

            //ModelState.AddModelError("UserName", "Something went wrong");

            //return View(model);
        //}
    }
}
