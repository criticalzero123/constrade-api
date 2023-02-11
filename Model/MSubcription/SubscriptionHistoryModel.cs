
namespace ConstradeApi.Model.MSubcription
{
    public class SubscriptionHistoryModel
    {
        public int SubscriptionHistoryId { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime DateUpdate { get; set; }
        public string PreviousSubscriptionType { get; set; } = string.Empty;
        public string NewSubscriptionType { get; set; } = string.Empty;
        public DateTime PreviousDateStart { get; set; }
        public DateTime NewDateStart { get; set; }
        public DateTime PreviousDateEnd { get; set; }
        public DateTime NewDateEnd { get; set; }
        public decimal PreviousAmount { get; set; }
        public decimal NewAmount { get; set; }
    }
}
