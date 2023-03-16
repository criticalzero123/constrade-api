using ConstradeApi.Entity;
using ConstradeApi.Model.MProduct;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ConstradeApi.Model.MTransaction.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;

        public TransactionRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        /// <summary>
        /// POST: check if the product existed
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<int> SoldProduct(TransactionModel transaction)
        {

            Product? product = await _context.Products.FindAsync(transaction.ProductId);

            if (product == null) return -1;

            Transaction _t = new Transaction
            {
                ProductId = transaction.ProductId,
                BuyerUserId = transaction.BuyerUserId,
                SellerUserId = transaction.SellerUserId,
                GetWanted = transaction.GetWanted,
                InAppTransaction = transaction.InAppTransaction,
                IsReviewed = transaction.IsReviewed,
                DateTransaction = DateTime.Now
            };

            product.ProductStatus = "sold";
            await _context.SaveChangesAsync();

            _context.Transactions.Add(_t);

            _context.SaveChanges();

            return _t.TransactionId;
        }

        /// <summary>
        /// Getting all transactions
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TransactionModel>> GetAllTransaction()
        {
            return await _context.Transactions.Select(_t => _t.ToModel()).ToListAsync();
        }

        /// <summary>
        /// Getting specific transaction by passing the id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserAndTransactionModel?> GetTransaction(int id)
        {
            Transaction transaction = await _context.Transactions.Include(_t => _t.Seller.Person).Include(_t => _t.Buyer.Person).Where(_t => _t.ProductId == id).FirstAsync();

            return new UserAndTransactionModel
            {
                Transaction = transaction.ToModel(),
                Buyer = transaction.Buyer.Person.ToModel(),
                Seller = transaction.Seller.Person.ToModel(),
            };
        }

        //public async Task<List<TransactionModel>> GetTransactionByUser(int uid)
        //{

        //}
    }
}
