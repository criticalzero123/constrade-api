
namespace ConstradeApi.Model.MSubcription
{
    public class SubscriptionModel
    {
        public int SubscriptionId { get; set; }
        public int UserId { get; set; }
        public string SubscriptionType { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal Amount { get; set; }
    }
}
