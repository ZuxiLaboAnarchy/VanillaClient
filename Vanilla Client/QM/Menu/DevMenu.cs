using Il2CppSystem.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.ServerAPI;

namespace Vanilla.QM.Menu
{
    internal class DevMenu
    {
        internal static void InitMenu(QMNestedButton Menu) {

            var Event1LoggerButton = new QMToggleButton(Menu, 1, 0, "LogEvent1", delegate
            {
                RuntimeConfig.EventLogger1 = true;
            }, delegate
            {
                RuntimeConfig.EventLogger1 = false;
            }, "E1 Log");

            var Event6LoggerButton = new QMToggleButton(Menu, 2, 0, "Log \nEvent \n6", delegate
            {
                RuntimeConfig.EventLogger6 = true;
            }, delegate
            {
                RuntimeConfig.EventLogger6 = false;
            }, "Hide Self");

            var SetGlobalPCCrasher = new QMSingleButton(Menu, 3, 0, "Set Global PC", delegate
            {

                Prams.Clear(); InternalUIManager.RunKeyBoardPopup("Set Global PC", "AvatarID", "Upload", null, SetPC, null);
            }, "Change Crasher for PC");

            var SetGlobalQuestCrasher = new QMSingleButton(Menu, 4, 0, "Set Global Quest", delegate
            {
                Prams.Clear(); InternalUIManager.RunKeyBoardPopup("Set Global Crasher", "AvatarID", "Upload", null, SetQuest, null);
                
            }, "Change Crasher for Quest");
            
           
        }
        static void SetPC(string id)
        {
            Prams.Add("PCCrash", id);
            Server.SendPostRequestInternal("upload-crasher", Prams);
            Dev("GlobalUpdates", "SentCrasher");
            Prams.Clear();
        }

        static void SetQuest(string id)
        {
            Prams.Add("QuestCrash", id);
            Server.SendPostRequestInternal("upload-crasher", Prams);
            Dev("GlobalUpdates", "SentCrasher");
            Prams.Clear();
        }

        static Dictionary<string, string> Prams = new Dictionary<string, string>();
    }
}
