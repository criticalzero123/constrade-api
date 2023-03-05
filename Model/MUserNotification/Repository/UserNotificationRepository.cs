using ConstradeApi.Entity;
using ConstradeApi.Model.MProduct;
using ConstradeApi.Model.MUser;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MUserNotification.Repository
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly DataContext _context;

        public UserNotificationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserNotificationModel>> GetNotifications(int userId)
        {
            IEnumerable<UserNotificationModel> notif = await _context.Notification.Where(_f => _f.UserId == userId).Select(_f => _f.ToModel()).ToListAsync();

            return notif;
        }

        public async Task SendNotificationToFollower(IEnumerable<int> ids, int productId, int ownerId)
        {
            if (ids.Count() == 0) return;

            User user = await _context.Users.Include(_u => _u.Person).Where(_u => _u.UserId == ownerId).FirstAsync();

            string message = $"{user?.Person.FirstName} {user?.Person.LastName} posted a new Item.";

            foreach (int id in ids)
            {
                _context.Notification.Add(new UserNotification
                {
                    UserId = id,
                    ProductId = productId,
                    NotificationMessage= message,
                    NotificationType = "post",
                    NotificationDate= DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
