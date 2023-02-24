using ConstradeApi.Entity;

namespace ConstradeApi.Model.MUserMessage.Repository
{
    public class UserMessageRepository : IUserMessageRepository
    {
        private readonly DataContext _context;
        public UserMessageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddMessage(UserMessageModel userMessage)
        {
            UserMessage message = new UserMessage()
            {
                UserChatId = userMessage.UserChatId,
                SenderId = userMessage.SenderId,
                Message = userMessage.Message,
                DateSent = userMessage.DateSent,
            };

            await _context.UserMessage.AddAsync(message);
            await _context.SaveChangesAsync();
        }
        public Task<IEnumerable<UserMessageModel>> GetByUserChatId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
