using ConstradeApi.Entity;

namespace ConstradeApi.Model.MWallet
{
    public class WalletUserDetailModel
    {
        public int WalletId { get; set; }
        public User User { get; set; }
    }
}
