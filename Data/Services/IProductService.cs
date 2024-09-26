using ECommerceWebApp.Data.Enums;
using ECommerceWebApp.DTOs;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;

namespace ECommerceWebApp.Data.Services
{
    public interface IProductService
    {
        Task AddProductAsync(ProductDTO productDTO);

        Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(ProductCategory? productCategory);

        Task<ProductDTO> GetProductByIdAsync(int id);

        Task <IReadOnlyCollection<ProductDTO>> SearchForProductAsync(string searchString);

        Task EditProductAsync(ProductDTO editedProductDTO);

        Task AddRatingAsync(int productId, int userId, int numberOfStars);

        Task RemoveProductFromSaleAsync(int productId);

        Task<IEnumerable<ProductDTO>> GetNotAvailableProductsAsync();

        Task ReturnProductToSaleAsync(int productId);
    }
}
