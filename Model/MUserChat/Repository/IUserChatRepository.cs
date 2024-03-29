﻿using ConstradeApi.Entity;

namespace ConstradeApi.Model.MUserChat.Repository
{
    public interface IUserChatRepository
    {
        Task<int?> GetUserChatIdByUId(int userId1, int userId2);
        Task<IEnumerable<ChatResponseInfoModel>> GetUserChatListByUId(int userId);
        Task<int> AddUserChat(int userId1, int userId2, string message);
        Task<UserChatModel> UpdateLastMessage(int chatId, string message);
        Task<IEnumerable<ChatResponseInfoModel>> GetUserByName(string name);
        Task<IEnumerable<UserChatModel>> GetAllUsersChats();
    }
}
