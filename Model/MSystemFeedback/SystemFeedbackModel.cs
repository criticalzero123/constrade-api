

namespace ConstradeApi.Model.MSystemFeedback
{
    public class SystemFeedbackModel
    {
        public int SystemFeedbackId { get; set; }
        public int UserId { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }
}
