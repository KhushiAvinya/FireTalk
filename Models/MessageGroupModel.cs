using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireTalk.Models
{
    [FirestoreData]
    public class MessageGroupModel
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Title { get; set; }
        [FirestoreProperty]
        public string LastSentMessage { get; set; }
        [FirestoreProperty]
        public Timestamp LastSentTime { get; set; }
        [FirestoreProperty]
        public string OwnerId { get; set; }
        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; }
        [FirestoreProperty]
        public List<string> Member { get; set; }
        [FirestoreProperty] // optional if you want to sync with Firestore
        public int UnreadCount { get; set; } = 0;
    }
}
