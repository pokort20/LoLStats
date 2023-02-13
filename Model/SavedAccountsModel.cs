using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLStats
{
    public class SavedAccountsModel
    {
        /*
         * This class contains functions used for saving and loading
         * saved accounts from a file. May you move the saved_accounts.txt
         * file elsewhere, please adjust the filePath variable accordingly.
         * 
         */
        public ViewModelMainWin ViewModelMainWinReference { get; set; }
        private const string filePath = "../../Data/saved_accounts.txt";

        public SavedAccountsModel (ViewModelMainWin viewModelMainWinReference)
        {
            ViewModelMainWinReference = viewModelMainWinReference;
        }
        public ObservableCollection<SavedAccountViewModel> ReadFile()
        {
            ObservableCollection<SavedAccountViewModel> SavedAccounts = new ObservableCollection<SavedAccountViewModel>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    try
                    {
                        string line;
                        string[] data = new string[3];
                        while ((line = sr.ReadLine()) != null)
                        {
                            data = line.Split(';');
                            SavedAccounts.Add(new SavedAccountViewModel(ViewModelMainWinReference, data[0], data[1]));
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("An error has occured while reading the saved accounts\n" +
                            "Exception message: " + e.Message);
                    }
                }
            }
            return SavedAccounts;
        }
        public void SaveFile(ObservableCollection<SavedAccountViewModel> SavedAccounts)
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                foreach (SavedAccountViewModel savedAccount in SavedAccounts)
                {
                    sw.WriteLine(savedAccount.Region + ";" + savedAccount.AccountName);
                }
            }
        }
    }
}
