using ConstradeApi.Entity;
using ConstradeApi.Enums;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MSubcription.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly DataContext _context;

        public SubscriptionRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        /// <summary>
        /// POST: Creating a user subscription for registration
        /// </summary>
        /// <param name="uid"></param>
        public async Task CreateSubscription(int uid)
        {
            Subscription _s = new Subscription()
            {
                UserId = uid,
                SubscriptionType = "free",
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now,
                Amount = 0,
            };
            await _context.Subscriptions.AddAsync(_s);
            await _context.SaveChangesAsync();

            SubscriptionHistory _sh = new SubscriptionHistory()
            {
                SubscriptionId = _s.SubscriptionId,
                DateUpdate = DateTime.Now,
                PreviousSubscriptionType = _s.SubscriptionType,
                NewSubscriptionType = _s.SubscriptionType,
                PreviousDateStart = _s.DateStart,
                PreviousDateEnd = _s.DateEnd,
                NewDateStart = _s.DateStart,
                NewDateEnd = _s.DateEnd,
                PreviousAmount = _s.Amount,
                NewAmount = _s.Amount,
            };

            await _context.SubscriptionsHistory.AddAsync(_sh);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// PUT: when the user want to upgrade the subscription type
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>true if success otherwise false</returns>
        public async Task<SubscriptionResponseType> SubscribePremium(int uid)
        {
            User? _user = await _context.Users.FindAsync(uid);

            if (_user == null) return SubscriptionResponseType.UserNotFound;
            if (!_user.UserType.Equals("verified")) return SubscriptionResponseType.NotFullyVerified;

            Subscription sub = await _context.Subscriptions.Where(_s => _s.UserId.Equals(uid)).FirstAsync();
            //already a premium
            if (sub.SubscriptionType.Equals("premium")) return SubscriptionResponseType.AlreadyPremium;

            Wallet walletUser = _context.UserWallet.Where(_w => _w.UserId == sub.UserId).First();

            //not enough balance
            if (walletUser.Balance < 100) return SubscriptionResponseType.NotEnoughBalance;

            DateTime datetime = DateTime.Now;
            DateTime expiredDateTime = datetime.AddDays(30);
            string newSubscription = "premium";
            //TODO: this is a temporary amount
            int amount = 100;

            sub.SubscriptionType = newSubscription;
            sub.DateStart = datetime;
            sub.DateEnd = expiredDateTime;
            sub.Amount = amount;

            SubscriptionHistory subH = await _context.SubscriptionsHistory.Where(_sh => _sh.SubscriptionId.Equals(sub.SubscriptionId)).FirstAsync();
            subH.DateUpdate = datetime;
            subH.NewDateStart = datetime;
            subH.NewDateEnd = expiredDateTime;
            subH.NewAmount = amount;
            subH.NewSubscriptionType = newSubscription;

            walletUser.Balance -= amount;

            await _context.OtherTransactions.AddAsync(new OtherTransaction
            {
                WalletId = walletUser.WalletId,
                Amount = amount,
                Date = DateTime.Now,
                TransactionType = OtherTransactionType.Subscribe,
            });

            _user.UserType = newSubscription;
            _user.CountPost += 10;

            await _context.SaveChangesAsync();
            return SubscriptionResponseType.Success;
        }

        /// <summary>
        /// PUT: cancelling the premium subscription type of the user
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>false if the user is already free otherwise true</returns>
        public async Task<bool> CancelPremium(int uid)
        {
            Subscription sub = await _context.Subscriptions.Where(_s => _s.UserId.Equals(uid)).FirstAsync();
            if (sub.SubscriptionType.Equals("free")) return false;

            User user = await _context.Users.Where(_u => _u.UserId == uid).FirstAsync();
            user.UserType = "verified";

            DateTime datetime = DateTime.Now;
            DateTime expiredDateTime = DateTime.Now;
            string newSubscription = "free";
            //TODO: this is a temporary amount
            int amount = 0;

            sub.SubscriptionType = newSubscription;
            sub.DateStart = datetime;
            sub.DateEnd = expiredDateTime;
            sub.Amount = amount;
            _context.SaveChanges();

            SubscriptionHistory subH = await _context.SubscriptionsHistory.Where(_sh => _sh.SubscriptionId.Equals(sub.SubscriptionId)).FirstAsync();
            subH.DateUpdate = datetime;
            subH.PreviousSubscriptionType = subH.NewSubscriptionType;
            subH.NewSubscriptionType = newSubscription;
            subH.PreviousDateStart = subH.NewDateStart;
            subH.NewDateStart = datetime;
            subH.PreviousDateEnd = subH.NewDateEnd;
            subH.NewDateEnd = expiredDateTime;
            subH.PreviousAmount = subH.NewAmount;
            subH.NewAmount = amount;
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// GET: getting the subscription history of the user
        /// </summary>
        /// <param name="uid"></param>
        /// <returns> SubscriptionHistoryModel or NULL </returns>
        public async Task<SubscriptionHistoryModel?> GetSubscriptionHistoryByUserId(int uid)
        {
            Subscription? sub = await _context.Subscriptions.Where(_s => _s.UserId.Equals(uid)).FirstOrDefaultAsync();

            if (sub == null) return null;

            return await _context.SubscriptionsHistory.Where(_sh => _sh.SubscriptionId.Equals(sub.SubscriptionId))
                .Select(_sh => _sh.ToModel()).FirstOrDefaultAsync();
        }

        /// <summary>
        /// GET: getting the current subscription of the user
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>SubscriptionModel or NULL</returns>
        public async Task<SubscriptionModel?> GetSubscriptionByUserId(int uid)
        {
            return await _context.Subscriptions.Where(_s => _s.UserId.Equals(uid))
                .Select(sub => sub.ToModel()).FirstOrDefaultAsync();
        }
    }
}
