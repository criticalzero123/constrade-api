using ConstradeApi.Entity;

namespace ConstradeApi.Model.MSystemFeedback.Repository
{
    public class SystemFeedbackRepository : ISystemFeedbackRepository
    {
        private readonly DataContext _context;

        public SystemFeedbackRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddSystemFeedback(SystemFeedbackModel info)
        {
            SystemFeedback feedback = new SystemFeedback()
            {
                UserId = info.UserId,
                ReportType = info.ReportType,
                Title= info.Title,
                Description= info.Description,
                DateSubmitted = DateTime.Now,
            };

            await _context.SystemFeedback.AddAsync(feedback);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
