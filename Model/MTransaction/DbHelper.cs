using ConstradeApi.Entity;

namespace ConstradeApi.Model.MTransaction
{
    public class DbHelper
    {
        private readonly DataContext _context;

        public DbHelper(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<bool> SoldProduct(TransactionModel transaction) 
        {
            User? seller = await _context.Users.FindAsync(transaction.SellerUserId);
            User? buyer = await _context.Users.FindAsync(transaction.BuyerUserId);
            if (seller == null || buyer == null) return false;

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
                DateTransaction= transaction.DateTransaction
            });

            _context.SaveChanges();

            return true;
        }

        public IEnumerable<TransactionModel> GetAllTransaction()
        {
            return _context.Transactions.Select(_t => new TransactionModel()
            {
                TransactionId = _t.TransactionId,
                ProductId= _t.ProductId,
                BuyerUserId = _t.BuyerUserId,
                SellerUserId= _t.SellerUserId,
                InAppTransaction= _t.InAppTransaction,
                GetWanted= _t.GetWanted,
                DateTransaction= _t.DateTransaction
            });
        }


        public TransactionModel? GetTransaction(int id)
        {
            Transaction? data = _context.Transactions.Find(id);
            if (data == null) return null;

            TransactionModel transaction = new TransactionModel()
            {
                TransactionId = data.TransactionId,
                ProductId = data.ProductId,
                BuyerUserId = data.BuyerUserId,
                SellerUserId = data.SellerUserId,
                InAppTransaction = data.InAppTransaction,
                GetWanted = data.GetWanted,
                DateTransaction = data.DateTransaction
            };

            return transaction;
        }
    }
}
