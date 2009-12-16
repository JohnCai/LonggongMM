using Longgong.mm.Models;
using Mavis.MVVM;

namespace Longgong.mm.ViewModels
{
    public class LogonViewModel: ViewModelBase
    {
        private UserAuthentication _currentUserAuthentication;

        public UserAuthentication CurrentUserAuthentication
        {
            get { return _currentUserAuthentication; }
            set
            {
                _currentUserAuthentication = value;
                NotifyPropertyChanged("CurrentUserAuthentication");
            }
        }
    }
}