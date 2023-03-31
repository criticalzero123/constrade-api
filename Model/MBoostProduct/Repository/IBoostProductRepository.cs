using ConstradeApi.Model.MProduct;

namespace ConstradeApi.Model.MBoostProduct.Repository
{
    public interface IBoostProductRepository
    {
        Task<BoostProductModel?> GetProductBoost(int id);
        Task<bool> ProductBoost(int id, int days);
        Task<bool> CancelBoost(int id);
        Task<IEnumerable<ProductCardDetails>> GetBoostedProducts();
    }
}
