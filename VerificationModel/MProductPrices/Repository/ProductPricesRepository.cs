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
           
            IEnumerable<string> products = await _context.ProductPrices.Where(_p => _p.Name.ToLower().Contains(text.ToLower()))
                                                                                      .Select(_p => _p.Name)
                                                                                      .ToListAsync();
            var result = Enumerable.DistinctBy(products, _p => _p);                                                  
            return result;
        }

        public async Task<IEnumerable<ProductPricesResponse>> GetAllShopPrices(string name)
        {
            string trimmedSearchName = name.Replace("[", "").Replace("]", "").Trim();
            IEnumerable<ProductPricesResponse> result = await _context.ProductPrices.Where(_p => _p.Name.ToLower() == trimmedSearchName.ToLower())
                                                                                    .Select(_p => _p.ToModel())
                                                                                    .ToListAsync();

            return result;
        }
    }
}
