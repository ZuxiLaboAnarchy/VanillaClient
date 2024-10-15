using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Vanilla.Tomlyn;
using Vanilla.Tomlyn.Model;
using Vanilla.Tomlyn.Syntax;

namespace Vanilla.Config
{ 

    public class MainConfig
    {
        private static bool HasLoadedFirst = false; 
        public MainConfig() { 
       
        }
        private static MainConfig instance;

       private static readonly string FilePath = Path.Combine(FileHelper.GetCheatFolder(), "Vanilla.json");


        private bool _LoadMusic = true;
        public bool LoadMusic { get => _LoadMusic; set { _LoadMusic = value; Save(); } }

        private string _MusicPath = "";
        public string MusicPath { get => _MusicPath; set { _MusicPath = value; Save(); } }

        public bool _AutoFriends = false;

        public bool AutoFrends { get => _AutoFriends; set { _AutoFriends = value; Save(); } }

        private int _theme = 0;
        public int Theme { get => _theme; set { _theme = value; Save(); } }

        private bool _TestBool = true;
        public bool TestBool { get => _TestBool; set { _TestBool = value; Save(); } }

        private bool _UiRecolor = true;
        public bool UiRecolor { get => _UiRecolor; set { _UiRecolor = value; Save(); } }

        private string _PCCrashID = "";
        public string PCCrashID { get => _PCCrashID; set { _PCCrashID = value; Save(); } }
        
        private string _QuestCrashID = "";
        public string QuestCrashID { get => _QuestCrashID; set { _QuestCrashID = value; Save(); } }

        private bool _ESP = true;
        public bool ESP { get => _ESP; set { _ESP = value; Save(); } }

        private bool _JoinLogger = true;
        public bool JoinLogger { get => _JoinLogger; set { _JoinLogger = value; Save(); } }

        private bool _LogModerations = true;
        public bool LogModerations { get => _LogModerations; set { _LogModerations = value; Save(); } }

        private bool _NameplateWallhack = true;
        public bool NameplateWallhack { get => _NameplateWallhack; set { _NameplateWallhack = value; Save(); } }

        private bool _NameplateMoreInfo = true;
        public bool NameplateMoreInfo { get => _NameplateMoreInfo; set { _NameplateMoreInfo = value; Save(); } }

        private bool _ShowActorID = true;
        public bool ShowActorID { get => _ShowActorID; set { _ShowActorID = value; Save(); } }

        private bool _DetectLagOrCrash = true;
        public bool DetectLagOrCrash { get => _DetectLagOrCrash; set { _DetectLagOrCrash = value; Save(); } }

        private bool _AntiE1 = true;
        public bool AntiE1 { get => _AntiE1; set { _AntiE1 = value; Save(); } }
       
        private bool _ImageCache = true;
        public bool ImageCache { get => _ImageCache; set { _ImageCache = value; Save(); } }

        private bool _GlobalSyncCrasher = true;
        public bool GlobalSyncCrasher { get => _GlobalSyncCrasher; set { _GlobalSyncCrasher = value; Save(); } }

        [JsonConverter(typeof(ColorConverter))]
        private Color _GlobalColors = new Color(1.0f, 0.8196f, 0.8627f, 1.0f);

        [JsonConverter(typeof(ColorConverter))]
        public Color GlobalColors { get => _GlobalColors; set { _GlobalColors = value; Save(); } }

        private List<string> _WhiteListedAvtID = new List<string>();
        public List<string> WhiteListedAvatarIDs { get => _WhiteListedAvtID; set { _WhiteListedAvtID = value; Save(); } }

        private List<string> _WhiteListedShaderList = new List<string>();
        public List<string> WhiteListedShaderList { get => _WhiteListedShaderList; set { _WhiteListedShaderList = value; Save(); } }

        private string _ZuxiApiKey = "";
        public string ApiKey { get => _ZuxiApiKey; set { _ZuxiApiKey = value; Save(); } }

        public bool AntiFreezeExploit { get; internal set; } = false;
        public bool BlockAllRPCEvents { get; internal set; } = false;
        public List<string> BlockedPlayerEvents { get; internal set; }

        private bool _QuickMenuMusic = true;
        public bool QuickMenuMusic { get => _QuickMenuMusic; set { _QuickMenuMusic = value; Save(); } }
      

        public static MainConfig GetInstance()
        {
            if (instance == null)
            {
                new MainConfig().Load();
            }
            return instance;
        }
        
        public MainConfig Load()
        {
            if (!File.Exists(FilePath)) { instance = new MainConfig(); HasLoadedFirst = true; Save(); return instance; }
            
            string filestr = File.ReadAllText(FilePath);
            if (string.IsNullOrEmpty(filestr)) { instance = new MainConfig(); HasLoadedFirst = true; Save(); return instance; }
            if (filestr == "null") { instance = new MainConfig(); HasLoadedFirst = true; Save(); return instance; }

            Dev("Config", "Loaded...");
            instance = JsonConvert.DeserializeObject<MainConfig>(filestr);
            HasLoadedFirst = true; 
            return instance;


          
             
        }

        public void Save()
        {
            if (!HasLoadedFirst) return;
            
          string jsonObject = JsonConvert.SerializeObject(instance, Formatting.Indented);
          File.WriteAllText(FilePath, jsonObject);
        }

    }
}

