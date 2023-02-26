using ConstradeApi.Entity;

namespace ConstradeApi.Model.MUserChat
{
    public class ChatResponseInfoModel
    {
        public int UserChatId { get; set; }
        public User User { get; set; }
        public string OtherUserName { get; set; } = string.Empty;
        public string LastMessage { get; set; } = string.Empty;
        public DateTime LastMessageDate { get; set; }
    }
}
