using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("product_message")]
    public class ProductMessage
    {
        [Key]
        [Column("product_message_id")]
        public int ProductMessageId { get; set; }

        [ForeignKey("Chat")]
        [Column("product_chat_id")]
        public int ProductChatId { get; set; }
        public ProductChat Chat { get; set; }

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
