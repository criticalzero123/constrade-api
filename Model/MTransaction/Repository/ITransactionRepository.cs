

using ConstradeApi.Entity;
using ConstradeApi.Model.MProduct;

namespace ConstradeApi.Model.MTransaction.Repository
{
    public interface ITransactionRepository
    {
        /// <summary>
        /// POST: check if the product existed
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<int> SoldProduct(TransactionModel transaction);
        /// <summary>
        /// Getting all transactions in the schema
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TransactionModel>> GetAllTransaction();
        /// <summary>
        /// Getting specific transaction by passing the id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TransactionFullDetails?> GetTransaction(int id);
        Task<IEnumerable<TransactionDisplayDetails>> GetTransactionByUser(int id);
    }
}
