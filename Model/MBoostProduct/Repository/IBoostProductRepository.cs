using ConstradeApi.Model.MProduct;

namespace ConstradeApi.Model.MBoostProduct.Repository
{
    public interface IBoostProductRepository
    {
        Task<BoostProductModel?> GetProductBoost(int id);
        Task<bool> ProductBoost(int id, int days, int userId);
        Task<bool> CancelBoost(int id);
        Task<IEnumerable<ProductCardDetails>> GetBoostedProducts();
        Task<bool> EditBoostDay(int id, int newDay);

    }
}
