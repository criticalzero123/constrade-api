namespace ConstradeApi.Model.MReport.Repository
{
    public interface IReportRepository
    {
        Task<bool> CreateReport(ReportModel model);
    }
}
