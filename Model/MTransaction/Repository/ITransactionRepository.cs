namespace ConstradeApi.Model.MTransaction.Repository
{
    public interface ITransactionRepository
    {
         Task<bool> SoldProduct(TransactionModel transaction);

         Task<IEnumerable<TransactionModel>> GetAllTransaction();

         Task<TransactionModel?> GetTransaction(int id);

    }
}
