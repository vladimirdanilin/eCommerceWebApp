using eCommerceWebApp.Data;
using eCommerceWebApp.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(ProductCategory? productCategory)
        {
            var allProducts = await _context.Products.ToListAsync();

            if (productCategory != null)
            {
                allProducts = await _context.Products.Where(p => p.ProductCategory == productCategory).ToListAsync();
            }
            return View(allProducts);
        }
    }
}
