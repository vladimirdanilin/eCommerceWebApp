using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task AddProductAsync(Product product);

        Product UpdateProduct(int id, Product newProduct);

        void DeleteProduct(int id);
    }
}
