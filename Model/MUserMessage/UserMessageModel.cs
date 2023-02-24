
namespace ConstradeApi.Model.MUserMessage
{
    public class UserMessageModel
    {
        public int UserMessageId { get; set; }
        public int UserChatId { get; set; }
        public int SenderId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime DateSent { get; set; }
    }
}
