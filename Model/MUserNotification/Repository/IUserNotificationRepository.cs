
using ConstradeApi.Model.MProduct;
using ConstradeApi.Model.MUser;

namespace ConstradeApi.Model.MUserNotification.Repository
{
    public interface IUserNotificationRepository
    {
        Task SendNotificationToFollower(IEnumerable<int> followModel, int productId, int ownerId);
        Task<IEnumerable<UserNotificationModel>> GetNotifications(int userId);
    }
}
