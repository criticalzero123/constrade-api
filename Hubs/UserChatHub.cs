using ConstradeApi.Entity;
using ConstradeApi.Model.MUserChat;
using ConstradeApi.Model.MUserChat.Repository;
using ConstradeApi.Model.MUserMessage;
using ConstradeApi.Model.MUserMessage.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ConstradeApi.Hubs
{
    [Authorize]
    public class UserChatHub : Hub
    {
        private readonly IUserChatRepository _userChatRepository;
        private readonly IUserMessageRepository _userMessageRepository;

        public UserChatHub(IUserChatRepository userRepo, IUserMessageRepository chatRepo)
        {
            _userChatRepository = userRepo;
            _userMessageRepository = chatRepo;
        }

        public async Task SendMessage(int senderId, int receiverId, string receiverEmail, string message)
        {
            UserChatModel? userChat = await _userChatRepository.GetUserChatById(senderId, receiverId);

            
            int chatId = userChat == null ? await _userChatRepository.AddUserChat(senderId, receiverId, message) : 
                                           userChat.UserChatId;

            UserMessageModel _message = new UserMessageModel
            {
                UserChatId = chatId,
                SenderId = senderId,
                Message = message,
                DateSent= DateTime.UtcNow
            };

            await _userMessageRepository.AddMessage(_message);
            await _userChatRepository.UpdateLastMessage(chatId, message);


            var userId = Context.User.FindFirst(ClaimTypes.Name)?.Value;
            var fromUserEmail = Context.User.FindFirst(ClaimTypes.Email)?.Value;

            var fromUserId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await Clients.User(receiverEmail).SendAsync("ReceiveMessage", message);
            await Clients.User(fromUserId).SendAsync("ReceiveMessage", message);


        }

        //If the connection will be useful in the near future
        //public override async Task OnConnectedAsync()
        //{
        //    var userId = int.Parse(Context.UserIdentifier);
        //    var connectionId = Context.ConnectionId;

        //    // Store the user's connection ID in the UserConnection table
        //    await _userChatRepository.AddUserConnectionAsync(userId, connectionId);
        //}

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    var userId = int.Parse(Context.UserIdentifier);

        //    // Remove the user's connection ID from the UserConnection table
        //    await _userChatRepository.RemoveUserConnectionAsync(userId);

        //    await base.OnDisconnectedAsync(exception);
        //}
    }
}
