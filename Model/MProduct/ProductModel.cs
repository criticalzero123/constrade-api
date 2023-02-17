

namespace ConstradeApi.Model.MProduct
{
    public class ProductModel
    {
       
        public int ProductId { get; set; }
        public int PosterUserId { get; set; }

        public string Title { get; set; }
       
        public string Description { get; set; }
      
        public string? ModelNumber { get; set; }
      
        public string? SerialNumber { get; set; }

        public string GameGenre { get; set; }
       
        public string Platform { get; set; }

        public string ThumbnailUrl { get; set; }

        public decimal Cash { get; set; }

        public string Item { get; set; }
       
        public DateTime DateCreated { get; set; }

        public int CountFavorite { get; set; }

        public string Condition { get; set; }

       
        public string PreferTrade { get; set; }

       
        public string DeliveryMethod { get; set; }

        public string Location { get; set; }
        public string ProductStatus { get; set; }

        public bool HasWarranty { get; set; }

        public bool HasReceipts { get; set; }

    }
}
