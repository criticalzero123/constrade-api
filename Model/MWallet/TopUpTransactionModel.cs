using ConstradeApi.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.Model.MWallet
{
    public class TopUpTransactionModel
    {
        public int TopUpTransactionId { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTopUp { get; set; }
    }
}
