using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("subscription")]
    public class Subscription
    {
        [Key]
        [Column("subscription_id")]
        public int SubscriptionId { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required,Column("subscription_type")]
        public string SubscriptionType { get; set; } = string.Empty;

        [Required]
        [Column("date_start")]
        public DateTime DateStart { get; set; } = DateTime.Now;

        [Required]
        [Column("date_end")]
        public DateTime DateEnd { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }
    }
}
