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

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("subscription_type")]
        public string SubscriptionType { get; set; } = string.Empty;

        [Column("date_started")]
        public DateTime DateStarted { get; set; } 

        [Column("date_end")]
        public DateTime DateEnd { get; set; } 

    }
}
