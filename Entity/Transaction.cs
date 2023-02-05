using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("transactions")]
    public class Transaction
    {
        [Key]
        [Column("transaction_id")]
        public int TransactionId { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Buyer")]
        [Column("buyer_user_id")]
        public int BuyerUserId { get; set; }
        public User Buyer { get; set; }

        [ForeignKey("Seller")]
        [Column("seller_user_id")]
        public int SellerUserId { get; set; }
        public User Seller { get; set; }

        [Column("in_app_transaction")]
        public bool InAppTransaction { get; set; }

        [Column("get_wanted")]
        public bool GetWanted { get; set; }

        [Column("date_transaction")]
        public DateTime DateTransaction { get; set; }
    }
}
