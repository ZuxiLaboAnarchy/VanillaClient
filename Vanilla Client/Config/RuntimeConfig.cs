namespace Vanilla.Config
{
    internal class RuntimeConfig
    {
        /*Read Only Vars*/
        internal readonly static string GameVER = "2022.4.2p1-1275--Release";
        
        
        /*Unsaved Global Vars*/
        internal static bool isBot = false;
        internal static bool ShouldFly = false;
        internal static bool ShouldEarRape = false;
        internal static bool EventLogger1 = false;
        internal static bool isQuickMenuOpen = false;
        internal static bool RanksCustomRanks = true;
        internal static bool isConnectedToInstance = false;

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
        internal static bool WSAuthed = false;
        
#if DEBUG
        internal static string ReleaseID = "Debug";
#else
        internal static string ReleaseID = "Release";
#endif


        internal static void SetStaff(string id)
        {
           IsStaff = id.Equals("1") ? true : false;
        }

        internal static bool GetIsStaff()
        {
            /*TODO make this retun a bool instead*/
            return IsStaff;
        }


        internal static void SetUserName(string id)
        { if (Username != null) 
                return;
             Username = id;
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
