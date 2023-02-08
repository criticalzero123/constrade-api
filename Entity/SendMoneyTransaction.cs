using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("send_money_transaction")]
    public class SendMoneyTransaction
    {
        [Key, Required]
        [Column("send_money_transaction")]
        public int SendMoneyTransactionId { get; set; }

        [ForeignKey("Wallet1")]
        [Column("sender_wallet_id")]
        public int SenderWalletId { get; set; }
        public Wallet Wallet1 { get; set; }

        [ForeignKey("Wallet2")]
        [Column("receiver_wallet_id")]
        public int ReceiverWalletId {get; set; }
        public Wallet Wallet2 { get; set;}

        [Required]
        [Column("amount")]
        public decimal Amount { get; set; }

        [Required]
        [Column("date_send")]
        public DateTime DateSend { get; set; } = DateTime.Now;
    }
}
