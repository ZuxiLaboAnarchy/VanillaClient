namespace Vanilla.Config
{
    internal class RuntimeConfig
    {
        /*Read Only Vars*/
        internal readonly static string GameVER = "2023.1.1p3-1282--Release";
        
        
        /*Unsaved Global Vars*/
        internal static bool isBot = false;
        internal static bool ShouldFly = false;
        internal static bool ShouldEarRape = false;
        internal static bool EventLogger1 = false;
        internal static bool EventLogger6 = false;
        internal static bool isQuickMenuOpen = false;
        internal static bool RanksCustomRanks = true;
        internal static bool isConnectedToInstance = false;
        internal static bool isForced = false;
        internal static string RealPCrashID = MainConfig.GetInstance().GlobalSyncCrasher ? PCCrashID : MainConfig.GetInstance().PCCrashID;
        internal static string RealQCrashID = MainConfig.GetInstance().GlobalSyncCrasher ? QuestCrashID : MainConfig.GetInstance().QuestCrashID;

        /*User Vars*/
        private static string Username = null;
        private static bool IsStaff = false;
        private static int UUID = 0;
        private static string SubTime = null;
        internal static string Server_JWT = null;

        /*Server Set Var*/
        private static string PCCrashID = null;
        private static string QuestCrashID = null;

        /*Server Controller Vars*/
        internal static bool nextUpdateCheckComplete = true;
        internal static bool WSAuthed = true;
        internal static VRCPlayer SelectedPlayer = null;
#if DEBUG
        internal static string ReleaseID = "Debug";
#else
        internal static string ReleaseID = "Release";
#endif
        internal static bool ItemLagger = false;

        internal static void SetStaff(string id)
        {
           IsStaff = id.Equals("1") ? true : false;
        }

        internal static bool GetIsStaff()
        {
            return IsStaff;
        }


        internal static void SetUserName(string username)
        { if (Username is not null) 
                return;
             Username = username;
            Log("Core", "Welcome " + Username);
        }

        internal static string GetUserName()
        { return Username; }

        internal static void SetUUID(string id)
        { int.TryParse(id, out UUID); }

        internal static int GetUUID() { return UUID;  }

        internal static void SetSubTime(string subtime) { SubTime = subtime; }


        internal static string GetSubtime() { return SubTime; }

        

        internal static void SetCrashingAvatarPC(string id)
        {
            PCCrashID = id;
        }

        internal static string GetCrashingAvatarPC()
        {
            return PCCrashID;
        }

        internal static void SetCrashingAvatarQuest(string id)
        {
            QuestCrashID = id;
        }

        internal static string GetCrashingAvatarQuest()
        {
            return QuestCrashID;
        }
    }
}
