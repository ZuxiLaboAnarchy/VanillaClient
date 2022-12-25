namespace Vanilla.Config
{
    internal class RuntimeConfig
    {
        internal readonly static string GameVER = "2022.4.2p1-1275--Release";
        internal static bool isBot = false;
        internal static bool ShouldFly = false;
        internal static bool ShouldEarRape = false;
        internal static string NamePlatesLongString = "usr_94d9bc4e-6e16-438e-aa97-7382cb5187e4|VGhlIFJhaWQ/|IzkxNzRkYg==";
        internal static string Server_JWT = null;

        internal static bool WSAuthed = false;
        private static string PCCrashID = null;
        private static string QuestCrashID = null;



        /*User Vars*/
        private static string Username = null;
        private static bool IsStaff = false;
        private static int UUID = 0;
        private static string SubTime = null;


        internal static bool nextUpdateCheckComplete = false;



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
