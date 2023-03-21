using ConstradeApi.Entity;
using ConstradeApi.Model.MUser;

namespace ConstradeApi.Model.MWallet
{
    public class WalletUserDetailModel
    {
        public int WalletId { get; set; }
        public UserModel User { get; set; }
        public PersonModel Person { get; set; }
    }
}
