using eCommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebApp.Data.Services
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

        public List<Product> SearchForProduct(string searchString)
        {
            List<Product> searchedProducts = new List<Product>();

            if (searchString == null)
                return searchedProducts;

            return _context.Products
                .Where(p => p.Name.ToLower().Contains(searchString.ToLower()))
                .ToList();
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
    }
}
