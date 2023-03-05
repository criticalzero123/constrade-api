using ConstradeApi.Entity;
using ConstradeApi.Enums;

namespace ConstradeApi.Model.MReport.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly DataContext _context;

        public ReportRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateReport(ReportModel model)
        {
            Report report = new Report
            {
                ReportedBy = model.ReportedBy,
                IdReported = model.IdReported,
                ReportType = model.ReportType,
                Description = model.Description,
                Status = ReportStatus.Active,
                DateSubmitted = model.DateSubmitted,
            };

            await _context.Report.AddAsync(report);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
