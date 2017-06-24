using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.WebUI.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ManagerSystem.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                var userDto = new UserDto { Name = model.Name, Password = model.Password };
                var claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDto
            {
                Name = "Admin",
                Password = "Admin",
                Role = "admin"
            }, new List<string> { "user", "admin" });
        }
    }
}