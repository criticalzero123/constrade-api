using ConstradeApi.Entity;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MUserChat.Repository
{
    public class UserChatRepository : IUserChatRepository
    {
        private readonly DataContext _context;
        public UserChatRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> AddUserChat(int userId1, int userId2, string message)
        {
            UserChat chat = new UserChat
            {
                UserId1 = userId1,
                UserId2 = userId2,
                LastMessage = message,
                LastMessageDate = DateTime.UtcNow
            };

            await _context.UserChats.AddAsync(chat);
            await _context.SaveChangesAsync();

            return chat.UserChatId;
        }
        public async Task<IEnumerable<UserChatModel>> GetAllUsersChats()
        {
            return await _context.UserChats.Select(_u => _u.ToModel()).ToListAsync();
        }
        public async Task<UserChatModel?> GetUserChatById(int userId1, int userId2)
        {
            UserChat? userChat = await _context.UserChats.Where(_u => (_u.UserId1 == userId1 && _u.UserId2 == userId2) ||
                                                                (_u.UserId1 == userId2 && _u.UserId2 == userId1)).FirstOrDefaultAsync();
        
            if(userChat == null) return null;
            return userChat.ToModel();
        }
        public async Task UpdateLastMessage(int chatId, string message)
        {
            UserChat? chat = await _context.UserChats.FindAsync(chatId);

            chat.LastMessage = message;
            chat.LastMessageDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
