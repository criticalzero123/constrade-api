namespace ConstradeApi.Model.MSubcription
{
    public class SubscriptionHistoryModel
    {
        public int SubscriptionHistoryId { get; set; }
        public int UserId { get; set; }
        public string SubscriptionType { get; set; } = string.Empty;
        public DateTime DateStarted { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
