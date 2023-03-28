using ConstradeApi.Services.EntityToModel;
using ConstradeApi.VerificationEntity;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.VerificationModel.MProductPrices.Repository
{
    public class ProductPricesRepository : IProductPricesRepository
    {
        private VerificationDataContext _context;

        public ProductPricesRepository(VerificationDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<string>> GetAllProductsPrice(string text)
        {
            string lower = text.ToLower();
            IEnumerable<string> products = await _context.ProductPrices.Where(_p => _p.Name.ToLower().Contains(lower))
                                                                                      .Select(_p => _p.Name)
                                                                                      .ToListAsync();
            var result = Enumerable.DistinctBy(products, _p => _p);                                                  
            return result;
        }

        public async Task<IEnumerable<ProductPricesResponse>> GetAllShopPrices(string name)
        {
            IEnumerable<ProductPricesResponse> result = await _context.ProductPrices.Where(_p => _p.Name.ToLower() == name)
                                                                                    .Select(_p => _p.ToModel())
                                                                                    .ToListAsync();

            return result;
        }
    }
}
