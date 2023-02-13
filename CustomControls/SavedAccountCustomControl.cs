using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LoLStats
{
    public class SavedAccountCustomControl : ToggleButton
    {
        static SavedAccountCustomControl ()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SavedAccountCustomControl), new FrameworkPropertyMetadata(typeof(SavedAccountCustomControl)));
        }
        public string AccountName
        {
            get
            {
                return (string)GetValue(AccountNameProperty);
            }
            set
            {
                SetValue(AccountNameProperty, value);
            }
        }
        public string Region
        {
            get
            {
                return (string)GetValue(RegionProperty);
            }
            set
            {
                SetValue(RegionProperty, value);
            }
        }
        public ICommand SearchAccountCommand
        {
            get
            {
                return (ICommand)GetValue(SearchAccountCommandProperty);
            }
            set
            {
                SetValue(SearchAccountCommandProperty, value);
            }
        }
        public ICommand RemoveSavedAccountCommand
        {
            get
            {
                return (ICommand)GetValue(RemoveSavedAccountCommandProperty);
            }
            set
            {
                SetValue(RemoveSavedAccountCommandProperty, value);
            }
        }
        public static readonly DependencyProperty AccountNameProperty =
             DependencyProperty.Register("AccountName", typeof(string), typeof(SavedAccountCustomControl));
        public static readonly DependencyProperty RegionProperty =
             DependencyProperty.Register("Region", typeof(string), typeof(SavedAccountCustomControl));
        public static readonly DependencyProperty SearchAccountCommandProperty =
             DependencyProperty.Register("SearchAccountCommand", typeof(ICommand), typeof(SavedAccountCustomControl));
        public static readonly DependencyProperty RemoveSavedAccountCommandProperty =
             DependencyProperty.Register("RemoveSavedAccountCommand", typeof(ICommand), typeof(SavedAccountCustomControl));
    }
}
