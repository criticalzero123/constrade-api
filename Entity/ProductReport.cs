using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("product_report")]
    public class ProductReport
    {
        [Key]
        [Column("product_report_id")]
        public int ProductReportId { get; set; }

        [ForeignKey("User")]
        [Column("reported_by")]
        public int ReportedBy { get; set; }
        public User User { get; set; }

        [ForeignKey("Product")]
        [Column("product_reported")]
        public int ProductReported { get; set; }
        public Product Product { get; set; }

        [StringLength(50)]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [StringLength(155)]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("date_submitted")]
        public DateTime DateSubmitted { get; set; }

    }
}
