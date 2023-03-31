

using ConstradeApi.Entity;

namespace ConstradeApi.Model.MProduct
{
    public class ProductModel
    {
       
        public int ProductId { get; set; }
        public int PosterUserId { get; set; }
        public User? User { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ModelNumber { get; set; }
        public string? SerialNumber { get; set; }
        public string GameGenre { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public decimal Cash { get; set; }
        public string Item { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public int CountFavorite { get; set; }
        public string Condition { get; set; } = string.Empty;
        public string PreferTrade { get; set; } = string.Empty;
        public bool IsDeliver { get; set; }
        public bool IsMeetup { get; set; }
        public string Location { get; set; } = string.Empty;
        public string ProductStatus { get; set; } = string.Empty;
        public bool HasWarranty { get; set; }
        public decimal Value { get; set; }
        public bool HasReceipts { get; set; }
        public bool IsGenerated { get; set; }
    }
}
