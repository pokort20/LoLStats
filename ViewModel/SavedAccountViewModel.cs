using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLStats
{
    public class SavedAccountViewModel : ViewModelSupport
    {
        private string _accountName;
        private string _region;
        private Command _searchSavedAccount;
        private Command _removeSavedAccount;
        private ViewModelMainWin _viewModelMainWinReference;

        public SavedAccountViewModel(ViewModelMainWin viewModelMainWinReference, string region, string accountName)
        {
            _viewModelMainWinReference = viewModelMainWinReference;
            AccountName = accountName;
            Region = region;
        }
        public string AccountName
        {
            get { return _accountName; }
            set { _accountName = value; OnPropertyChanged(); }
        }
        public string Region
        {
            get { return _region; }
            set { _region = value; OnPropertyChanged(); }
        }
        public Command SearchSavedAccountCommand
        {
            get
            {
                return _searchSavedAccount ?? (_searchSavedAccount = new Command(SearchSavedAccount, SearchSavedAccountCanExecute));
            }
        }
        public Command RemoveSavedAccountCommand
        {
            get
            {
                return _removeSavedAccount ?? (_removeSavedAccount = new Command(RemoveSavedAccount, RemoveSavedAccountCanExecute));
            }
        }

        private bool RemoveSavedAccountCanExecute(object obj)
        {
            return true;
        }

        private void RemoveSavedAccount(object obj)
        {
            _viewModelMainWinReference.SavedAccounts.Remove(this);
        }

        private bool SearchSavedAccountCanExecute(object obj)
        {
            return true;
        }

        private void SearchSavedAccount(object obj)
        {
            _viewModelMainWinReference.Region = Region;
            _viewModelMainWinReference.SummonerName = AccountName;
            _viewModelMainWinReference.SaveAccount = false;
            _viewModelMainWinReference.SearchSummonerCommand.Execute(null);
        }
    }
}
