using ConstradeApi.Entity;
using ConstradeApi.Model.MUserMessage;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MProductMessage.Repository
{
    public class ProductMessageRepository : IProductMessageRepository
    {
        private readonly DataContext _context;

        public ProductMessageRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ProductMessageModel> AddMessage(ProductMessageModel productMessageModel)
        {
            ProductMessage message = new ProductMessage()
            {
                ProductChatId = productMessageModel.ProductChatId,
                SenderId = productMessageModel.SenderId,
                DateSent = productMessageModel.DateSent,
                Message = productMessageModel.Message,

            };

            await _context.ProductMessages.AddAsync(message);
            await _context.SaveChangesAsync();

            return message.ToModel();
        }

        public async Task<bool> DeleteMessageById(int id)
        {
            ProductMessage? message = await _context.ProductMessages.FindAsync(id);

            if (message == null) return false;

            _context.ProductMessages.Remove(message);
            _context.SaveChanges();

            return true;
        }

        public async Task<ProductMessageModel?> GetProductMessageById(int id)
        {
            ProductMessage? message = await _context.ProductMessages.FindAsync(id);

            if (message == null) return null;

            return message.ToModel();
            
        }

        public async Task<IEnumerable<ProductMessageModel>> GetProductMessagesByProductChatId(int id, int index)
        {
            //if the index is 0 then return takeCount otherwise index * takeCount
            int takeCount = 10;
            int skipIndex = index == 0 ? 0 : index * takeCount;

            IEnumerable<ProductMessageModel> messages = await _context.ProductMessages.OrderByDescending(_m => _m.DateSent)
                                                            .Where(_m => _m.ProductChatId == id)
                                                            .Skip(skipIndex)
                                                            .Take(takeCount)
                                                            .Select(_m => _m.ToModel())
                                                            .ToListAsync();

            return messages;
        }
    }
}
