using ConstradeApi.Entity;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MProductChat.Repository
{
    public class ProductChatRepository : IProductChatRepository
    {
        private readonly DataContext _context;

        public ProductChatRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<int> AddProductChat(int userId1, int userId2, int productId, string message)
        {
            ProductChat chat = new ProductChat()
            {
                UserId1 = userId1,
                UserId2 = userId2,
                ProductId = productId,
                LastMessage = message,
                LastMessageDate = DateTime.Now,
                ChatCreated = DateTime.Now,
            };

            await _context.ProductChat.AddAsync(chat);
            await _context.SaveChangesAsync();

            return chat.ProductChatId;
        }

        public async Task<int?> GetProductChatId(int userId1, int userId2, int productId)
        {
            ProductChat? productChatId = await _context.ProductChat.Where(_pc  => ((_pc.UserId1 == userId1 && _pc.UserId2 == userId2) || 
                                                                            (_pc.UserId1 == userId2 && _pc.UserId2 == userId1)) && 
                                                                            _pc.ProductId == productId)
                                                          .FirstOrDefaultAsync();
            if (productChatId == null) return null;

            return productChatId.ProductChatId;
        }

        public async Task<IEnumerable<ProductChatResponseInfo>> GetUserChatListById(int userId)
        {
            var productChat = _context.ProductChat.Where(pc => pc.UserId1 == userId || pc.UserId2 == userId)
                                                  .Select(pc => new
                                                  {
                                                      pc.ProductChatId,
                                                      pc.UserId1,
                                                      pc.Product,
                                                      OtherUserId = pc.UserId1 == userId ? pc.UserId2 : pc.UserId1,
                                                      pc.LastMessage,
                                                      pc.LastMessageDate,
                                                  });

            var productChatList = await _context.Users.Join(productChat,
                                                            u => u.UserId,
                                                            pc => pc.OtherUserId,
                                                            (u, pc) => new ProductChatResponseInfo
                                                            {
                                                                ProductChatId = pc.ProductChatId,
                                                                User = u,
                                                                Product = pc.Product,
                                                                OtherUserName = $"{u.Person.FirstName} {u.Person.LastName}",
                                                                LastMessage = pc.LastMessage,
                                                                LastMessageDate = pc.LastMessageDate,
                                                            }).ToListAsync();

            return productChatList; 
        }

        public async Task<ProductChatModel> UpdateLastMessage(int productChatId, string message)
        {
            ProductChat? chat = await _context.ProductChat.FindAsync(productChatId);

            chat!.LastMessage = message;
            chat!.LastMessageDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return chat.ToModel();
        }
    }
}
