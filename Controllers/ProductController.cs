using ECommerceWebApp.Data;
using ECommerceWebApp.Data.Enums;
using ECommerceWebApp.Data.Services;
using ECommerceWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<User> _userManager;

        public ProductController(IProductService productService, UserManager<User> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(ProductCategory? productCategory, string returnUrl = null)
        {
            var allProducts = await _productService.GetAllProductsAsync();

            if (productCategory != null)
            {
                allProducts = allProducts.Where(p => p.ProductCategory == productCategory);
            }

            ViewBag.ReturnUrl = returnUrl;

            return View(allProducts);
        }

        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([Bind("PictureURL, Name, Description, Price")] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await _productService.AddProductAsync(product);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetDetails(int id)
        {
            var productDetails = await _productService.GetProductByIdAsync(id);

            if (productDetails == null)
            {
                return View("Empty");
            }

            return View(productDetails);
        }

        public async Task<IActionResult> SearchForProduct(string searchString)
        {
            IEnumerable<Product> searchedProducts = await _productService.SearchForProductAsync(searchString);

            return View("Index", searchedProducts);
        }

        //Action to display the form to edit
        public async Task<IActionResult> EditProduct(int productId)
        {
            var productDetails = await _productService.GetProductByIdAsync(productId);

            return View(productDetails);
        }

        //Updates the edited product in the database
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View("EditProduct", product);
            }

            await _productService.EditProductAsync(product);
            return RedirectToAction(nameof(GetDetails), new { id = product.Id });
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> AddRating(int productId, int numberOfStars)
        {
            var user = await _userManager.GetUserAsync(User);
            await _productService.AddRatingAsync(productId, user.Id, numberOfStars);

            return RedirectToAction("Index");
        }
    }
}
