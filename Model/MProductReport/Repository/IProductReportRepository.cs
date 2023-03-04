namespace ConstradeApi.Model.MProductReport.Repository
{
    public interface IProductReportRepository
    {
        Task<bool> ReportProduct(ProductReportModel info);
    }
}
