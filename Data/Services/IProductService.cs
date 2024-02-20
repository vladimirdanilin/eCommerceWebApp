using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();

        Product GetById(int id);

        void Add(Product product);

        Product Update(int id, Product newProduct);

        void Delete(int id);
    }
}
