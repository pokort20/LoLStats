using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoLStats
{
    public class ViewModelMainWin : ViewModelSupport
    {
        //general variables
        public ObservableCollection<ChampionViewModel> Champions { get; set; } = new ObservableCollection<ChampionViewModel>();
        public ObservableCollection<SavedAccountViewModel> SavedAccounts { get; set; } = new ObservableCollection<SavedAccountViewModel>();
        public SavedAccountsModel savedAccountsModel;
        private string summonerName;
        private string region;
        private bool saveAccount;
        private Command _searchSummoner;
        private Command _close;
        private Command _minimize;
        private string displayedSummonerName;
        private string icon;
        private long summonerLevel;
        private const int MAX_CHAMPIONS_DISPLAYED = 15;
        //Ranked SOLO/DUO variables
        private string tier;
        private string rank;
        private int leaguePoints;
        private int wins;
        private int losses;
        private double winRatio;
        private string tierIcon;
        //Ranked FLEX variables
        private string tierF;
        private string rankF;
        private int leaguePointsF;
        private int winsF;
        private int lossesF;
        private double winRatioF;
        private string tierIconF;
        private readonly Dictionary<string, int> rankToDigit = new Dictionary<string, int>()
        {
            ["I"] = 1,
            ["II"] = 2,
            ["III"] = 3,
            ["IV"] = 4
        };
        public ViewModelMainWin()
        {
            savedAccountsModel = new SavedAccountsModel(this);
            SavedAccounts = savedAccountsModel.ReadFile();
            Icon = "../../Data/defaultIcon.png";
            TierIcon = "../../Data/TierIcons/default.png";
            TierIconF = "../../Data/TierIcons/default.png";
        }
        public Command SearchSummonerCommand
        {
            get
            {
                return _searchSummoner ?? (_searchSummoner = new Command(SearchSummoner, SearchSummonerCanExecute));
            }
        }
        public Command MinimizeCommand
        {
            get
            {
                return _minimize ?? (_minimize = new Command(Minimize, MinimizeCanExecute));
            }
        }

        private bool MinimizeCanExecute(object obj)
        {
            return true;
        }

        private void Minimize(object obj)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

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
            savedAccountsModel.SaveFile(SavedAccounts);
            Application.Current.Shutdown();
        }

        private bool SearchSummonerCanExecute(object obj)
        {
            if (region != null && summonerName != null && summonerName!="") return true;
            else
            {
                Debug.WriteLine("Summoner name or region is empty!");
                return false;
            }
        }
        private void SearchSummoner(object obj)
        {
            Debug.WriteLine("Searching for summoner " + summonerName + " in " + region + " region.");
            Api RiotGamesApi = new Api();
            SummonerModel summmonerModel = RiotGamesApi.GetSummonerByName(region, summonerName);
            if (summmonerModel == null)
            {
                Debug.WriteLine("An error has occured while searching for this summoner");
                ErrorWindow ew = new ErrorWindow();
                ViewModelErrorWindow viewModelErrorWindow = new ViewModelErrorWindow();
                ew.DataContext = viewModelErrorWindow;
                ew.ShowDialog();
                return;
            }
            if(SaveAccount)
            {
                SavedAccounts.Add(new SavedAccountViewModel(this, Region, SummonerName));
            }
            Icon = "https://opgg-static.akamaized.net/images/profile_icons/profileIcon" + summmonerModel.ProfileIconId + ".jpg";
            SummonerLevel = summmonerModel.SummonerLevel;
            DisplayedSummonerName = summmonerModel.Name;

            List<LeagueInfoModel> leagueInfoModel = RiotGamesApi.GetLeagueInfoBySummonerID(region, summmonerModel.Id);
            if (leagueInfoModel == null || leagueInfoModel.Count == 0)
            {
                Debug.WriteLine("Couldn't load League_v4 from RiotGames Api");
                DisplayDefault();
            }
            else
            {
                DisplayDefault();     
                foreach(LeagueInfoModel model in leagueInfoModel)
                {
                    if(model.QueueType == "RANKED_SOLO_5x5")
                    {
                        Tier = model.Tier;
                        Rank = model.Rank;
                        //TierIcon = "https://opgg-static.akamaized.net/images/medals/" + GetTierIcon(Tier, Rank) + ".png";
                        TierIcon = "../../Data/TierIcons/" + GetTierIcon(Tier, Rank) + ".png";
                        LeaguePoints = model.LeaguePoints;
                        Wins = model.Wins;
                        Losses = model.Losses;
                        WinRatio = Math.Round((double)Wins / (double)(Wins + Losses) * 100.0, 2);
                    }
                    if(model.QueueType == "RANKED_FLEX_SR")
                    {
                        TierF = model.Tier;
                        RankF = model.Rank;
                        //TierIconF = "https://opgg-static.akamaized.net/images/medals/" + GetTierIcon(Tier, Rank) + ".png";
                        TierIconF = "../../Data/TierIcons/" + GetTierIcon(TierF, RankF) + ".png";
                        LeaguePointsF = model.LeaguePoints;
                        WinsF = model.Wins;
                        LossesF = model.Losses;
                        WinRatioF = Math.Round((double)WinsF / (double)(WinsF + LossesF) * 100.0, 2);
                    }
                }
            }
            List<ChampionModel> championsModel = RiotGamesApi.GetChampionsBySummonerID(region, summmonerModel.Id);
            if(championsModel == null || championsModel.Count == 0)
            {
                Debug.WriteLine("Couldn't load Champions_V4 from RiotGames Api");
                Champions.Clear();
            }
            else
            {
                Champions.Clear();
                foreach (ChampionModel model in championsModel)
                {
                    if (Champions.Count >= MAX_CHAMPIONS_DISPLAYED) break;
                    Champions.Add(new ChampionViewModel(this, model.ChampionId, model.ChampionPoints, model.ChampionLevel));
                }
            }
        }
        public void DisplayDefault()
        {
            Tier = "UNRANKED";
            Rank = "";
            //TierIcon = "https://opgg-static.akamaized.net/images/medals/" + GetTierIcon(Tier, Rank) + ".png";
            TierIcon = "../../Data/TierIcons/" + GetTierIcon(Tier, Rank) + ".png";
            LeaguePoints = 0;
            Wins = 0;
            Losses = 0;
            WinRatio = 0;
            TierF = "UNRANKED";
            RankF = "";
            //TierIconF = "https://opgg-static.akamaized.net/images/medals/" + GetTierIcon(Tier, Rank) + ".png";
            TierIconF = "../../Data/TierIcons/" + GetTierIcon(Tier, Rank) + ".png";
            LeaguePointsF = 0;
            WinsF = 0;
            LossesF = 0;
            WinRatioF = 0;
        }
        private string GetTierIcon (string tier, string rank)
        {
            string ret;
            if (rank == "")
            {
                ret = "default";
                return ret;
            }
            ret = tier.ToLower() + "_" + rankToDigit[rank];
            return ret;
        }
        //GENERAL
        public string SummonerName
        {
            get { return summonerName; }
            set { summonerName = value; OnPropertyChanged(); }
        }
        public string Region
        {
            get { return region; }
            set { region = value; OnPropertyChanged(); }
        }
        public bool SaveAccount
        {
            get { return saveAccount; }
            set { saveAccount = value; OnPropertyChanged(); }
        }
        public string Icon
        {
            get { return icon; }
            set { icon = value; OnPropertyChanged(); }
        }
        public string DisplayedSummonerName
        {
            get { return displayedSummonerName; }
            set { displayedSummonerName = value; OnPropertyChanged(); }
        }
        public long SummonerLevel
        {
            get { return summonerLevel; }
            set { summonerLevel = value; OnPropertyChanged(); }
        }
        //SOLO/DUO
        public string Tier
        {
            get { return tier; }
            set { tier = value; OnPropertyChanged(); }
        }
        public string Rank
        {
            get { return rank; }
            set { rank = value; OnPropertyChanged(); }
        }
        public int LeaguePoints
        {
            get { return leaguePoints; }
            set { leaguePoints = value; OnPropertyChanged(); }
        }
        public int Wins
        {
            get { return wins; }
            set { wins = value; OnPropertyChanged(); }
        }
        public int Losses
        {
            get { return losses; }
            set { losses = value; OnPropertyChanged(); }
        }
        public double WinRatio
        {
            get { return winRatio; }
            set { winRatio = value; OnPropertyChanged(); }
        }
        public string TierIcon
        {
            get { return tierIcon; }
            set { tierIcon = value; Debug.WriteLine("TierIcon url: " + TierIcon); OnPropertyChanged(); }
        }
        //FLEX
        public string TierF
        {
            get { return tierF; }
            set { tierF = value; OnPropertyChanged(); }
        }
        public string RankF
        {
            get { return rankF; }
            set { rankF = value; OnPropertyChanged(); }
        }
        public int LeaguePointsF
        {
            get { return leaguePointsF; }
            set { leaguePointsF = value; OnPropertyChanged(); }
        }
        public int WinsF
        {
            get { return winsF; }
            set { winsF = value; OnPropertyChanged(); }
        }
        public int LossesF
        {
            get { return lossesF; }
            set { lossesF = value; OnPropertyChanged(); }
        }
        public double WinRatioF
        {
            get { return winRatioF; }
            set { winRatioF = value; OnPropertyChanged(); }
        }
        public string TierIconF
        {
            get { return tierIconF; }
            set { tierIconF = value; OnPropertyChanged(); }
        }
    }
}
