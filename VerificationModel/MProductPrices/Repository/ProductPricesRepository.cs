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
        public async Task<IEnumerable<ProductPricesResponse>> GetAllProductsPrice(string text)
        {
            string lower = text.ToLower();
            IEnumerable<ProductPricesResponse> products = await _context.ProductPrices.Where(_p => _p.Name.ToLower().Contains(lower))
                                                                                      .Select(_p => _p.Response())
                                                                                      .ToListAsync();

            return products;
        }
    }
}
