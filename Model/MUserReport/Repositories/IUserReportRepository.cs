namespace ConstradeApi.Model.MUserReport.Repositories
{
    public interface IUserReportRepository
    {
        Task<bool> ReportUser(UserReportModel userReportModel);
    }
}
