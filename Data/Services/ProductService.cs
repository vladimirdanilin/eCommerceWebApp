using ECommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApp.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public void DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var result = await _context.Products.ToListAsync();
            return result;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<IEnumerable<Product>> SearchForProductAsync(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return Enumerable.Empty<Product>();
            }

            return await _context.Products
                .Where(p => p.Name.ToLower().Contains(searchString.ToLower()))
                .ToListAsync();
        }

        public async Task EditProductAsync(Product editedProduct)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == editedProduct.Id);

            if (existingProduct == null)
            {

            }
            else
            { 
                existingProduct.Name = editedProduct.Name;
                existingProduct.Description = editedProduct.Description;
                existingProduct.Price = editedProduct.Price;
                existingProduct.PictureURL = editedProduct.PictureURL;
                existingProduct.ProductCategory = editedProduct.ProductCategory;
            }
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
    }
}
