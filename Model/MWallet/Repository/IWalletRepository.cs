using ConstradeApi.Entity;
using ConstradeApi.Enums;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MWallet.Repository
{
    public interface IWalletRepository
    {
         Task CreateWalletUser(int uid);
         Task<WalletModel?> GetWalletUser(int uid);
         Task<IEnumerable<WalletUserDetailModel>> GetAllWalletUserDetails();

         Task<WalletModel?> GetWalletById(int id);

         Task<WalletResponseType> SendMoneyUser(SendMoneyTransactionModel info);
         Task<IEnumerable<SendMoneyTransactionModel>> GetAllTransactionWallet(int userId);
         Task<IEnumerable<SendMoneyTransactionModel>> GetReceiveMoneyTransaction(int walletId);

         Task<IEnumerable<SendMoneyTransactionModel>> GetSendMoneyTransaction(int walletId);

         Task<IEnumerable<SendMoneyTransactionModel>> GetAllMoneyTransaction();

         Task<SendMoneyTransactionModel?> GetWalletTransactionById(int id);

         Task<bool> TopUpMoney(OtherTransactionModel info);

         Task<IEnumerable<OtherTransactionModel>> GetTopUpByWalletId(int walletId);
    }
}
