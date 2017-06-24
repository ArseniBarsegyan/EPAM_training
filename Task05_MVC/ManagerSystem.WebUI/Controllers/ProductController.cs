using System.Web.Mvc;
using ManagerSystem.BLL.DTO;
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = _productService.Create(productDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Product");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(productDto);
        }
    }
}