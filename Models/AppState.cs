using System;

namespace FireTalk.Models
{
    public class AppState
    {
        private UserModel? _currentUser;
        public event Action<UserModel?>? NotifyUserChanged;
        public UserModel? CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                NotifyUserChanged?.Invoke(_currentUser);
            }
        }
        public object? CurrentData { get; set; }
    }
}
