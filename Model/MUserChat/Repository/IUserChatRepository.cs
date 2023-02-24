using ConstradeApi.Entity;

namespace ConstradeApi.Model.MUserChat.Repository
{
    public interface IUserChatRepository
    {
        Task<UserChatModel?> GetUserChatById(int userId1, int userId2);
        Task<int> AddUserChat(int userId1, int userId2, string message);
        Task UpdateLastMessage(int chatId, string message);
        Task<IEnumerable<UserChatModel>> GetAllUsersChats();
    }
}
