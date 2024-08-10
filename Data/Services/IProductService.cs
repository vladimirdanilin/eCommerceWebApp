using ECommerceWebApp.Models;

namespace ECommerceWebApp.Data.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task AddProductAsync(Product product);

        Task EditProductAsync(Product editedProduct);

        Task <IEnumerable<Product>> SearchForProductAsync(string searchString);

        Task AddRatingAsync(int productId, int userId, int numberOfStars);

        void DeleteProductAsync(int id);
    }
}
