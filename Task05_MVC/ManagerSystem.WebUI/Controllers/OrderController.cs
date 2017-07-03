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
    public class OrderController : Controller
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Index(int? page)
        {
            var pageSize = ConstantStorage.PageSize;
            var pageNumber = (page ?? 1);
            return View(_orderService.GetAllOrderList().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult OrderSearch(string name)
        {
            name = name.ToLower();
            var orders = _orderService.GetAllOrderList().Where(x => x.ManagerName.ToLower() == name);
            ViewBag.Orders = orders;
            return PartialView(orders);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OrderCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var orderDto = new OrderDto
                {
                    ClientName = model.ClientName,
                    ManagerName = model.ManagerName,
                    Price = model.Price,
                    ProductName = model.ProductName,
                    Date = model.Date
                };

                var operationDetails = _orderService.Create(orderDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Order");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var orderDto = _orderService.GetOrderDtoById(id);
            if (orderDto != null)
            {
                var model = new OrderEditModel
                {
                    Id = orderDto.Id,
                    ManagerName = orderDto.ManagerName,
                    ProductName = orderDto.ProductName,
                    Price = orderDto.Price,
                    Date = orderDto.Date,
                    ClientName = orderDto.ClientName
                };

                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(OrderEditModel model)
        {
            if (ModelState.IsValid)
            {
                var orderDto = new OrderDto
                {
                    Id = model.Id,
                    ManagerName = model.ManagerName,
                    ProductName = model.ProductName,
                    Price = model.Price,
                    Date = model.Date,
                    ClientName = model.ClientName
                };

                var operationDetails = _orderService.Edit(orderDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var purchaseDto = _orderService.GetOrderDtoById(id);
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
                var operationDetails = _orderService.Delete(id);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View();
        }
    }
}