using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("reviews")]
    public class Review
    {
        [Key]
        [Required]
        public int ReviewId { get; set; }

        [ForeignKey("Transaction")]
        [Column("transaction_ref_id")]
        public int TransactionRefId { get; set; }
        public Transaction Transaction { get; set; }

        [Required]
        [Column("rate")]
        public short Rate { get; set; }

        [Required]
        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
