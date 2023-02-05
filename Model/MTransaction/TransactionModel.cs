
namespace ConstradeApi.Model.MTransaction
{
    public class TransactionModel
    {

        public int TransactionId { get; set; }


        public int ProductId { get; set; }

        public int BuyerUserId { get; set; }

        public int SellerUserId { get; set; }

        public bool InAppTransaction { get; set; }
        public bool GetWanted { get; set; }

        public DateTime DateTransaction { get; set; }
    }
}
