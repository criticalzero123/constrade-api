using ConstradeApi.Entity;
using ConstradeApi.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.Model.MReport
{
    public class ReportModel
    {
        public int ReportId { get; set; }
        public int ReportedBy { get; set; }
        public int IdReported { get; set; }
        public ReportType ReportType { get; set; }
        public string Description { get; set; } = string.Empty;
        public ReportStatus Status { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }
}
