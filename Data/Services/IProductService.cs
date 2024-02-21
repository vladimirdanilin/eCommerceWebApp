using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Product GetProductById(int id);

        void AddProduct(Product product);

        Product UpdateProduct(int id, Product newProduct);

        void DeleteProduct(int id);
    }
}
