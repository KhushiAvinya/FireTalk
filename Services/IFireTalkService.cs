
using FireTalk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireTalk.Services
{
    public interface IFireTalkService
    {

        Task<bool> SaveUserDataAsync(UserModel user);
        Task<UserModel?> LoginAsync(string email, string password);
        Task<List<UserModel>> GetAllUsers();
        Task<bool> CreateMessageGroup(MessageGroupModel payload);
        Task<bool> CreateChatMessage(ChatModel payload);
        Task<List<MessageGroupModel>> GetUserGroups(string userId);
        Task<List<ChatModel>> GetUsersChats(string groupId, int pagesize);
        Task<List<UserModel>> GetUserDetailsByIds(List<string> userIds);
        Task<UserModel?> GetUserByIdAsync(string userId);
        Task<bool> UpdateUserAsync(UserModel user);
        Task NotifyTyping(string groupId, string userId, bool isTyping);
        Task<MessageGroupModel> GetGroupById(string groupId);

        //void ListenToUserGroups(string userId, Action<MessageGroupModel> onGroupUpdated);
        //Task MarkGroupAsRead(string userId, string groupId);
         //Task<DateTime?> GetLastReadTime(string userId, string groupId);

    }
}
