using ConstradeApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("report")]
    public class Report
    {
        [Key]
        [Column("user_id")]
        public int ReportId { get; set; }

        [ForeignKey("User")]
        [Column("reported_by")]
        public int ReportedBy { get; set; }
        public User User { get; set; }

        [Column("id_reported")]
        public int IdReported { get; set; }

        [Column("report_type")]
        public ReportType ReportType { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("status")]
        public ReportStatus Status { get; set; }

        [Column("date_submitted")]
        public DateTime DateSubmitted { get; set; }= DateTime.Now;  
    }
}
