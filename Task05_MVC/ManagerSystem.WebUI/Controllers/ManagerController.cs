using System.Linq;
using System.Web.Mvc;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.WebUI.Models;
using ManagerSystem.WebUI.Util;
using PagedList;

namespace ManagerSystem.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManagerController : Controller
    {
        private IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        public ActionResult Index(int? page)
        {
            var pageSize = ConstantStorage.PageSize;
            var pageNumber = (page ?? 1);
            return View(_managerService.GetAllManagersList().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult ManagerSearch(string name)
        {
            name = name.ToLower();
            var managers = _managerService.GetAllManagersList().Where(x => x.LastName.ToLower() == name);
            ViewBag.Managers = managers;
            return PartialView(managers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ManagerCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var managerDto = new ManagerDto {LastName = model.LastName};
                var operationDetails = _managerService.Create(managerDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Manager");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var managerDto = _managerService.GetManagerById(id);
            
            if (managerDto != null)
            {
                var model = new ManagerEditModel
                {
                    Id = managerDto.Id,
                    LastName = managerDto.LastName
                };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ManagerEditModel model)
        {
            if (ModelState.IsValid)
            {
                var managerDto = new ManagerDto
                {
                    Id = model.Id,
                    LastName = model.LastName
                };
                var operationDetails = _managerService.Edit(managerDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var purchaseDto = _managerService.GetManagerById(id);
            if (purchaseDto != null)
            {
                return View(purchaseDto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = _managerService.Delete(id);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View();
        }
    }
}