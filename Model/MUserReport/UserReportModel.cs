namespace ConstradeApi.Model.MUserReport
{
    public class UserReportModel
    {
        public int UserReportId { get; set; }
        public int ReportBy { get; set; }
        public int Reported { get; set; }
        public string ReportStatus { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateSubmitted { get; set; }
    }
}
