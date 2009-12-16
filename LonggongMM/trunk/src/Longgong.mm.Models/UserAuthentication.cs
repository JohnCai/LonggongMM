using Mavis.MVVM;

namespace Longgong.mm.Models
{
    public class UserAuthentication : EditableObject
    {
        private bool _isAuthenticated;
        private string _password;
        private string _userID;
        private string _userName;

        public string UserID
        {
            get { return _userID; }
            set
            {
                _userID = value;
                NotifyPropertyChanged("UserID");
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyPropertyChanged("Password");
            }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set
            {
                _isAuthenticated = value;
                NotifyPropertyChanged("IsAuthenticated");
            }
        }
    }
}