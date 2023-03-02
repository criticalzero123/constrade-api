using ConstradeApi.Model.MProductChat;
using ConstradeApi.Model.MProductChat.Repository;
using ConstradeApi.Model.MProductMessage;
using ConstradeApi.Model.MProductMessage.Repository;
using Microsoft.AspNetCore.SignalR;

namespace ConstradeApi.Hubs
{
    public class ProductChatHub : Hub
    {
        private readonly IProductMessageRepository _productMessage;
        private readonly IProductChatRepository _productChat;

        public ProductChatHub(IProductMessageRepository productMessage, IProductChatRepository productChat)
        {
            _productMessage = productMessage;
            _productChat = productChat;
        }

        public async Task SendMessage(int senderId, int receiverId, int productId, string message)
        {
            try
            {
                int? productChat = await _productChat.GetProductChatId(senderId, receiverId, productId);

                int _productChatId = productChat == null ? await _productChat.AddProductChat(senderId, receiverId, productId, message)
                                                     : (int)productChat;

                ProductMessageModel _messageModel = new ProductMessageModel
                {
                    ProductChatId = _productChatId,
                    SenderId = senderId,
                    Message = message,
                    DateSent = DateTime.Now
                };

                var _messageNew = await _productMessage.AddMessage(_messageModel);
                ProductChatModel _updated = await _productChat.UpdateLastMessage(_productChatId, message);


                await Clients.User(senderId.ToString()).SendAsync("ProductReceiveMessage", _messageNew);
                await Clients.User(receiverId.ToString()).SendAsync("ProductReceiveMessage", _messageNew);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
