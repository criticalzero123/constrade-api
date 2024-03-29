﻿using ConstradeApi.Entity;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Net.Mime.MediaTypeNames;

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
                LastMessageDate = DateTime.Now
            };

            await _context.UserChats.AddAsync(chat);
            await _context.SaveChangesAsync();

            return chat.UserChatId;
        }
        public async Task<IEnumerable<UserChatModel>> GetAllUsersChats()
        {
            return await _context.UserChats.Select(_u => _u.ToModel()).ToListAsync();
        }

        public async Task<IEnumerable<ChatResponseInfoModel>> GetUserByName(string name)
        {
            IEnumerable<ChatResponseInfoModel> users = await _context.Users.Include(_u => _u.Person)
                                                                           .Where(_u => _u.Person.FirstName.ToLower().Contains(name.ToLower()) || _u.Person.LastName.ToLower().Contains(name.ToLower()) || _u.Email.ToLower().Contains(name.ToLower()))
                                                                           .Select(_u => new ChatResponseInfoModel
                                                                           {
                                                                               User = _u,
                                                                               OtherUserName = $"{_u.Person.FirstName} {_u.Person.LastName}",
                                                                           })
                                                                           .ToListAsync();

            return users;
        }

        public async Task<int?> GetUserChatIdByUId(int userId1, int userId2)
        {
            UserChat? userChat = await _context.UserChats.Where(_u => (_u.UserId1 == userId1 && _u.UserId2 == userId2) ||
                                                          (_u.UserId1 == userId2 && _u.UserId2 == userId1)).FirstOrDefaultAsync();

            if (userChat == null) return null;
            return userChat.UserChatId;
        }
        public async Task<IEnumerable<ChatResponseInfoModel>> GetUserChatListByUId(int userId)
        {
            var chat = _context.UserChats
                            .Where(uc => uc.UserId1 == userId || uc.UserId2 == userId)
                            .Select(uc => new
                            {
                                uc.UserChatId,
                                OtherUserId = uc.UserId1 == userId ? uc.UserId2 : uc.UserId1,
                                uc.LastMessage,
                                uc.LastMessageDate,
                            });

            var chatList = await _context.Users
                                        .Join(chat,
                                            u => u.UserId,
                                            uc => uc.OtherUserId,
                                            (u, uc) => new ChatResponseInfoModel
                                            {
                                                UserChatId = uc.UserChatId,
                                                User = u,
                                                OtherUserName = $"{u.Person.FirstName} {u.Person.LastName}",
                                                LastMessage = uc.LastMessage,
                                                LastMessageDate = uc.LastMessageDate,
                                            })
                                        .OrderByDescending(_uc => _uc.LastMessageDate)
                                        .ToListAsync();

            return chatList;
        }
        public async Task<UserChatModel> UpdateLastMessage(int chatId, string message)
        {
            UserChat? chat = await _context.UserChats.FindAsync(chatId);
            
            chat!.LastMessage = message;
            chat!.LastMessageDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return chat.ToModel();
        }
    }
}
