using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("user_chat")]
    public class UserChat
    {
        [Key]
        [Column("user_chat_id")]
        public int UserChatId { get; set; }

        [ForeignKey("User1")]
        [Column("user_id_1")]
        public int UserId1 { get; set; }
        public User User1 { get; set; }

        [ForeignKey("User2")]
        [Column("user_id_2")]
        public int UserId2 { get; set; }
        public User User2 { get; set; }

        [Required]
        [StringLength(150)]
        [Column("last_message")]
        public string LastMessage { get; set; } = string.Empty;

        [Column("last_message_date")]
        public DateTime LastMessageDate { get; set; }

    }
}
