using System.Web.Mvc;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.WebUI.Models;
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(_orderService.GetAllOrderList().ToPagedList(pageNumber, pageSize));
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
            var purchaseDto = _orderService.GetOrderDtoById(id);
            if (purchaseDto != null)
            {
                return View(purchaseDto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = _orderService.Edit(orderDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(orderDto);
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