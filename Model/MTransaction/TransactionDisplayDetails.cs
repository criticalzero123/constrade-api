using ConstradeApi.Model.MProduct;
using ConstradeApi.Model.MUser;

namespace ConstradeApi.Model.MTransaction
{
    public class TransactionDisplayDetails
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public string BuyerName { get; set; } = string.Empty;
        public int SellerId { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
    }
}
