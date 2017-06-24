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

        public ActionResult Edit(int id)
        {
            var managerDto = _productService.GetProductById(id);
            if (managerDto != null)
            {
                return View(managerDto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = _productService.Edit(productDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(productDto);
        }

        public ActionResult Delete(int id)
        {
            var purchaseDto = _productService.GetProductById(id);
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
                var operationDetails = _productService.Delete(id);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View();
        }
    }
}