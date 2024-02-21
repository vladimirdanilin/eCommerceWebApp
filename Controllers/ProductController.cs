using eCommerceWebApp.Data;
using eCommerceWebApp.Data.Enums;
using eCommerceWebApp.Data.Services;
using eCommerceWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(ProductCategory? productCategory)
        {
            var allProducts = await _service.GetAllProducts();

            if (productCategory != null)
            {
                allProducts = allProducts.Where(p => p.ProductCategory == productCategory);
            }
            return View(allProducts);
        }

        //Get: Product/AddProduct
        public async Task<IActionResult> AddProduct()
        {
        return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddProduct([Bind("PictureURL, Name, Description, Price")]Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            { 
            _service.AddProduct(product);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
