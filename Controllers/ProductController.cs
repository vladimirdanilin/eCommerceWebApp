using eCommerceWebApp.Data;
using eCommerceWebApp.Data.Enums;
using eCommerceWebApp.Data.Services;
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
    }
}
