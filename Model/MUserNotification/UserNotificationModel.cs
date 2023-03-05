namespace ConstradeApi.Model.MUserNotification
{
    public class UserNotificationModel
    {

        public int UserNotificationId { get; set; }
        public int UserId { get; set; }
        public int ToId { get; set; }
        public string NotificationType { get; set; } = string.Empty;
        public string NotificationMessage { get; set; } = string.Empty;
    }
}
