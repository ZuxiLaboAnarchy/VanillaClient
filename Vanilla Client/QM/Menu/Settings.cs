using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.Modules;
using static Vanilla.Main;

namespace Vanilla.QM.Menu
{
    internal class Settings
    {
        internal static void SettingsMenu(QMTabMenu tabMenu)
        {
            var settingsmenu = new QMNestedButton(tabMenu, 1, 3, "Vanilla Settings", "Vanilla", "Vanilla Client");

            //  var miscsettings = new QMNestedButton(settingsmenu, 3, 3, "Misc Settings", "Miscellaneous Settings", "Galaxy Client");

            var JoinLogger = new QMToggleButton(settingsmenu, 1, 0, "Join Logger", delegate

            {
                MainConfig.ESP = true;
                MainConfig.Save();
            }, delegate
            {
                MainConfig.ESP = false;
                MainConfig.Save();
            }, "Toggle ESP Saving");

            var LoadMusicToggle = new QMToggleButton(settingsmenu, 2, 0, "LoadMusic", delegate

            {
                MainConfig.LoadMusic = true;
                MelonCoroutines.Start(Modules.LoadMusic.Starter());
                MainConfig.Save();
            }, delegate
            {
                MainConfig.LoadMusic = false;
                MainConfig.Save();
            }, "Toggle Load Music");

            var ESP = new QMToggleButton(settingsmenu, 3, 0, "ESP", delegate

            {
                MainConfig.ESP = true;
                MainConfig.Save();
            }, delegate
            {
                MainConfig.ESP = false;
                MainConfig.Save();
            }, "Toggle ESP Saving");

            var FriendsToggle = new QMToggleButton(settingsmenu, 4, 0, "Auto-Save Friends", delegate

            {
                MainConfig.AutoFrends = true;
                MainConfig.Save();
            }, delegate
            {
                MainConfig.AutoFrends = false;
                MainConfig.Save();
            }, "Toggle AutoFriendsList Saving");

            var RestartGame = new QMSingleButton(settingsmenu, 1, 3, "Restart Game", delegate
            {
                MainConfig.Save();
                GeneralUtils.Restart();
            }, "Restart Game");

            var closeGame = new QMSingleButton(settingsmenu, 2, 3, "Close Game", delegate
            {
                MainConfig.Save();
                GeneralUtils.CloseGame();
            }, "Close Game");

          


            /* How to do the murder 4 hacks Implement soon 
             
                GameObject Murder = submenu.Create("Murder 4", Xploitsubmenu);
                new Submenubutton(Xploitsubmenu.GetMenu(), "Murder 4", Murder, Download_Images._XploitIcon, false, 3, 0);
                new NToggle("Self Gold Weapon", Murder.GetMenu(), () => Defiance.Settings.ConfigVars.murdergoldweapon = true, () => Defiance.Settings.ConfigVars.murdergoldweapon = false, Defiance.Settings.ConfigVars.murdergoldweapon);
                new NToggle("Everyone Gold Weapon", Murder.GetMenu(), () => Defiance.Settings.ConfigVars.everyonegoldgun = true, () => Defiance.Settings.ConfigVars.everyonegoldgun = false, Defiance.Settings.ConfigVars.everyonegoldgun);
                new NToggle("God Mode", Murder.GetMenu(), () => Defiance.Settings.ConfigVars.murdergodmod = true, () => Defiance.Settings.ConfigVars.murdergodmod = false, Defiance.Settings.ConfigVars.murdergodmod);
                new NToggle("Self No ShootC", Murder.GetMenu(), () => Defiance.Settings.ConfigVars.continuesfire = true, () => Defiance.Settings.ConfigVars.continuesfire = false, Defiance.Settings.ConfigVars.continuesfire);
                new NToggle("Everyone No ShootC", Murder.GetMenu(), () => Defiance.Settings.ConfigVars.everyonecontinuesfire = true, () => Defiance.Settings.ConfigVars.everyonecontinuesfire = false, Defiance.Settings.ConfigVars.everyonecontinuesfire);
                new NButton(Murder.GetMenu(), "Start Game", () => Defiance.Exploits.MurderMisc.MurderMod("Btn_Start"));
                new NButton(Murder.GetMenu(), "Throw Apples", () => Defiance.Exploits.MurderMisc.MurderMod("OnLocalPlayerBlinded"));
                new NButton(Murder.GetMenu(), "Abort Game", () => Defiance.Exploits.MurderMisc.MurderMod("SyncAbort"));
                new NButton(Murder.GetMenu(), "Good Apples Win", () => Defiance.Exploits.MurderMisc.MurderMod("SyncVictoryB"));
                new NButton(Murder.GetMenu(), "Bad Apples Win", () => Defiance.Exploits.MurderMisc.MurderMod("SyncVictoryM"));

            */

            /* How to do the Among Us hacks Implement soon 
             
                GameObject Among = submenu.Create("AmongUs", Xploitsubmenu);
                new Submenubutton(Xploitsubmenu.GetMenu(), "AmongUs", Among, Download_Images._XploitIcon, false, 4, 0);
                new NButton(Among.GetMenu(), "Complete Task", () => Defiance.Exploits.AmongUsMisc.AmongUsMod("OnLocalPlayerCompletedTask"));
                new NButton(Among.GetMenu(), "Crew Win", () => Defiance.Exploits.AmongUsMisc.AmongUsMod("Btn_Start"));
                new NButton(Among.GetMenu(), "Sussy Wub", () => Defiance.Exploits.AmongUsMisc.AmongUsMod("SyncVictoryM"));
                new NButton(Among.GetMenu(), "Kill All", () => Defiance.Exploits.AmongUsMisc.AmongUsMod("KillLocalPlayer"));
                new NButton(Among.GetMenu(), "Start Game", () => Defiance.Exploits.AmongUsMisc.AmongUsMod("SyncVictoryB"));
                new NButton(Among.GetMenu(), "Force Meeting", () => Defiance.Exploits.AmongUsMisc.AmongUsMod("StartMeeting"));
            
             */


            /* World History button Somthing like this 
             
            Nocturnal.Ui.qm.Worldhistory.worldhistorymenu = submenu.Create("World History", Menu);
            new Submenubutton(Menu.GetMenu(), "World History", Nocturnal.Ui.qm.Worldhistory.worldhistorymenu, Download_Images._WorldIcon, true, 2, 6);
            
             */



            if (MainConfig.LoadMusic == true)
            { LoadMusicToggle.ClickMe(); }
            if (MainConfig.ESP == true)
            { ESP.ClickMe(); }
            if (MainConfig.JoinLogger == true)
            { JoinLogger.ClickMe(); }
        }

     

    }
}
