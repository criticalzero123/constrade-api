

using ConstradeApi.Entity;
using ConstradeApi.Model.MProduct;

namespace ConstradeApi.Model.MTransaction.Repository
{
    public interface ITransactionRepository
    {
         Task<int> SoldProduct(TransactionModel transaction);

         Task<IEnumerable<TransactionModel>> GetAllTransaction();

         Task<UserAndTransactionModel?> GetTransaction(int id);

    }
}
