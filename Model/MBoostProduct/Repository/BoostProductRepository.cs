using ConstradeApi.Entity;
using ConstradeApi.Model.MProduct;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MBoostProduct.Repository
{
    public class BoostProductRepository : IBoostProductRepository
    {
        private readonly DataContext _context;

        public BoostProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CancelBoost(int id)
        {
            BoostProduct? boostProduct = await _context.BoostProduct.FindAsync(id);

            if (boostProduct == null) return false;

            if(DateTime.Now > boostProduct.DateTimeExpired)
            {
                boostProduct.Status = "expired";
                return false;
            }

            boostProduct.Status = "cancelled";
            boostProduct.DateTimeExpired= DateTime.Now;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProductCardDetails>> GetBoostedProducts()
        {
            IEnumerable<ProductCardDetails> products = await _context.BoostProduct.Join(_context.Products.Include(p => p.User.Person),
                                                                                        bp => bp.ProductId,
                                                                                        p => p.ProductId,
                                                                                        (bp, p) => new { bp, p })
                                                                                  .Where(result => result.bp.DateTimeExpired > DateTime.Now)
                                                                                  .Select(result => new ProductCardDetails
                                                                                  {
                                                                                      ProductId = result.p.ProductId,
                                                                                      ProductName = result.p.Title,
                                                                                      ThumbnailUrl = result.p.ThumbnailUrl,
                                                                                      UserName = result.p.User.Person.FirstName + " " + result.p.User.Person.LastName,
                                                                                      UserImage = result.p.User.ImageUrl,
                                                                                      PreferTrade = result.p.PreferTrade,
                                                                                      DateCreated = result.p.DateCreated,
                                                                                  })
                                                                                  .ToListAsync();

            return products;
        }

        public async Task<BoostProductModel?> GetProductBoost(int id)
        {
            BoostProduct? boosted = await _context.BoostProduct.Where(_b => _b.ProductId == id && _b.Status == "active").FirstOrDefaultAsync();

            if(boosted == null || boosted.Status != "active") return null;

            if(boosted.DateTimeExpired < DateTime.Now)
            {
                boosted.Status = "expired";
                _context.SaveChanges();
                return null;
            }

            return boosted.ToModel();
        }

        public async Task<bool> ProductBoost(int id, int days, int userId)
        {
            User user = await _context.Users.Where(_u => _u.UserId == userId).FirstAsync();
            //this is the amount per day
            int amount = 5;
            Wallet wallet= await _context.UserWallet.Where(w => w.UserId == userId).FirstAsync();

            if(wallet.Balance < days * amount) return false;

            int partialAmount = amount * days;
            decimal deduction = user.UserType == "premium" ?  Convert.ToDecimal(Convert.ToDouble(partialAmount) - Convert.ToDouble(partialAmount * .15)) : partialAmount;

            wallet.Balance -= deduction;

            BoostProduct product = new BoostProduct
            {
                ProductId = id,
                DaysBoosted = days,
                Status = "active",
                DateTimeExpired = DateTime.Now.AddDays(days),
                DateBoosted = DateTime.Now
            };

            await _context.BoostProduct.AddAsync(product);
            await _context.SaveChangesAsync();

            await _context.OtherTransactions.AddAsync(new OtherTransaction
            {
                WalletId = wallet.WalletId,
                Amount = deduction,
                TransactionType = Enums.OtherTransactionType.Boost,
                Date = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditBoostDay(int id, int newDay)
        {
            BoostProduct? product = await _context.BoostProduct.Include(p => p.Product.User)
                                                                 .Where(p => p.BoostProductId == id && p.Status == "active")
                                                                 .FirstOrDefaultAsync();

            if (product == null) return false;


            int daysLeft = (product.DateTimeExpired.Day - DateTime.Now.Day) - newDay;


            product.DateTimeExpired = product.DateTimeExpired.AddDays(-daysLeft);

            Wallet userWallet = await _context.UserWallet.Where(w => w.UserId == product.Product.User.UserId).FirstAsync();
            int amountForBoost = 5;
            decimal amountRefund = daysLeft * amountForBoost;

            decimal refund = product.Product.User.UserType == "premium" ? Convert.ToDecimal(Convert.ToDouble(amountRefund) - (Convert.ToDouble(amountRefund) * .15)) : amountRefund;

            userWallet.Balance += refund;
             _context.SaveChanges();

            await _context.OtherTransactions.AddAsync(new OtherTransaction
            {
                WalletId = userWallet.WalletId,
                Amount = refund,
                TransactionType = Enums.OtherTransactionType.Refund,
                Date = DateTime.Now,
            });
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
