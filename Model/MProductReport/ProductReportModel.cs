
namespace ConstradeApi.Model.MProductReport
{
    public class ProductReportModel
    {
        public int ProductReportId { get; set; }
        public int ReportedBy { get; set; }
        public int ProductReported { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateSubmitted { get; set; }
    }
}
