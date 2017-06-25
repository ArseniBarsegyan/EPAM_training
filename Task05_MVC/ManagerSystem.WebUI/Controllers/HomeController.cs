using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.WebUI.Models;

namespace ManagerSystem.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IOrderService _orderService;
        private IManagerService _managerService;
        private int PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);

        public HomeController(IOrderService orderService, IManagerService managerService)
        {
            _orderService = orderService;
            _managerService = managerService;
        }

        public ActionResult Index(string manager, string product, string date, decimal? fromValue
            , decimal? toValue, int page = 1)
        {
            var pageSize = 10;
            var elementsPerPage = _orderService.GetAllOrderList().Skip((page - 1) * pageSize).Take(pageSize);
            var pageInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = elementsPerPage.Count()
            };
            var orders = elementsPerPage;

            var managers = orders.Select(x => x.ManagerName).Distinct().ToList();
            managers.Insert(0, "All");
            var products = orders.Select(x => x.ProductName).Distinct().ToList();
            products.Insert(0, "All");
            var dates = orders.Select(x => x.Date.ToString("d")).Distinct().ToList();
            dates.Insert(0, "All");

            var prices = orders.Select(x => x.Price).Distinct().ToList();
            prices.Insert(0, 0m);

            if (!string.IsNullOrEmpty(manager) && !manager.Equals("All"))
            {
                orders = orders.Where(x => x.ManagerName == manager);
            }

            if (!string.IsNullOrEmpty(product) && !product.Equals("All"))
            {
                orders = orders.Where(x => x.ProductName == product);
            }

            if (!string.IsNullOrEmpty(date) && !date.Equals("All"))
            {
                orders = orders.Where(x => x.Date.ToString("d") == date);
            }

            if (fromValue != null && fromValue != 0)
            {
                orders = orders.Where(x => x.Price >= fromValue && x.Price <= toValue);
            }

            var ordersListViewModel = new OrderListViewModel
            {
                Orders = orders,
                Managers = new SelectList(managers),
                Products = new SelectList(products),
                Dates = new SelectList(dates),
                PagingInfo = pageInfo
            };

            return View(ordersListViewModel);
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
    }
}