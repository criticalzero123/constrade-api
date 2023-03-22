namespace ConstradeApi.Model.MProduct
{
    public class ProductCardDetails
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string UserImage { get; set; } = string.Empty;
        public string PreferTrade { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }
}
