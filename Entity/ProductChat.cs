using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("product_chat")]
    public class ProductChat
    {
        [Key]
        [Column("product_chat_id")]
        public int ProductChatId { get; set; }

        [ForeignKey("User1")]
        [Column("user_id_1")]
        public int UserId1 { get; set; }
        public User User1 { get; set; }

        [ForeignKey("User2")]
        [Column("user_id_2")]
        public int UserId2 { get; set; }
        public User User2 { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        [Column("last_message")]
        public string LastMessage { get; set; } = string.Empty;

        [Required]
        [Column("last_message_date")]
        public DateTime LastMessageDate { get; set; }

        [Required]
        [Column("chat_created")]
        public DateTime ChatCreated { get; set; }
    }
}
