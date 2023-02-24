
namespace ConstradeApi.Model.MUserChat
{
    public class UserChatModel
    {

        public int UserChatId { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public string LastMessage { get; set; } = string.Empty;
        public DateTime LastMessageDate { get; set; }
    }
}
