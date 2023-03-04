using ConstradeApi.Entity;
using ConstradeApi.Enums;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MSubcription.Repository
{
    public interface ISubscriptionRepository
    {
        Task CreateSubscription(int uid);
        Task<SubscriptionResponseType> SubscribePremium(int uid);
        Task<bool> CancelPremium(int uid);
        Task<SubscriptionHistoryModel?> GetSubscriptionHistoryByUserId(int uid);
        Task<SubscriptionModel?> GetSubscriptionByUserId(int uid);
    }
}
