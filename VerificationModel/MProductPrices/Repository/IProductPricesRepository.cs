namespace ConstradeApi.VerificationModel.MProductPrices.Repository
{
    public interface IProductPricesRepository
    {
        public Task<IEnumerable<string>> GetAllProductsPrice(string text);
        public Task<IEnumerable<ProductPricesResponse>> GetAllShopPrices(string text);

    }
}
