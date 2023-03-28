namespace ConstradeApi.VerificationModel.MProductPrices.Repository
{
    public interface IProductPricesRepository
    {
        public Task<IEnumerable<ProductPricesResponse>> GetAllProductsPrice(string text);

    }
}
