using ConstradeApi.Entity;

namespace ConstradeApi.Model.MProductChat
{
    public class ProductChatResponseInfo
    {
        public int ProductChatId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public string OtherUserName { get; set; } = string.Empty;
        public string LastMessage { get; set; } = string.Empty;
        public DateTime LastMessageDate { get; set; }
    }
}
