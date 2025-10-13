using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireTalk.Services
{
    public class FireStoreService
    {
        private readonly FirestoreDb _fireStoreDb;

        public FireStoreService()
        {
            var stream = FileSystem.OpenAppPackageFileAsync("chat-app-01-25f3c-firebase-adminsdk-fbsvc-7160c1b199.json").Result;
            //var stream = FileSystem.OpenAppPackageFileAsync("firebase_admin_sdk.json").Result;
            using var reader = new StreamReader(stream);
            var content = reader.ReadToEnd();

            var clientBuilder = new FirestoreClientBuilder
            {
                JsonCredentials = content
            };
            _fireStoreDb=FirestoreDb.Create("chat-app-01-25f3c", clientBuilder.Build());
            //_fireStoreDb=FirestoreDb.Create("firetalk01-f1879", clientBuilder.Build());
        }
        public FirestoreDb Db => _fireStoreDb;
    }
}
