using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ManagerSystem.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Index()
        {
            return View(UserService.GetAllUsersList());
        }

        public ActionResult Edit(string id)
        {
            var userDto = UserService.GetById(id);
            if (userDto != null)
            {
                return View(userDto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = await UserService.EditAsync(userDto);

                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Admin");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View();
        }
    }
}