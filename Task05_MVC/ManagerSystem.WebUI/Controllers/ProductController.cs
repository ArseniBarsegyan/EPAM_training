using System.Web.Mvc;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.BLL.Services;
using ManagerSystem.DAL.Repositories;

namespace ManagerSystem.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private IProductService _productService = new ProductService(new UnitOfWork("DefaultConnection"));

        public ActionResult Index()
        {
            var allProducts = _productService.GetAllProductsList();
            return View(allProducts);
        }
    }
}