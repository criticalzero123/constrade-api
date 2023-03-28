using ConstradeApi.VerificationEntity;


namespace ConstradeApi.VerificationModel.MProductPrices
{
    public class ProductPricesResponse
    {
        public int ProductPricesId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Platform { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string OriginUrl { get; set; } = string.Empty;
    }
}
