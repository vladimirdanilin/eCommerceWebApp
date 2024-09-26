using ECommerceWebApp.Data.Enums;
using ECommerceWebApp.DTOs;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace ECommerceWebApp.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(ProductDTO productDTO)
        {
            var product = new Product
            {
                PictureURL = productDTO.PictureURL,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                ProductCategory = productDTO.ProductCategory
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(ProductCategory? productCategory)
        {
            return await _context.Products
                    .Where(p => p.AvailableForSale == true && (productCategory == null || p.ProductCategory == productCategory))
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        PictureURL = p.PictureURL,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        TotalNumberOfRates = p.TotalNumberOfRates,
                        TotalNumberOfStars = p.TotalNumberOfStars,
                        Quantity = p.Quantity,
                        AvailableForSale = p.AvailableForSale
                    }
                    ).ToListAsync();
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDTO
                { 
                   Id = p.Id,
                   PictureURL = p.PictureURL,
                   Name = p.Name,
                   Description = p.Description,
                   Price = p.Price,
                   TotalNumberOfRates = p.TotalNumberOfRates,
                   TotalNumberOfStars= p.TotalNumberOfStars,
                   Quantity = p.Quantity
                }).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<ProductDTO>> SearchForProductAsync(string searchString)
        {
            return await _context.Products
                .Where(p => p.Name.ToLower().Contains(searchString.ToLower()) && p.AvailableForSale == true)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    PictureURL = p.PictureURL,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    TotalNumberOfRates = p.TotalNumberOfRates,
                    TotalNumberOfStars = p.TotalNumberOfStars,
                    ProductCategory = p.ProductCategory,
                    Quantity = p.Quantity,
                    AvailableForSale = p.AvailableForSale
                }).ToListAsync();
        }

        public async Task EditProductAsync(ProductDTO editedProduct)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == editedProduct.Id);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException("Product not found");
            } 

            existingProduct.Name = editedProduct.Name;
            existingProduct.Description = editedProduct.Description;
            existingProduct.Price = editedProduct.Price;
            existingProduct.PictureURL = editedProduct.PictureURL;
            existingProduct.ProductCategory = editedProduct.ProductCategory;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task AddRatingAsync(int productId, int userId, int numberOfStars)
        {
            var existingRating = await _context.Ratings.FirstOrDefaultAsync(r => r.ProductId == productId && r.UserId == userId);

            if (existingRating == null)
            {
                var rating = new Rating
                {
                    ProductId = productId,
                    UserId = userId,
                    NumberOfStars = numberOfStars
                };

                _context.Ratings.Add(rating);

                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

                if (product != null)
                {
                    product.TotalNumberOfStars += numberOfStars;
                    product.TotalNumberOfRates++;
                    _context.Products.Update(product);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveProductFromSaleAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            product.AvailableForSale = false;

            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetNotAvailableProductsAsync()
        {
            return await _context.Products
                    .Where(p => p.AvailableForSale == false)
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        PictureURL = p.PictureURL,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        TotalNumberOfRates = p.TotalNumberOfRates,
                        TotalNumberOfStars = p.TotalNumberOfStars,
                        Quantity = p.Quantity,
                        AvailableForSale = p.AvailableForSale
                    }).ToListAsync();
        }

        public async Task ReturnProductToSaleAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            product.AvailableForSale = true;

            _context.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
