using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("user_report")]
    public class UserReport
    {
        [Key]
        [Column("user_report_id")]
        public int UserReportId { get; set; }

        [ForeignKey("User1")]
        [Column("report_by")]
        public int ReportBy { get; set; }
        public User User1 { get; set; }

        [ForeignKey("User2")]
        [Column("reported")]
        public int Reported { get; set; }
        public User User2 { get; set; }

        [Column("report_status")]
        public string ReportStatus { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("date_submitted")]
        public DateTime DateSubmitted { get; set; } 
    }
}
