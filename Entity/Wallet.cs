using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("user_wallet")]
    public class Wallet
    {
        [Key]
        [Column("wallet_id")]
        public int WalletId { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [Column("balance")]
        public decimal Balance { get; set; } = 0;
    }
}
