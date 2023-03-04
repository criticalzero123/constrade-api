using ConstradeApi.Entity;

namespace ConstradeApi.Model.MProductReport.Repository
{
    public class ProductReportRepository : IProductReportRepository
    {
        private readonly DataContext _context;

        public ProductReportRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> ReportProduct(ProductReportModel info)
        {
            ProductReport report = new ProductReport
            {
                ReportedBy = info.ReportedBy,
                ProductReported = info.ProductReported,
                Title = info.Title,
                Description = info.Description,
                DateSubmitted = DateTime.Now
            };

            await _context.ProductReport.AddAsync(report);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
