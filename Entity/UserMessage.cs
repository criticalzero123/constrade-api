using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("user_message")]
    public class UserMessage
    {
        [Key]
        [Column("user_message_id")]
        public int UserMessageId { get; set; }

        [ForeignKey("Chat")]
        [Column("user_chat_id")]
        public int UserChatId { get; set; }
        public UserChat Chat { get; set; }

        [ForeignKey("User")]
        [Column("sender_id")]
        public int SenderId { get; set; }
        public User User { get; set; }

        [StringLength(150)]
        [Column("message")]
        public string Message { get; set; } = string.Empty;

        [Column("date_sent")]
        public DateTime DateSent { get; set; }
    }
}
