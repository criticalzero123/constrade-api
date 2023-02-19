using ConstradeApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MWallet.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly DataContext _context;

        public WalletRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        /// <summary>
        /// POST: Creating wallet when the user registers to the system
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task CreateWalletUser(int uid)
        {
            Wallet _wallet = new Wallet()
            {
                UserId = uid,
            };

            await _context.UserWallet.AddAsync(_wallet);
            await _context.SaveChangesAsync();

        }
        /// <summary>
        /// GET: Getting the wallet info of the user
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>Null if the userid not exist, WalletModel if the user found</returns>
        public async Task<WalletModel?> GetWalletUser(int uid)
        {
            WalletModel? _wallet = await _context.UserWallet.Where(_u => _u.UserId.Equals(uid)).Select(_w => new WalletModel()
            {
                WalletId = _w.WalletId,
                UserId = _w.UserId,
                Balance = _w.Balance,
            }).FirstOrDefaultAsync();

            return _wallet;
        }

        /// <summary>
        /// GET: getting the Wallet by using wallet id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<WalletModel?> GetWalletById(int id)
        {
            Wallet? data = await _context.UserWallet.FindAsync(id);

            if (data == null) return null;

            return new WalletModel()
            {

                WalletId = data.WalletId,
                UserId = data.UserId,
                Balance = data.Balance,
            };
        }
        /// <summary>
        /// POST: Sending money 
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true if the send transaction success otherwise false</returns>
        public async Task<bool> SendMoneyUser(SendMoneyTransactionModel info)
        {
            if (info.SenderWalletId == info.ReceiverWalletId) return false;

            Wallet? _receiver = await _context.UserWallet.FindAsync(info.ReceiverWalletId);
            Wallet? _sender = await _context.UserWallet.FindAsync(info.SenderWalletId);

            if (_receiver == null || _sender == null) return false;
            if (_sender.Balance < info.Amount) return false;

            //Deducting the balance of the sender
            _sender.Balance -= info.Amount;
            await _context.SaveChangesAsync();

            //Adding the balance of the receiver
            _receiver.Balance += info.Amount;
            await _context.SaveChangesAsync();

            SendMoneyTransaction _transaction = new SendMoneyTransaction()
            {

                SenderWalletId = info.SenderWalletId,
                ReceiverWalletId = info.ReceiverWalletId,
                Amount = info.Amount,
            };

            await _context.SendMoneyTransactions.AddAsync(_transaction);
            await _context.SaveChangesAsync();

            return true;
        }
        /// <summary>
        /// GET: Getting the receive transactions in wallet
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>List of SendMoneyTransactionModel</returns>
        public async Task<IEnumerable<SendMoneyTransactionModel>> GetReceiveMoneyTransaction(int walletId)
        {
            List<SendMoneyTransactionModel> data = await _context.SendMoneyTransactions.Where(_t => _t.ReceiverWalletId.Equals(walletId)).Select(_t => new SendMoneyTransactionModel()
            {
                SendMoneyTransactionId = _t.SendMoneyTransactionId,
                ReceiverWalletId = _t.ReceiverWalletId,
                SenderWalletId = _t.SenderWalletId,
                Amount = _t.Amount,
                DateSend = _t.DateSend,
            }).ToListAsync();

            return data;
        }
        /// <summary>
        /// GET: Getting the user send money transaction in wallet
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>List of SendMoneyTransactionModel</returns>
        public async Task<IEnumerable<SendMoneyTransactionModel>> GetSendMoneyTransaction(int walletId)
        {
            List<SendMoneyTransactionModel> data = await _context.SendMoneyTransactions.Where(_t => _t.SenderWalletId.Equals(walletId)).Select(_t => new SendMoneyTransactionModel()
            {
                SendMoneyTransactionId = _t.SendMoneyTransactionId,
                ReceiverWalletId = _t.ReceiverWalletId,
                SenderWalletId = _t.SenderWalletId,
                Amount = _t.Amount,
                DateSend = _t.DateSend,
            }).ToListAsync();

            return data;
        }
        /// <summary>
        /// GET: Getting all the transaction of the users
        /// </summary>
        /// <returns>List of SendMoneyTransactionModel</returns>
        public async Task<IEnumerable<SendMoneyTransactionModel>> GetAllMoneyTransaction()
        {
            return await _context.SendMoneyTransactions.Select(_t => new SendMoneyTransactionModel()
            {
                SendMoneyTransactionId = _t.SendMoneyTransactionId,
                ReceiverWalletId = _t.ReceiverWalletId,
                SenderWalletId = _t.SenderWalletId,
                Amount = _t.Amount,
                DateSend = _t.DateSend,
            }).ToListAsync();
        }
        /// <summary>
        /// GET: Getting the wallet transaction using SendMoneyTransaction id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or SendMoneyTransactionModel</returns>
        public async Task<SendMoneyTransactionModel?> GetWalletTransactionById(int id)
        {
            SendMoneyTransaction? _transaction = await _context.SendMoneyTransactions.FindAsync(id);

            if (_transaction == null) return null;

            return new SendMoneyTransactionModel()
            {
                SendMoneyTransactionId = _transaction.SendMoneyTransactionId,
                Amount = _transaction.Amount,
                DateSend = _transaction.DateSend,
                ReceiverWalletId = _transaction.ReceiverWalletId,
                SenderWalletId = _transaction.SenderWalletId,
            };
        }
        /// <summary>
        /// POST: Creating a TopUpMoney Transaction and Updating the wallet amount
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true if the transaction is success otherwise false</returns>
        public async Task<bool> TopUpMoney(TopUpTransactionModel info)
        {
            Wallet? _wallet = await _context.UserWallet.FindAsync(info.WalletId);
            if (_wallet == null) return false;

            TopUpTransaction topUp = new TopUpTransaction()
            {
                WalletId = _wallet.WalletId,
                Amount = info.Amount
            };
            await _context.TopUpTransactions.AddAsync(topUp);
            await _context.SaveChangesAsync();

            _wallet.Balance += info.Amount;
            _context.SaveChanges();

            return true;
        }
        /// <summary>
        /// GET: Getting the list of top up transaction using wallet id
        /// </summary>
        /// <param name="walletId"></param>
        /// <returns>List of TopUpTransactionModel</returns>
        public async Task<IEnumerable<TopUpTransactionModel>> GetTopUpByWalletId(int walletId)
        {
            return await _context.TopUpTransactions.Where(_t => _t.WalletId.Equals(walletId)).Select(_t => new TopUpTransactionModel()
            {
                WalletId = _t.WalletId,
                Amount = _t.Amount,
                DateTopUp = _t.DateTopUp,
            }).ToListAsync();
        }
        /// <summary>
        /// GET: Getting all the top up transactions by the users
        /// </summary>
        /// <returns>List of TopUpTransactionModel</returns>
        public async Task<IEnumerable<TopUpTransactionModel>> GetAllTopUpTransaction()
        {
            return await _context.TopUpTransactions.Select(_t => new TopUpTransactionModel()
            {
                TopUpTransactionId = _t.TopUpTransactionId,
                WalletId = _t.WalletId,
                Amount = _t.Amount,
                DateTopUp = _t.DateTopUp,
            }).ToListAsync();
        }
        /// <summary>
        /// GET: Getting a specific top-up transaction by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or TopUpTransactionModel</returns>
        public async Task<TopUpTransactionModel?> GetTopUpById(int id)
        {
            TopUpTransaction? _transaction = await _context.TopUpTransactions.FindAsync(id);
            if (_transaction == null) return null;


            return new TopUpTransactionModel()
            {
                WalletId = _transaction.WalletId,
                Amount = _transaction.Amount,
                DateTopUp = _transaction.DateTopUp,
            };
        }
    }
}
