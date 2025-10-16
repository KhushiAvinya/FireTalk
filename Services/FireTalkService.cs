using FireTalk.Models;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FireTalk.Services
{
    public class FireTalkService : IFireTalkService
    {
        private readonly FirestoreDb _firestoreDb;
        //private readonly HttpClient _http;
        //private string baseUrl = "https://firetalk01-f1879-default-rtdb.firebaseio.com/";

        public FireTalkService(FireStoreService fireStoreService)
        {
            _firestoreDb = fireStoreService.Db;
            //_http = http;
        }
        public async Task<List<UserModel>> GetAllUsers()
        {
            var usersRef = _firestoreDb.Collection("users");
            var querySnapshot = await usersRef.GetSnapshotAsync();
            return querySnapshot.Documents.Select(doc => doc.ConvertTo<UserModel>()).ToList();
        }

        public async Task<UserModel?> LoginAsync(string email, string password)
        {
            var usersRef = _firestoreDb.Collection("users");
            var query = usersRef.WhereEqualTo("Email", email);
            var querySnapshot = await query.GetSnapshotAsync();
            var doc = querySnapshot.Documents.FirstOrDefault();
            if (doc != null)
            {
                var user = doc.ConvertTo<UserModel>();
                if (user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }
        private async Task<bool> IsUserExistAysnc(string email)
        {
            var usersRef = _firestoreDb.Collection("users");
            var query = usersRef.WhereEqualTo("Email", email);
            var querySnapshot = await query.GetSnapshotAsync();
            return querySnapshot.Count > 0;
        }
        public async Task<bool> SaveUserDataAsync(UserModel user)
        {
            //var json = JsonSerializer.Serialize(user);
            //var response = await _http.PutAsync($"{baseUrl}users/{user.Id}.json", new StringContent(json, Encoding.UTF8, "application/json"));
            //response.EnsureSuccessStatusCode();
            if (await IsUserExistAysnc(user.Email))
            {
                return false; // User already exists
            }
            var docRef = _firestoreDb.Collection("users").Document(user.Id);
            await docRef.SetAsync(user);
            return true; // User saved successfully
        }


        public async Task<bool> CreateMessageGroup(MessageGroupModel payload)
        {
            var docRef = _firestoreDb.Collection("groups").Document(payload.Id);
            await docRef.SetAsync(payload);
            return true;
        }
        public async Task<bool> CreateChatMessage(ChatModel payload)
        {
            var docRef = _firestoreDb.Collection("chats").Document(payload.Id);
            await docRef.SetAsync(payload);
            return true;
        }
        public async Task<List<MessageGroupModel>> GetUserGroups(string userId)
        {
            var docRef = _firestoreDb.Collection("groups");
            var query = docRef.WhereArrayContains("Member", userId);
            var snapshots = await query.GetSnapshotAsync();
            return snapshots.Documents.Select(d => d.ConvertTo<MessageGroupModel>()).ToList();

        }
        public async Task<List<ChatModel>> GetUsersChats(string groupId, int pagesize)
        {
            var docRef = _firestoreDb.Collection("chats");
            var query = docRef.WhereEqualTo("GroupId", groupId)
                .OrderByDescending("CreatedAt")
                .Limit(pagesize);
            var snapshots = await query.GetSnapshotAsync();
            return snapshots.Documents.Select(d => d.ConvertTo<ChatModel>()).ToList();
        }
        public async Task<List<UserModel>> GetUserDetailsByIds(List<string> userIds)
        {
            var userRef = _firestoreDb.Collection("users");
            var query = userRef.WhereIn("Id", userIds);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<UserModel>()).ToList();
        }
        public async Task<UserModel?> GetUserByIdAsync(string userId)
        {
            var docRef = _firestoreDb.Collection("users").Document(userId);
            var snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
                return snapshot.ConvertTo<UserModel>();

            return null; // User not found
        }

        public async Task<bool> UpdateUserAsync(UserModel user)
        {
            var docRef = _firestoreDb.Collection("users").Document(user.Id);
            await docRef.SetAsync(user, SetOptions.Overwrite);
            return true;
        }
        public async Task NotifyTyping(string groupId, string userId, bool isTyping)
        {
            var doc = _firestoreDb.Collection("typing").Document($"{groupId}_{userId}");
            await doc.SetAsync(new { GroupId = groupId, UserId = userId, IsTyping = isTyping, UpdatedAt = Timestamp.FromDateTime(DateTime.UtcNow) });
        }
        public async Task<MessageGroupModel> GetGroupById(string groupId)
{
    var doc = await _firestoreDb.Collection("messageGroups").Document(groupId).GetSnapshotAsync();
    if (doc.Exists)
    {
        return doc.ConvertTo<MessageGroupModel>();
    }
    return null;
}


        //public void ListenToUserGroups(string userId, Action<MessageGroupModel> onGroupUpdated)
        //{
        //    var groupsRef = _firestoreDb.Collection("groups")
        //                        .WhereArrayContains("Members", userId);

        //    groupsRef.Listen(snapshot =>
        //    {
        //        foreach (var change in snapshot.Changes)
        //        {
        //            if (change.ChangeType == DocumentChange.Type.Modified ||
        //                change.ChangeType == DocumentChange.Type.Added)
        //            {
        //                var group = change.Document.ConvertTo<MessageGroupModel>();
        //                group.Id = change.Document.Id;
        //                onGroupUpdated?.Invoke(group);
        //            }
        //        }
        //    });
        //}

        //public async Task MarkGroupAsRead(string userId, string groupId)
        //{
        //    var userReadRef = _firestoreDb
        //        .Collection("groups")
        //        .Document(groupId)
        //        .Collection("ReadStatus")
        //        .Document(userId);

        //    await userReadRef.SetAsync(new { LastReadTime = Timestamp.GetCurrentTimestamp() }, SetOptions.MergeAll);
        //}

        //public async Task<DateTime?> GetLastReadTime(string userId, string groupId)
        //{
        //    var docRef = _firestoreDb
        //        .Collection("groups")
        //        .Document(groupId)
        //        .Collection("ReadStatus")
        //        .Document(userId);

        //    var snapshot = await docRef.GetSnapshotAsync();
        //    if (snapshot.Exists && snapshot.TryGetValue("LastReadTime", out Timestamp ts))
        //    {
        //        return ts.ToDateTime();
        //    }
        //    return null;
        //}
    }
}
