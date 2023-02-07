using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("top_up_transaction")]
    public class TopUpTransaction
    {
        [Key,Required]
        [Column("top_up_transaction")]
        public int TopUpTransactionId { get; set; }

        [ForeignKey("Wallet")]
        [Column("wallet_id")]
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }

        [Required]
        [Column("amount")]
        public decimal amount { get; set; }

        [Column("date_topup")]
        public DateTime DateTopUp { get; set; } = DateTime.Now;

    }
}
