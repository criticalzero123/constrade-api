

using ConstradeApi.Entity;

namespace ConstradeApi.Model.MTransaction.Repository
{
    public interface ITransactionRepository
    {
         Task<int> SoldProduct(TransactionModel transaction);

         Task<IEnumerable<TransactionModel>> GetAllTransaction();

         Task<TransactionModel?> GetTransaction(int id);

    }
}
