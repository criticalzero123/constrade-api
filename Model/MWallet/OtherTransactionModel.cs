using ConstradeApi.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ConstradeApi.Enums;

namespace ConstradeApi.Model.MWallet
{
    public class OtherTransactionModel
    {
        public int OtherTransactionId { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public OtherTransactionType TransactionType { get; set; }
        public DateTime Date { get; set; }
    }
}
