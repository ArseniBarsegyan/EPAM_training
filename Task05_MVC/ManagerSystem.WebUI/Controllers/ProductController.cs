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
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index(int? page)
        {
            var pageSize = ConstantStorage.PageSize;
            var pageNumber = (page ?? 1);
            return View(_productService.GetAllProductsList().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult ProductSearch(string name)
        {
            name = name.ToLower();
            var products = _productService.GetAllProductsList().Where(x => x.Name.ToLower() == name);
            ViewBag.Products = products;
            return PartialView(products);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var productDto = new ProductDto {Name = model.ProductName, Price = model.Price};
                var operationDetails = _productService.Create(productDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Product");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var productDto = _productService.GetProductById(id);

            if (productDto != null)
            {
                var model = new ProductEditModel
                {
                    Id = productDto.Id,
                    Name = productDto.Name,
                    Price = productDto.Price
                };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ProductEditModel model)
        {
            if (ModelState.IsValid)
            {
                var productDto = new ProductDto
                {
                    Id = model.Id,
                    Name = model.Name,
                    Price = model.Price
                };

                var operationDetails = _productService.Edit(productDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
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