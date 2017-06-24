using System;
using System.Configuration;
using System.Web.Mvc;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.BLL.Services;
using ManagerSystem.DAL.Repositories;

namespace ManagerSystem.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IOrderService _orderService = new OrderService(new UnitOfWork("DefaultConnection"));
        private int PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);

        public ActionResult Index(string manager, string product, string date, decimal? fromValue
            , decimal? toValue, int page = 1)
        {
            return View();
        }
    }
}