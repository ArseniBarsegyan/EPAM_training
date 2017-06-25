using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PagedList;

namespace ManagerSystem.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        private int PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);

        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(UserService.GetAllUsersList().ToPagedList(pageNumber, pageSize));
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

        public ActionResult Delete(string id)
        {
            var userDto = UserService.GetById(id);
            if (userDto != null)
            {
                return View(userDto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = await UserService.DeleteAsync(userDto);

                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Admin");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View();
        }
    }
}