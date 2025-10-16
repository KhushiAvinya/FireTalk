using Google.Cloud.Firestore;
using System;

namespace FireTalk.Models
{
    [FirestoreData]  // Important
    public class UserModel
    {
        [FirestoreProperty] // marks property as storable in Firestore
        public string Id { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string Email { get; set; }

        [FirestoreProperty]
        public string Password { get; set; }

        [FirestoreProperty]
        public string CreatedAt { get; set; }  
        [FirestoreProperty]
public string? ProfilePictureUrl { get; set; }

        
    }
}
