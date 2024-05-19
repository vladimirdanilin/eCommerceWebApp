using eCommerceWebApp.Data;
using eCommerceWebApp.Data.Enums;
using eCommerceWebApp.Data.Services;
using eCommerceWebApp.Models;
using Microsoft.AspNetCore.Authorization;
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
            var allProducts = await _service.GetAllProductsAsync();

            if (productCategory != null)
            {
                allProducts = allProducts.Where(p => p.ProductCategory == productCategory);
            }
            return View(allProducts);
        }

        //Get: Product/AddProduct
        [Authorize(Roles = "admin")]
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
            await _service.AddProductAsync(product);
            }
            return RedirectToAction(nameof(Index));
        }

        //Get: Product/GetDetails/id

        public async Task<IActionResult> GetDetails(int id)
        { 
        var productDetails = await _service.GetProductByIdAsync(id);

            if (productDetails == null)
            {
                return View("Empty");
            }
            else
            {
                return View(productDetails);
            }
        }

        public IActionResult SearchForProduct(string searchString)
        {
            List<Product> searchedProducts = _service.SearchForProduct(searchString);
            if (searchedProducts.Any())
            {
                return View("Index", searchedProducts);
            }
            else
            {
                return View("Empty");
            }
        }

        //Action to display the form to edit
        public async Task<IActionResult> EditProduct(int productId)
        { 
            var productDetails = await _service.GetProductByIdAsync(productId);

            return View(productDetails);
        }

        //Updates the edited product in the database
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _service.EditProductAsync(product);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
