using ConstradeApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("other_transaction")]
    public class OtherTransaction
    {
        [Key,Required]
        [Column("other_transaction_id")]
        public int OtherTransactionId { get; set; }

        [ForeignKey("Wallet")]
        [Column("wallet_id")]
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }

        [Required]
        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("transction_type")]
        public OtherTransactionType TransactionType { get; set; }

        [Column("date")]
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
