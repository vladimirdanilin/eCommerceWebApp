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

            foreach (var item in _context.Products)
            {
                if (item.Name.ToLower().Contains(searchString.ToLower()))
                { 
                searchedProducts.Add(item);
                }
            }
            return searchedProducts;
        }

        public async Task<Product> UpdateProductAsync(int id, Product newProduct)
        {
            throw new NotImplementedException();
        }
    }
}
