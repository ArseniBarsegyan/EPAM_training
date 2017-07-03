using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.WebUI.Models;
using ManagerSystem.WebUI.Util;
using PagedList;

namespace ManagerSystem.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IOrderService _orderService;
        private IManagerService _managerService;

        public HomeController(IOrderService orderService, IManagerService managerService)
        {
            _orderService = orderService;
            _managerService = managerService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Charts()
        {
            return View();
        }

        public ActionResult Orders(string manager, string product, string date, decimal? fromValue
            , decimal? toValue, int? page)
        {
            var pageSize = ConstantStorage.PageSize;
            int pageNumber = (page ?? 1);
            var orders = _orderService.GetAllOrderList();

            var managers = orders.Select(x => x.ManagerName).Distinct().ToList();
            managers.Insert(0, ConstantStorage.AllRecordsInDropDownListValue);
            var products = orders.Select(x => x.ProductName).Distinct().ToList();
            products.Insert(0, ConstantStorage.AllRecordsInDropDownListValue);
            var dates = orders.Select(x => x.Date.ToString("d")).Distinct().ToList();
            dates.Insert(0, ConstantStorage.AllRecordsInDropDownListValue);

            var prices = orders.Select(x => x.Price).Distinct().ToList();
            prices.Insert(0, 0m);

            if (!string.IsNullOrEmpty(manager) && !manager.Equals(ConstantStorage.AllRecordsInDropDownListValue))
            {
                orders = orders.Where(x => x.ManagerName == manager);
            }

            if (!string.IsNullOrEmpty(product) && !product.Equals(ConstantStorage.AllRecordsInDropDownListValue))
            {
                orders = orders.Where(x => x.ProductName == product);
            }

            if (!string.IsNullOrEmpty(date) && !date.Equals(ConstantStorage.AllRecordsInDropDownListValue))
            {
                orders = orders.Where(x => x.Date.ToString("d") == date);
            }

            if (fromValue != null && fromValue != 0 && toValue != null && toValue != 0)
            {
                orders = orders.Where(x => x.Price >= fromValue && x.Price <= toValue);
            }

            var ordersListViewModel = new OrderListViewModel
            {
                Orders = orders.ToPagedList(pageNumber, pageSize),
                Managers = new SelectList(managers),
                Products = new SelectList(products),
                Dates = new SelectList(dates),
            };

            return View(ordersListViewModel);
        }

        public ActionResult OrdersSummary(IEnumerable<OrderDto> orderDtos, int? page)
        {
            var pageSize = ConstantStorage.PageSize;
            int pageNumber = (page ?? 1);

            return PartialView(orderDtos.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetManagersData()
        {
            var allOrders = _orderService.GetAllOrderList();

            var managersViewsModels = _managerService.GetAllManagersList()
                .Select(x => x.LastName)
                .Select(name => new ManagerViewModel
                {
                    Name = name
                }).ToList();

            foreach (var t in managersViewsModels)
            {
                foreach (var order in allOrders)
                {
                    if (t.Name != order.ManagerName) continue;
                    t.OrdersCount++;
                    t.TotalPrice += order.Price;
                }
            }
            return Json(managersViewsModels, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}