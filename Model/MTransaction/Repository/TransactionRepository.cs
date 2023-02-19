using ConstradeApi.Entity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ConstradeApi.Model.MTransaction.Repository
{
    public class TrsanctionRepository : ITransactionRepository
    {
        private readonly DataContext _context;

        public TrsanctionRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        /// <summary>
        /// POST: check if the product existed
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<bool> SoldProduct(TransactionModel transaction)
        {

            Product? product = await _context.Products.FindAsync(transaction.ProductId);
            if (product == null) return false;


            product.ProductStatus = "sold";
            await _context.SaveChangesAsync();

            _context.Transactions.Add(new Transaction()
            {
                ProductId = transaction.ProductId,
                BuyerUserId = transaction.BuyerUserId,
                SellerUserId = transaction.SellerUserId,
                InAppTransaction = transaction.InAppTransaction,
                GetWanted = transaction.GetWanted,
                IsReviewed = transaction.IsReviewed,
                DateTransaction = transaction.DateTransaction
            });

            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Getting all transactions
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TransactionModel>> GetAllTransaction()
        {
            return await _context.Transactions.Select(_t => new TransactionModel()
            {
                TransactionId = _t.TransactionId,
                ProductId = _t.ProductId,
                BuyerUserId = _t.BuyerUserId,
                SellerUserId = _t.SellerUserId,
                InAppTransaction = _t.InAppTransaction,
                IsReviewed = _t.IsReviewed,
                GetWanted = _t.GetWanted,
                DateTransaction = _t.DateTransaction
            }).ToListAsync();
        }

        /// <summary>
        /// Getting specific transaction by passing the id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TransactionModel?> GetTransaction(int id)
        {
            Transaction? data = await _context.Transactions.FindAsync(id);
            if (data == null) return null;

            TransactionModel transaction = new TransactionModel()
            {
                TransactionId = data.TransactionId,
                ProductId = data.ProductId,
                BuyerUserId = data.BuyerUserId,
                SellerUserId = data.SellerUserId,
                IsReviewed = data.IsReviewed,
                InAppTransaction = data.InAppTransaction,
                GetWanted = data.GetWanted,
                DateTransaction = data.DateTransaction
            };

            return transaction;
        }

        //public async Task<List<TransactionModel>> GetTransactionByUser(int uid)
        //{

        //}
    }
}
