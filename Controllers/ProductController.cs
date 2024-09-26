using ECommerceWebApp.Data;
using ECommerceWebApp.Data.Enums;
using ECommerceWebApp.Data.Services;
using ECommerceWebApp.DTOs;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;
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

        public async Task<IActionResult> Index(ProductCategory? productCategory)
        {
            var productsDTOs = await _productService.GetProductsByCategoryAsync(productCategory);

            var productsViewModels = productsDTOs.Select
                (dto => new ProductViewModel
                {
                    Id = dto.Id,
                    PictureURL = dto.PictureURL,
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    TotalNumberOfRates = dto.TotalNumberOfRates,
                    TotalNumberOfStars = dto.TotalNumberOfStars,
                    Quantity = dto.Quantity,
                    AvailableForSale = dto.AvailableForSale
                }).ToList();

            if (productsViewModels.Count == 0)
            {
                return View("ProductsNotFound");
            }

            return View(productsViewModels);
        }

        [Authorize(Roles = Roles.AdminRoles)]
        public IActionResult AddProduct()
        {
            return View();
        }

        [Authorize(Roles = Roles.AdminRoles)]
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productViewModel);
            }

            var productDTO = new ProductDTO
            {
                PictureURL= productViewModel.PictureURL,
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                ProductCategory = productViewModel.ProductCategory,
            };

            await _productService.AddProductAsync(productDTO);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetDetails(int id)
        {
            var productDTO = await _productService.GetProductByIdAsync(id);

            if (productDTO == null)
            {
                return View("Empty");
            }

            var productViewModel = new ProductViewModel
            { 
                PictureURL = productDTO.PictureURL,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                ProductCategory = productDTO.ProductCategory,
            };

            return View(productViewModel);
        }

        public async Task<IActionResult> SearchForProduct(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return View("ProductsNotFound");
            }

            searchString = searchString.Trim();

            if (searchString.Length > 255)
            {
                return View("ProductsNotFound");
            }

            var searchedProductsDTOs = await _productService.SearchForProductAsync(searchString);

            if (!searchedProductsDTOs.Any())
            {
                return View("ProductsNotFound");
            }

            var productViewModels = searchedProductsDTOs.Select(dto => new ProductViewModel
            {
                Id = dto.Id,
                PictureURL = dto.PictureURL,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                TotalNumberOfRates = dto.TotalNumberOfRates,
                TotalNumberOfStars = dto.TotalNumberOfStars,
                ProductCategory = dto.ProductCategory,
                Quantity = dto.Quantity,
                AvailableForSale = dto.AvailableForSale,
            }).ToList();

            return View("Index", productViewModels);
        }

        //Action to display the form to edit
        [Authorize(Roles = Roles.SuperAdmin + ", " + Roles.SalesManager)]
        public async Task<IActionResult> EditProduct(int productId)
        {
            var productDTO =  await _productService.GetProductByIdAsync(productId);

            var productViewModel = new ProductViewModel
            { 
                Id = productId,
                PictureURL = productDTO.PictureURL,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                ProductCategory = productDTO.ProductCategory,
            };

            return View(productViewModel);
        }

        //Updates the edited product in the database
        [HttpPost]
        [Authorize(Roles = Roles.SuperAdmin + ", " + Roles.SalesManager)]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("EditProduct", productViewModel);
            }

            var productDTO = new ProductDTO
            {
                Id = productViewModel.Id,
                PictureURL = productViewModel.PictureURL,
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                ProductCategory = productViewModel.ProductCategory,
            };

            await _productService.EditProductAsync(productDTO);
            return RedirectToAction(nameof(GetDetails), new { id = productDTO.Id });
        }

        [Authorize(Roles = Roles.Customer)]
        [HttpPost]
        public async Task<IActionResult> AddRating(int productId, int numberOfStars)
        {
            var user = await _userManager.GetUserAsync(User);
            await _productService.AddRatingAsync(productId, user.Id, numberOfStars);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.AdminRoles)]
        public async Task<IActionResult> RemoveProductFromSale(int productId)
        {
            await _productService.RemoveProductFromSaleAsync(productId);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.AdminRoles)]
        public async Task<IActionResult> DisplayNotAvailableProducts()
        {
            var productsDTOs = await _productService.GetNotAvailableProductsAsync();

            if (!productsDTOs.Any())
            {
                return View("ProductsNotFound");
            }

            var productsViewModels = productsDTOs.Select
                (dto => new ProductViewModel
                {
                    Id = dto.Id,
                    PictureURL = dto.PictureURL,
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    TotalNumberOfRates = dto.TotalNumberOfRates,
                    TotalNumberOfStars = dto.TotalNumberOfStars,
                    Quantity = dto.Quantity,
                    AvailableForSale = dto.AvailableForSale
                }).ToList();

            return View("Index", productsViewModels);
        }

        [Authorize(Roles = Roles.AdminRoles)]
        public async Task<IActionResult> ReturnProductToSale(int productId)
        { 
            await _productService.ReturnProductToSaleAsync(productId);

            return RedirectToAction("DisplayNotAvailableProducts"); 
        }
    }
}
