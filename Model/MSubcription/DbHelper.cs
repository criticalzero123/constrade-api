using ConstradeApi.Entity;

namespace ConstradeApi.Model.MSubcription
{
    public class DbHelper
    {
        private readonly DataContext _context;

        public DbHelper(DataContext dataContext)
        {
            _context = dataContext;
        }

        //public async Task<bool> CreateSubscription(SubscriptionHistoryModel info)
        //{
        //    Subscription? user = await _context.Subscriptions.Where(_s => _s.UserId == );

        //    if (user == null) return false;

        //    user.SubscriptionType = info.SubscriptionType;
        //    await _context.SaveChangesAsync();

        //    SubscriptionHistory sb = new SubscriptionHistory()
        //    {
        //        UserId= info.UserId,
        //        SubscriptionType = info.SubscriptionType,
        //        DateEnd= info.DateEnd,
        //        Status= info.Status,
        //    };

        //    await _context.SubscriptionsHistory.AddAsync(sb);
        //    await _context.SaveChangesAsync();

        //    return true;
        //}
        //public async Task<List<SubscriptionHistoryModel>?> GetSubscriptionHistoryByUser(int uid)
        //{
        //    User? user= await _context.Users.FindAsync(uid);

        //    if (user == null) return null;

        //    List<SubscriptionHistoryModel> data = _context.SubscriptionsHistory.Where(_s => _s.UserId.Equals(uid)).Select(_s => new SubscriptionHistoryModel()
        //    {
        //        SubscriptionHistoryId= _s.SubscriptionHistoryId,
        //        UserId = _s.UserId,
        //        SubscriptionType = _s.SubscriptionType,
        //        DateStarted = _s.DateStarted,
        //        DateEnd= _s.DateEnd,
        //        Status= _s.Status,
        //        DateUpdated= _s.DateUpdated,
        //    }).ToList();

        //    return data;
        //}
        //public SubscriptionHistoryModel? GetSubscriptionHistoryById(int id)
        //{
        //    SubscriptionHistory? _s = _context.SubscriptionsHistory.Find(id);

        //    if(_s == null) return null;

        //    return new SubscriptionHistoryModel()
        //    {
        //        SubscriptionHistoryId= _s.SubscriptionHistoryId,
        //        UserId= _s.UserId,
        //        SubscriptionType = _s.SubscriptionType,
        //        DateStarted = _s.DateStarted,
        //        DateEnd= _s.DateEnd,
        //        Status = _s.Status,
        //        DateUpdated = _s.DateUpdated,
        //    };
        //}
        //public async Task<bool> CancelSubscription(int )
    }
}
