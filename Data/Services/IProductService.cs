using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task AddProductAsync(Product product);

        Task EditProductAsync(Product editedProduct);

        List<Product> SearchForProduct(string searchString);

        void DeleteProductAsync(int id);
    }
}
