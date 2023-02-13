using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLStats
{
    public class ChampionViewModel : ViewModelSupport
    {
        private ViewModelMainWin _viewModelMainWinRef;
        private long _championID;
        private int _championPoints;
        private int _championLevel;
        private Command _openBuildsCommand;
        private Command _openCountersCommand;
        private string _iconSource;
        private readonly Dictionary<long, string> champIdToName = new Dictionary<long, string>()
        {
            [0] = "WrongChampionID",
            [266] = "Aatrox",
            [103] = "Ahri",
            [84] = "Akali",
            [12] = "Alistar",
            [32] = "Amumu",
            [34] = "Anivia",
            [1] = "Annie",
            [523] = "Aphelios",
            [22] = "Ashe",
            [136] = "AurelionSol",
            [268] = "Azir",
            [432] = "Bard",
            [53] = "Blitzcrank",
            [63] = "Brand",
            [201] = "Braum",
            [51] = "Caitlyn",
            [164] = "Camille",
            [69] = "Cassiopeia",
            [31] = "Chogath",
            [42] = "Corki",
            [122] = "Darius",
            [131] = "Diana",
            [119] = "Draven",
            [36] = "DrMundo",
            [245] = "Ekko",
            [60] = "Elise",
            [28] = "Evelynn",
            [81] = "Ezreal",
            [9] = "Fiddlesticks",
            [114] = "Fiora",
            [105] = "Fizz",
            [3] = "Galio",
            [41] = "Gangplank",
            [86] = "Garen",
            [150] = "Gnar",
            [79] = "Gragas",
            [104] = "Graves",
            [120] = "Hecarim",
            [74] = "Heimerdinger",
            [420] = "Illaoi",
            [39] = "Irelia",
            [427] = "Ivern",
            [40] = "Janna",
            [59] = "JarvanIV",
            [24] = "Jax",
            [126] = "Jayce",
            [202] = "Jhin",
            [222] = "Jinx",
            [145] = "Kaisa",
            [429] = "Kalista",
            [43] = "Karma",
            [30] = "Karthus",
            [38] = "Kassadin",
            [55] = "Katarina",
            [10] = "Kayle",
            [141] = "Kayn",
            [85] = "Kennen",
            [121] = "Khazix",
            [203] = "Kindred",
            [240] = "Kled",
            [96] = "KogMaw",
            [7] = "Leblanc",
            [64] = "LeeSin",
            [89] = "Leona",
            [876] = "Lillia",
            [127] = "Lissandra",
            [236] = "Lucian",
            [117] = "Lulu",
            [99] = "Lux",
            [54] = "Malphite",
            [90] = "Malzahar",
            [57] = "Maokai",
            [11] = "MasterYi",
            [21] = "MissFortune",
            [62] = "MonkeyKing",
            [82] = "Mordekaiser",
            [25] = "Morgana",
            [267] = "Nami",
            [75] = "Nasus",
            [111] = "Nautilus",
            [518] = "Neeko",
            [76] = "Nidalee",
            [56] = "Nocturne",
            [20] = "Nunu",
            [2] = "Olaf",
            [61] = "Orianna",
            [516] = "Ornn",
            [80] = "Pantheon",
            [78] = "Poppy",
            [555] = "Pyke",
            [246] = "Qiyana",
            [133] = "Quinn",
            [497] = "Rakan",
            [33] = "Rammus",
            [421] = "RekSai",
            [526] = "Rell",
            [58] = "Renekton",
            [107] = "Rengar",
            [92] = "Riven",
            [68] = "Rumble",
            [13] = "Ryze",
            [360] = "Samira",
            [113] = "Sejuani",
            [235] = "Senna",
            [147] = "Seraphine",
            [875] = "Sett",
            [35] = "Shaco",
            [98] = "Shen",
            [102] = "Shyvana",
            [27] = "Singed",
            [14] = "Sion",
            [15] = "Sivir",
            [72] = "Skarner",
            [37] = "Sona",
            [16] = "Soraka",
            [50] = "Swain",
            [517] = "Sylas",
            [134] = "Syndra",
            [223] = "TahmKench",
            [163] = "Taliyah",
            [91] = "Talon",
            [44] = "Taric",
            [17] = "Teemo",
            [412] = "Thresh",
            [18] = "Tristana",
            [48] = "Trundle",
            [23] = "Tryndamere",
            [4] = "TwistedFate",
            [29] = "Twitch",
            [77] = "Udyr",
            [6] = "Urgot",
            [110] = "Varus",
            [67] = "Vayne",
            [45] = "Veigar",
            [161] = "Velkoz",
            [254] = "Vi",
            [112] = "Viktor",
            [8] = "Vladimir",
            [106] = "Volibear",
            [19] = "Warwick",
            [498] = "Xayah",
            [101] = "Xerath",
            [5] = "XinZhao",
            [157] = "Yasuo",
            [777] = "Yone",
            [83] = "Yorick",
            [350] = "Yuumi",
            [154] = "Zac",
            [238] = "Zed",
            [115] = "Ziggs",
            [26] = "Zilean",
            [142] = "Zoe",
            [143] = "Zyra",
        };
        public ChampionViewModel (ViewModelMainWin viewModelMainWin, long championID, int championPoints, int championLevel)
        {
            _viewModelMainWinRef = viewModelMainWin;
            ChampionId = championID;
            ChampionPoints = championPoints;
            ChampionLevel = championLevel;
            IconSource = "";
        }
        public long ChampionId
        {
            get { return _championID; }
            set { _championID = value; OnPropertyChanged(); }
        }
        public int ChampionPoints
        {
            get { return _championPoints; }
            set { _championPoints = value; OnPropertyChanged(); }
        }
        public int ChampionLevel
        {
            get { return _championLevel; }
            set { _championLevel = value; OnPropertyChanged(); }
        }
        public string IconSource
        {
            get { return _iconSource; }
            set 
            { 
                  //Loading icons from web _iconSource = "https://ddragon.leagueoflegends.com/cdn/10.25.1/img/champion/"+ champIdToName[ChampionId] + ".png"; Debug.WriteLine("IconSource: " + IconSource); OnPropertyChanged();
                  _iconSource ="../../Data/ChampionIcons/"  + champIdToName[ChampionId] + ".png"; Debug.WriteLine("IconSource: " + IconSource); OnPropertyChanged();
                  //Different set of icons _iconSource ="../../Data/ChampionIcons/"  + champIdToName[ChampionId] + ".jpg"; Debug.WriteLine("IconSource: " + IconSource); OnPropertyChanged();

            }
        }
        public Command OpenBuildsCommand
        {
            get
            {
                return _openBuildsCommand ?? (_openBuildsCommand = new Command(OpenBuilds, OpenBuildsCanExecute));
            }
        }
        public Command OpenCountersCommand
        {
            get
            {
                return _openCountersCommand ?? (_openCountersCommand = new Command(OpenCounters, OpenCountersCanExecute));
            }
        }

        private bool OpenCountersCanExecute(object obj)
        {
            return true;
        }

        private void OpenCounters(object obj)
        {
            if(ChampionId == 62) Process.Start("https://lolcounter.com/champions/" + "Wukong");
            else Process.Start("https://lolcounter.com/champions/" + champIdToName[ChampionId]);
        }

        private void OpenBuilds(object obj)
        {
            Process.Start("https://www.probuilds.net/champions/details/" + ChampionId);
        }

        private bool OpenBuildsCanExecute(object obj)
        {
            return true;
        }
    }
}
