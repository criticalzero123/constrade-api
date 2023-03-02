namespace ConstradeApi.Model.MProductChat.Repository
{
    public interface IProductChatRepository
    {
        Task<int?> GetProductChatId(int userId1, int userId2, int productId);
        Task<IEnumerable<ProductChatResponseInfo>> GetUserChatListById(int userId);
        Task<int> AddProductChat(int userId1, int userId2, int productId, string message);
        Task<ProductChatModel> UpdateLastMessage(int productChatId, string message);

    }
}
