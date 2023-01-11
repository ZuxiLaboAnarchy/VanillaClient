#if DEBUG
using Il2CppSystem;
using System.Collections.Generic;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.ServerAPI;
using Vanilla.Wrappers;
using static BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes.EaxBlockCipher;

namespace Vanilla.QM.Menu
{
    internal class DevSelectedMenu
    {
        internal static void InitMenu(QMNestedButton Menu)
        {

            var ChangeUserTag = new QMSingleButton(Menu, 1, 1, "Change Tag", delegate
            {
                var tag = "";
                var Color = "";
                InternalUIManager.RunKeyBoardPopup("Change " + RuntimeConfig.SelectedPlayer.prop_String_1 + " Tag", "Enter Tag", "Next", null, value => tag = value, null);
                InternalUIManager.RunKeyBoardPopup("Change " + RuntimeConfig.SelectedPlayer.prop_String_1 + " Tag", "Enter Color", "Next", null, value => Color = value, null);
                Dictionary<string, string> Prams = new Dictionary<string, string>();
                Prams.Add("vrchat_id", RuntimeConfig.SelectedPlayer.prop_String_3);
                Prams.Add("custom_rank", tag);
                Prams.Add("custom_tag_color", Color);
                Server.SendPostRequestInternal("upload-tag", Prams);
            }, "Change Selected User Tags");


            var SetGlobalQuestCrasher = new QMSingleButton(Menu, 1, 1, "Set Global Quest", delegate
            {
                Dictionary<string, string> Prams = new Dictionary<string, string>();
                
                InternalUIManager.RunKeyBoardPopup("Set Global Crasher", "AvatarID", "Upload", null, value => Prams.Add("Quest-Crash", value), null);
                
                Server.SendPostRequestInternal("upload-crasher", Prams);

            }, "Change Selected User Tags");
           
          

        }

    }
}
#endif