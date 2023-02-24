using ConstradeApi.Entity;

namespace ConstradeApi.Model.MUserMessage.Repository
{
    public interface IUserMessageRepository
    {
        Task AddMessage(UserMessageModel userMessage);
        Task<IEnumerable<UserMessageModel>> GetByUserChatId(int id);
    }
}
