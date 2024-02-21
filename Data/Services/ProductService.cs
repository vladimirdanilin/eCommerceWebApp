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

        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var result = await _context.Products.ToListAsync();
            return result;
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Product UpdateProduct(int id, Product newProduct)
        {
            throw new NotImplementedException();
        }
    }
}
