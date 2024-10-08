#if DEBUG
using Harmony;
using System.Collections.Generic;
using UnityEngine;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.ServerAPI;
using static BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes.EaxBlockCipher;

namespace Vanilla.QM.Menu
{
    internal class DevSelectedMenu
    {
        static Dictionary<string, string> Prams = new Dictionary<string, string>();
        internal static void InitMenu(QMNestedButton Menu)
        {

            var ChangeUserTag = new QMSingleButton(Menu, 1, 0, "Change Tag", delegate
            {
                
                Prams.Clear();
                
                Prams.Add("vrchat_id", RuntimeConfig.SelectedPlayer.prop_String_3);
               
                InternalUIManager.RunKeyBoardPopup("Change " + RuntimeConfig.SelectedPlayer.prop_String_1 + " Tag", "Enter Tag", "Next", null, SetColor, null );

            }, "Change Selected User Tags");


            var SetGlobalQuestCrasher = new QMSingleButton(Menu, 1, 1, "Set Global Quest", delegate
            {
                Dictionary<string, string> Prams = new Dictionary<string, string>();

                InternalUIManager.RunKeyBoardPopup("Set Global Crasher", "AvatarID", "Upload", null, value => Prams.Add("Quest-Crash", value), null);

                Server.SendPostRequestInternal("upload-crasher", Prams);

            }, "Change Selected User Tags");
        }


        static void SetColor(string RunRe)
        {

            Prams.Add("custom_rank", RunRe);
            InternalUIManager.RunKeyBoardPopup("Change " + RuntimeConfig.SelectedPlayer.prop_String_1 + " Tag", "Enter Color", "Next", null, RunReq, null);

        }
        static void RunReq(string RunReq)
        {
            Prams.Add("custom_tag_color", RunReq);
            Server.SendPostRequestInternal("upload-tag", Prams);
            Prams.Clear();
        }

    }
}
#endif