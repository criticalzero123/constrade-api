using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("user_wallet")]
    public class Wallet
    {
        [Key, Required]
        [Column("wallet_id")]
        public int WalletId { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }


        [Column("balance")]
        public decimal balance { get; set; } = 0;
    }
}
