
using ConstradeApi.Model.MProduct;
using ConstradeApi.Model.MUser;

namespace ConstradeApi.Model.MUserNotification.Repository
{
    public interface IUserNotificationRepository
    {
        Task SendNotificationToFollowerPosting(IEnumerable<int> followModel, int id, int ownerId);
        Task SendNotificationFollow(int userFollower, int userFollowed);
        Task<IEnumerable<UserNotificationModel>> GetNotifications(int userId);
    }
}
