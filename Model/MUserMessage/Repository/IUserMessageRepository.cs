using ConstradeApi.Entity;

namespace ConstradeApi.Model.MUserMessage.Repository
{
    public interface IUserMessageRepository
    {
        Task<UserMessageModel> AddMessage(UserMessageModel userMessage);
        Task<UserMessageModel?> GetMessageById(int id);
        Task<IEnumerable<UserMessageModel>> GetMessageByUserChatId(int id, int index);
        Task<bool> DeleteMessageById(int id);
    }
}
