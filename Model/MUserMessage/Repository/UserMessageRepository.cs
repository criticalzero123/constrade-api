using ConstradeApi.Entity;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MUserMessage.Repository
{
    public class UserMessageRepository : IUserMessageRepository
    {
        private readonly DataContext _context;
        public UserMessageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserMessageModel> AddMessage(UserMessageModel userMessage)
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

            return message.ToModel();
        }

        public async Task<UserMessageModel?> GetMessageById(int id)
        {
            UserMessage?  _temp = await _context.UserMessage.FindAsync(id);

            return _temp?.ToModel();
        }

        public async Task<IEnumerable<UserMessageModel>> GetMessageByUserChatId(int id, int index)
        {
            //if the index is 0 then return 0 otherwise index * takeCount
            int takeCount = 10; 
            int skipIndex = index == 0  ? 0 : index * takeCount;

            IEnumerable<UserMessageModel> messages = await _context.UserMessage.OrderByDescending(_m => _m.DateSent)
                                                            .Where(_m => _m.UserChatId == id)
                                                            .Skip(skipIndex)
                                                            .Take(takeCount)
                                                            .Select(_m => _m.ToModel())
                                                            .ToListAsync();

            return messages;
        }
    }
}
