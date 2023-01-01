﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Config;
using VRC;

namespace Vanilla.Modules
{
    internal class JoinLoggerModule : VanillaModule
    {
        protected override string ModuleName => "JoinLogger";

        internal override void PlayerJoin(Player __0)
        {
            if (!MainConfig.JoinLogger)
                return;

            if (__0.field_Private_APIUser_0.id == VRC.Core.APIUser.CurrentUser.id)
                return;

            string user = __0.field_Private_APIUser_0.displayName;
            string UID = __0.field_Private_APIUser_0.id;
            bool Quest = __0.field_Private_APIUser_0.IsOnMobile;

            if (user == "orchestrapyro")
            { user = "HyperV"; }

            if (UID == "usr_e49984a4-14de-482d-9899-62d710c7ead8")
            { UID = "IM HYPERV DONT WORRY ABOUT MY UID LOL"; }

            {

                string text = "<color=green>Player Join: <color=purple>" + user;
             //   InformHudText("PlayerJoin", text);
               // ConsoleUtils.Info(LanguageManager.GetUsedLanguage().ProtectionsMenuName, text, ConsoleColor.Red, "OnPlayerJoinPatch", 235);


              //  UserInterface.AddNotificationToHud($"{user} Joined");
                
                
                Log("Player Join", $"{user}"); }
        }

        internal override void PlayerLeave(Player __0)
        {
            if (!MainConfig.JoinLogger)
                return;

            if (__0.field_Private_APIUser_0.id == VRC.Core.APIUser.CurrentUser.id)
                return;

            string user = __0.field_Private_APIUser_0.displayName;
            string UID = __0.field_Private_APIUser_0.id;
            bool Quest = __0.field_Private_APIUser_0.IsOnMobile;

            if (user == "orchestrapyro")
            { user = "HyperV"; }

            if (UID == "usr_e49984a4-14de-482d-9899-62d710c7ead8")
            { UID = "IM HYPERV DONT WORRY ABOUT MY UID LOL"; }

            { Log("Player Leave", $"{user}"); }
        }





    }
}
