using FireTalk.Components.Layout;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireTalk.Models
{
    [FirestoreData]
    public class ChatModel
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string GroupId { get; set; }
        [FirestoreProperty]
        public string Text { get; set; }
        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; }
        [FirestoreProperty]
        public string OwnerId { get; set; }
        public bool IsSender { get => OwnerId == MainLayout._userInfo.Id; }
        public string OwnerName { get; set; }
        public string OwnerBackGroundColor{get; set;}

    }
}
