using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.WebUI.Util;
using Microsoft.AspNet.Identity.Owin;
using PagedList;

namespace ManagerSystem.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        public ActionResult Index(int? page)
        {
            var pageSize = ConstantStorage.pageSize;
            var pageNumber = (page ?? 1);
            return View(UserService.GetAllUsersList().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult UserSearch(string name)
        {
            name = name.ToLower();
            var users = UserService.GetAllUsersList().Where(x => x.Name.ToLower() == name);
            ViewBag.Users = users;
            return PartialView(users);
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