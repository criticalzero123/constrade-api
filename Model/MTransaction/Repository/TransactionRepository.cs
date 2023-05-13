using ConstradeApi.Entity;
using ConstradeApi.Model.MProduct;
using ConstradeApi.Model.MUser;
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

        public async Task<int> SoldProduct(TransactionModel transaction)
        {

            Product? product = await _context.Products.FindAsync(transaction.ProductId);

            if (product == null) return -1;

            if (product.PreferTrade == "wallet")
            {
                Wallet buyerWallet = _context.UserWallet.Where(w => w.UserId == transaction.BuyerUserId).First();
                Wallet senderWaller = _context.UserWallet.Where(w => w.UserId == transaction.SellerUserId).First();

                if (buyerWallet.Balance < product.Cash) return -2;

                //Deducting the balance of the sender
                buyerWallet.Balance -= product.Cash;
                await _context.SaveChangesAsync();

                //Adding the balance of the receiver
                senderWaller.Balance += product.Cash;
                await _context.SaveChangesAsync();

                SendMoneyTransaction _transaction = new SendMoneyTransaction()
                {
                    SenderWalletId = buyerWallet.WalletId,
                    ReceiverWalletId = senderWaller.WalletId,
                    Amount = product.Cash,
                };

                await _context.SendMoneyTransactions.AddAsync(_transaction);
                await _context.SaveChangesAsync();
            }

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

            return transaction.ProductId;
        }


        public async Task<IEnumerable<TransactionModel>> GetAllTransaction()
        {
            return await _context.Transactions.Select(_t => _t.ToModel()).ToListAsync();
        }


        public async Task<TransactionFullDetails?> GetTransaction(int id)
        {
           Transaction? transaction = await _context.Transactions.Include(_t => _t.Seller.Person)
                                                                 .Include(_t => _t.Buyer.Person)
                                                                 .Include(_t => _t.Product)
                                                                 .Where(_t => _t.ProductId == id)
                                                                 .FirstOrDefaultAsync();

            if(transaction == null) return null;

            return new TransactionFullDetails
            {
                Transaction = transaction.ToModel(),
                Buyer = new UserAndPersonModel(transaction.Buyer.ToModel(), transaction.Buyer.Person.ToModel()),
                Seller = new UserAndPersonModel(transaction.Seller.ToModel(), transaction.Seller.Person.ToModel()),
                Product = transaction.Product.ToModel()
            };
        }

        public async Task<IEnumerable<TransactionDisplayDetails>> GetTransactionByUser(int id)
        {
            IEnumerable<TransactionDisplayDetails> transactions = await _context.Transactions.Include(_t => _t.Buyer.Person)
                                                                                          .Include(_t => _t.Seller.Person)
                                                                                          .Include(_t => _t.Product)
                                                                                          .Where(_t => _t.BuyerUserId == id || _t.SellerUserId == id)
                                                                                          .Select(_t => new TransactionDisplayDetails
                                                                                          {
                                                                                              TransactionId = _t.TransactionId,
                                                                                              ProductId = _t.ProductId,
                                                                                              BuyerId=_t.BuyerUserId,
                                                                                              BuyerName = _t.Buyer.Person.FirstName + " " + _t.Buyer.Person.LastName,
                                                                                              SellerId = _t.SellerUserId,
                                                                                              SellerName = _t.Seller.Person.FirstName + " " + _t.Seller.Person.LastName,
                                                                                              ProductImage = _t.Product.ThumbnailUrl,
                                                                                              ProductName = _t.Product.Title,
                                                                                              TransactionDate = _t.DateTransaction,
                                                                                          })
                                                                                          .ToListAsync();

            return transactions;
        }

    }
}
