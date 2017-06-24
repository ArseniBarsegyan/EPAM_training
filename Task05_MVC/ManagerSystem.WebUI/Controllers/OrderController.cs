using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.BLL.Services;
using ManagerSystem.DAL.Repositories;

namespace ManagerSystem.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private IOrderService _orderService = new OrderService(new UnitOfWork("DefaultConnection"));

        public ActionResult Index()
        {
            var allOrders = _orderService.GetAllOrderList();
            return View(allOrders);
        }
    }
}