using ConstradeApi.Model.MProduct;
using ConstradeApi.Model.MUser;

namespace ConstradeApi.Model.MTransaction
{
    public class TransactionFullDetails
    {
        public TransactionModel Transaction { get; set; }
        public UserAndPersonModel Buyer { get; set; }
        public UserAndPersonModel Seller { get; set; }
        public ProductModel Product { get; set; }
    }
}
