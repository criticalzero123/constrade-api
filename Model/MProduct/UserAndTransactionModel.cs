using ConstradeApi.Model.MTransaction;
using ConstradeApi.Model.MUser;

namespace ConstradeApi.Model.MProduct
{
    public class UserAndTransactionModel
    {
        public TransactionModel Transaction { get; set; }
        public PersonModel Buyer { get; set; }
        public PersonModel Seller { get; set; }
    }
}
