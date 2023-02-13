using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoLStats
{
    public class ViewModelErrorWindow : ViewModelSupport
    {
        private Command _close;
        public Command CloseCommand
        {
            get
            {
                return _close ?? (_close = new Command(Close, CloseCanExecute));
            }
        }

        private bool CloseCanExecute(object obj)
        {
            return true;
        }

        private void Close(object obj)
        {
            Debug.WriteLine("ErrorWindow clicked");
            Application.Current.Windows[2].Close();
        }
    }
}
