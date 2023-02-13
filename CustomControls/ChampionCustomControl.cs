using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LoLStats
{
    public class ChampionCustomControl : ToggleButton
    {
        static ChampionCustomControl ()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChampionCustomControl), new FrameworkPropertyMetadata(typeof(ChampionCustomControl)));
        }
        public string ChampionName
        {
            get
            {
                return (string)GetValue(ChampionNameProperty);
            }
            set
            {
                SetValue(ChampionNameProperty, value);
            }
        }
        public int ChampionPoints
        {
            get
            {
                return (int)GetValue(ChampionPointsProperty);
            }
            set
            {
                SetValue(ChampionPointsProperty, value);
            }
        }
        public int ChampionLevel
        {
            get
            {
                return (int)GetValue(ChampionLevelProperty);
            }
            set
            {
                SetValue(ChampionLevelProperty, value);
            }
        }
        public ICommand OpenBuilds
        {
            get
            {
                return (ICommand)GetValue(OpenBuildsProperty);
            }
            set
            {
                SetValue(OpenBuildsProperty, value);
            }
        }
        public ICommand OpenCounters
        {
            get
            {
                return (ICommand)GetValue(OpenCountersProperty);
            }
            set
            {
                SetValue(OpenCountersProperty, value);
            }
        }
        public string IconSource
        {
            get
            {
                return (string)GetValue(IconSourceProperty);
            }
            set
            {
                SetValue(IconSourceProperty, value);
            }
        }
        public static readonly DependencyProperty ChampionNameProperty =
             DependencyProperty.Register("ChampionName", typeof(string), typeof(ChampionCustomControl));
        public static readonly DependencyProperty ChampionPointsProperty =
             DependencyProperty.Register("ChampionPoints", typeof(int), typeof(ChampionCustomControl));
        public static readonly DependencyProperty ChampionLevelProperty =
             DependencyProperty.Register("ChampionLevel", typeof(int), typeof(ChampionCustomControl));
        public static readonly DependencyProperty OpenBuildsProperty =
             DependencyProperty.Register("OpenBuilds", typeof(ICommand), typeof(ChampionCustomControl));
        public static readonly DependencyProperty OpenCountersProperty =
             DependencyProperty.Register("OpenCounters", typeof(ICommand), typeof(ChampionCustomControl));
        public static readonly DependencyProperty IconSourceProperty =
             DependencyProperty.Register("IconSource", typeof(string), typeof(ChampionCustomControl));

    }
}
