using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("system_feedback")]
    public class SystemFeedback
    {
        [Key]
        [Column("system_feedback_id")]
        public int SystemFeedbackId { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("report_type")]
        [StringLength(10)]
        public string ReportType { get; set; } = string.Empty;

        [Column("title")]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        [StringLength(155)]
        public string Description { get; set; } = string.Empty;

        [Column("status")]
        [StringLength(10)]
        public string Status { get; set; } = "active";

        [Column("date_submitted")]
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }
}
