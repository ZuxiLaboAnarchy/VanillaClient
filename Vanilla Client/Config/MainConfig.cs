﻿using System.IO;
using System.Linq;
using System.Net;
using Vanilla.Modules;
using Vanilla.Tomlyn;
using Vanilla.Tomlyn.Model;
using Vanilla.Tomlyn.Syntax;
using Vanilla.Utils;

namespace Vanilla.Config
{

    internal static class MainConfig
    {
       

        private static string FilePath = Path.Combine(FileHelper.GetCheatFolder(), "Vanilla.cfg");


        private static bool _LoadMusic = true;
        internal static bool LoadMusic { get => _LoadMusic; set { _LoadMusic = value; Save(); } }

        private static string _MusicPath = "";
        internal static string MusicPath { get => _MusicPath; set { _MusicPath = value; Save(); } }

        internal static bool _AutoFriends = false;

        internal static bool AutoFrends { get => _AutoFriends; set { _AutoFriends = value; Save(); } }

        private static int _theme = 0;
        internal static int Theme { get => _theme; set { _theme = value; Save(); } }

        private static bool _TestBool = true;
        internal static bool TestBool { get => _TestBool; set { _TestBool = value; Save(); } }

        private static string _PCCrashID = "";
        internal static string PCCrashID { get => _PCCrashID; set { _PCCrashID = value; Save(); } }

        private static bool _ESP = true;
        internal static bool ESP { get => _ESP; set { _ESP  = value; Save(); } }

        private static bool _JoinLogger = true;
        internal static bool JoinLogger { get => _JoinLogger; set { _JoinLogger = value; Save(); } }
        internal static void Load()
        {
            if (!File.Exists(FilePath))
                return;
            string filestr = File.ReadAllText(FilePath);
            if (string.IsNullOrEmpty(filestr))
                return;
            DocumentSyntax doc = Toml.Parse(filestr);
            if ((doc == null) || doc.HasErrors)
                return;
            TomlTable tbl = doc.ToModel();
            if ((tbl.Count <= 0) || !tbl.ContainsKey("Main"))
                return;
            TomlTable installertbl = (TomlTable)tbl["Main"];
            if ((installertbl == null) || (installertbl.Count <= 0))
                return;
           
            if (installertbl.ContainsKey("TestBool"))
                Boolean.TryParse(installertbl["TestBool"].ToString(), out _TestBool);

            if (installertbl.ContainsKey("LoadMusic"))
                Boolean.TryParse(installertbl["LoadMusic"].ToString(), out _LoadMusic);

            if (installertbl.ContainsKey("MusicPath"))
                _MusicPath = installertbl["MusicPath"].ToString();

            if (installertbl.ContainsKey("AutoSaveFriends"))
                Boolean.TryParse(installertbl["AutoSaveFriends"].ToString(), out _AutoFriends);

            if (installertbl.ContainsKey("PCCrashID"))
                _PCCrashID = installertbl["PCCrashID"].ToString();

            if (installertbl.ContainsKey("ESP"))
                Boolean.TryParse(installertbl["ESP"].ToString(), out _ESP);
            
            if (installertbl.ContainsKey("JoinEvents"))
                Boolean.TryParse(installertbl["JoinEvents"].ToString(), out _ESP);

            Dev("Config", "Loaded...");

        }

        internal static void Save()
        {
            DocumentSyntax doc = new DocumentSyntax();
            TableSyntax tbl = new TableSyntax("Main");
            //tbl.Items.Add(new KeyValueSyntax("Theme", new IntegerValueSyntax(_theme)));
            //tbl.Items.Add(new KeyValueSyntax("TestBool", new BooleanValueSyntax(_TestBool)));
            tbl.Items.Add(new KeyValueSyntax("LoadMusic", new BooleanValueSyntax(_LoadMusic)));
            tbl.Items.Add(new KeyValueSyntax("MusicPath", new StringValueSyntax(_MusicPath)));
            tbl.Items.Add(new KeyValueSyntax("AutoSaveFriends", new BooleanValueSyntax(_AutoFriends)));
            tbl.Items.Add(new KeyValueSyntax("PCCrashID", new StringValueSyntax(_PCCrashID)));
            tbl.Items.Add(new KeyValueSyntax("ESP", new BooleanValueSyntax(_ESP)));
            tbl.Items.Add(new KeyValueSyntax("JoinLogger", new BooleanValueSyntax(_JoinLogger)));

            //tbl.Items.Add(new KeyValueSyntax("LastSelectedGamePath", new StringValueSyntax(string.IsNullOrEmpty(_lastselectedgamepath) ? "" : _lastselectedgamepath)));
            doc.Tables.Add(tbl);
            File.WriteAllText(FilePath, doc.ToString());
        }

    }
}

