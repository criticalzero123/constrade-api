using ConstradeApi.Entity;

namespace ConstradeApi.Model.MUserReport.Repositories
{
    public class UserReportRepository : IUserReportRepository
    {
        private readonly DataContext _context;

        public UserReportRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> ReportUser(UserReportModel userReportModel)
        {
            UserReport report = new UserReport
            {
                ReportBy = userReportModel.ReportBy,
                Reported = userReportModel.Reported,
                Description= userReportModel.Description,
                DateSubmitted= userReportModel.DateSubmitted,
                ReportStatus= "active",
            };

            await _context.UserReport.AddAsync(report);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
