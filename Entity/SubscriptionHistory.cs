using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("subscription_history")]
    public class SubscriptionHistory
    {
        [Key]
        [Column("subscription_history_id")]
        public int SubscriptionHistoryId { get; set; }

        [ForeignKey("Subscription")]
        [Column("subscription_id")]
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        [Column("date_updated"), Required]
        public DateTime DateUpdate { get; set; }

        [Column("previous_subscription_type"), Required]
        public string PreviousSubscriptionType { get; set; } = string.Empty;

        [StringLength(20)]
        [Column("new_subscription_type"), Required]
        public string NewSubscriptionType { get; set; } = string.Empty;

        [Column("previous_date_start"), Required]
        public DateTime PreviousDateStart { get; set; }

        [Column("new_date_start"), Required]
        public DateTime NewDateStart { get; set; }

        [Column("previous_date_end"), Required]
        public DateTime PreviousDateEnd { get; set; }

        [Column("new_date_end"), Required]
        public DateTime NewDateEnd { get; set; }

        [Column("previous_amount"), Required]
        public decimal PreviousAmount { get; set; }

        [Column("new_amount"), Required]
        public decimal NewAmount { get; set; }
    }
}
