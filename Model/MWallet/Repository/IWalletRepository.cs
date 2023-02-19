using ConstradeApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MWallet.Repository
{
    public interface IWalletRepository
    {
         Task CreateWalletUser(int uid);
         Task<WalletModel?> GetWalletUser(int uid);

         Task<WalletModel?> GetWalletById(int id);

         Task<bool> SendMoneyUser(SendMoneyTransactionModel info);

         Task<IEnumerable<SendMoneyTransactionModel>> GetReceiveMoneyTransaction(int walletId);

         Task<IEnumerable<SendMoneyTransactionModel>> GetSendMoneyTransaction(int walletId);

         Task<IEnumerable<SendMoneyTransactionModel>> GetAllMoneyTransaction();

         Task<SendMoneyTransactionModel?> GetWalletTransactionById(int id);

         Task<bool> TopUpMoney(TopUpTransactionModel info);

         Task<IEnumerable<TopUpTransactionModel>> GetTopUpByWalletId(int walletId);

         Task<IEnumerable<TopUpTransactionModel>> GetAllTopUpTransaction();

         Task<TopUpTransactionModel?> GetTopUpById(int id);

    }
}
