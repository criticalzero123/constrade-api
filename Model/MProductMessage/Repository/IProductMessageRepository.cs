namespace ConstradeApi.Model.MProductMessage.Repository
{
    public interface IProductMessageRepository
    {
        Task<ProductMessageModel> AddMessage(ProductMessageModel productMessageModel);
        Task<ProductMessageModel?> GetProductMessageById(int id);
        Task<IEnumerable<ProductMessageModel>> GetProductMessagesByProductChatId(int id, int index);
    }
}
