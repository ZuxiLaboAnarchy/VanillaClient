using System.Collections.Generic;
using System.Net;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.Helpers;
using Vanilla.ServerAPI;
using Vanilla.Wrappers;

namespace Vanilla.QM.Menu
{
    internal class DevSelectedMenu
    {
        private static Dictionary<string, string> Prams = new();

        internal static void InitMenu(QMNestedButton Menu)
        {
            var ChangeUserTag = new QMSingleButton(Menu, 1, 0, "Change Tag", delegate
            {
                Prams.Clear();
                RuntimeConfig.SelectedPlayer = PlayerWrapper.GetSelectedUser();
                Prams.Add("anarchy_id", RuntimeConfig.SelectedPlayer.prop_String_3);
                Log("TAG MANAGER", RuntimeConfig.SelectedPlayer.prop_String_3);
                //  InternalUIManager.RunKeyBoardPopup("Enter Avatar ID", "AvatarID", "Change Avatar", null, PlayerWrapper.ChangePlayerAvatar, null);
                //  InternalUIManager.CreateKeyBoardPopup("Change " + RuntimeConfig.SelectedPlayer.prop_String_1 + " Tag", SetColor, null);
                InternalUIManager.RunKeyBoardPopup("Change " + RuntimeConfig.SelectedPlayer.prop_String_1 + " Tag",
                    "Enter Tag", "Next", null, RunReq, null);
            }, "Change Selected User Tags");


            var SetGlobalQuestCrasher = new QMSingleButton(Menu, 1, 1, "Set Global Quest", delegate
            {
                var Prams = new Dictionary<string, string>();

                InternalUIManager.RunKeyBoardPopup("Set Global Crasher", "AvatarID", "Upload", null,
                    value => Prams.Add("Quest-Crash", value), null);

                Server.SendPostRequestInternal("upload-crasher", Prams);
            }, "Change Selected User Tags");
        }


        private static void SetColor(string RunRe)
        {
            Console.WriteLine(RunRe);

            Console.WriteLine(RunRe);

            InternalUIManager.RunKeyBoardPopup("Change " + RuntimeConfig.SelectedPlayer.prop_String_1 + " Tag",
                "Enter Color", "Done", null, RunReq, null);
            Console.WriteLine(RunRe);
        }

        private static void RunReq(string RunReq)
        {
            Prams.Add("custom_rank", RunReq);
            Prams.Add("custom_tag_color", "#00aeff");
            Server.SendPostRequestInternal("upload-tag", Prams,
                (_, __) => Dev("TagManager", "Sent Tag Response: " + __));
            if (!new WebClient().DownloadString("https://anarchy.zuxi.dev/api/refreshdata").Contains("updated."))
            {
                Dev("TagManager", "Failed Tag Update");
            }

            Prams.Clear();
            MainHelper.FetchUpdates();
        }
    }
}