using ConstradeApi.Entity;
using ConstradeApi.Model.MProduct;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            BoostProduct? boosted = await _context.BoostProduct.Where(_b => _b.ProductId == id).FirstOrDefaultAsync();

            if(boosted == null || boosted.Status != "active") return null;

            return boosted.ToModel();
        }

        public async Task<bool> ProductBoost(int id, int days)
        {
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

            return true;
        }
    }
}
