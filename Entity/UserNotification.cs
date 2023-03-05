using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("user_notification")]
    public class UserNotification
    {
        [Key]
        [Column("user_notification_id")]
        public int UserNotificationId { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        
        [Column("to_id")]
        public int ToId { get; set; }

        [Column("notification_type")]
        public string NotificationType { get; set; } = string.Empty;
        [Column("notification_message")]
        public string NotificationMessage { get; set; } = string.Empty;

        [Column("notification_date")]
        public DateTime NotificationDate { get; set; }


    }
}
