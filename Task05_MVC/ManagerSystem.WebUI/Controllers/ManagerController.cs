using System.Web.Mvc;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.BLL.Services;
using ManagerSystem.DAL.Repositories;

namespace ManagerSystem.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManagerController : Controller
    {
        private IManagerService _managerService = new ManagerService(new UnitOfWork("DefaultConnection"));

        public ActionResult Index()
        {
            var allManagers = _managerService.GetAllManagersList();
            return View(allManagers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ManagerDto managerDto)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = _managerService.Create(managerDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Manager");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(managerDto);
        }

        public ActionResult Edit(int id)
        {
            var managerDto = _managerService.GetManagerById(id);
            if (managerDto != null)
            {
                return View(managerDto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ManagerDto managerDto)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = _managerService.Edit(managerDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(managerDto);
        }
    }
}