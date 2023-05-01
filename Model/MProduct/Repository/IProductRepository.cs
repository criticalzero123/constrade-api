
using ConstradeApi.Enums;

namespace ConstradeApi.Model.MProduct.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetAllProducts();
        Task<CreationProductResponse> Save(ProductModel product, IEnumerable<string> imageList);
        Task<IEnumerable<ProductCardDetails>> GetProductsByUserId(int userId);
        Task<ProductFullDetails?> Get(int id, int? userId);
        Task<bool> DeleteProduct(int id);
        Task<bool> UpdateProduct(int id, ProductModel product);
        Task<bool> AddFavoriteProduct(FavoriteModel info);
        Task<bool> DeleteFavoriteProduct(int id);
        Task<IEnumerable<FavoriteProductDetails>> GetFavoriteUser(int userId);
        Task<IEnumerable<ProductCardDetails>> GetSearchProduct(string text);
        Task<IEnumerable<ProductCardDetails>> GetSearchProductGenre(string genre);
        Task<IEnumerable<ProductCardDetails>> GetSearchProductPlatform(string platform);
        Task<IEnumerable<ProductCardDetails>> GetSearchProductMethod(string tradeMethod);
        Task<IEnumerable<ProductCardDetails>> GetProductByLength(int count);
    }
}
