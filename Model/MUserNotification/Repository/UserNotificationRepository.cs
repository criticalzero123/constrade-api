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

        public async Task SendNotificationFollow(int userFollower, int userFollowed)
        {
            User user = await _context.Users.Include(_u => _u.Person).Where(_u => _u.UserId == userFollower).FirstAsync();

            string message = $"{user.Person.FirstName} {user.Person.LastName} started following you.";

            await _context.Notification.AddAsync(new UserNotification()
            {
                UserId = userFollowed,
                ToId = userFollower,
                ImageUrl = user.ImageUrl,
                NotificationMessage = message,
                NotificationType = "follow",
                NotificationDate = DateTime.Now,
            }); ;

            await _context.SaveChangesAsync();
        }

        public async Task SendNotificationToFollowerPosting(IEnumerable<int> ids, int somethingId, int ownerId)
        {
            if (ids.Count() == 0) return;

            User user = await _context.Users.Include(_u => _u.Person).Where(_u => _u.UserId == ownerId).FirstAsync();

            string message = $"{user.Person.FirstName} {user.Person.LastName} posted a new Item.";

            foreach (int id in ids)
            {
                _context.Notification.Add(new UserNotification
                {
                    UserId = id,
                    ToId = somethingId,
                    ImageUrl = user.ImageUrl,
                    NotificationMessage= message,
                    NotificationType = "post",
                    NotificationDate= DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task SendNotificationTransaction(int sellerUserId, int buyerUserId, int transactionId)
        {
            UserNotification _u = new UserNotification
            {
                UserId = sellerUserId,
                ToId = transactionId,
                ImageUrl = "",
                NotificationMessage= "Transaction completed.",
                NotificationType="transaction",
                NotificationDate= DateTime.Now
            };

            await _context.Notification.AddAsync(_u);

            UserNotification _u1 = new UserNotification
            {
                UserId = buyerUserId,
                ToId = sellerUserId,
                ImageUrl = "",
                NotificationMessage = "Transaction completed. You can review him now.",
                NotificationType = "review",
                NotificationDate = DateTime.Now
            };

            await _context.Notification.AddAsync(_u1);

            await _context.SaveChangesAsync();

        }
    }
}
