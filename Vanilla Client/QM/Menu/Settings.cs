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
        public static void SettingsMenu(QMTabMenu tabMenu)
        {
            var settingsmenu = new QMNestedButton(tabMenu, 1, 3, "Vanilla Settings", "Vanilla", "Vanilla Client");

            var miscsettings = new QMNestedButton(settingsmenu, 3, 3, "Misc Settings", "Miscellaneous Settings", "Galaxy Client");

            var RestartGame = new QMSingleButton(settingsmenu, 1, 3, "Restart Game", delegate
            {
                MainConfig.Save();
                GeneralUtils.Restart();
            }, "Restart Game");

            var closeGame = new QMSingleButton(settingsmenu, 1, 4, "Close Game", delegate
            {
                MainConfig.Save();
                GeneralUtils.CloseGame();
            }, "Close Game");


            var LoadMusicToggle = new QMToggleButton(miscsettings, 2, 0, "LoadMusic", delegate

            {
                MainConfig.LoadMusic = true;
                MelonCoroutines.Start(Modules.LoadMusic.Starter());
                MainConfig.Save();
            }, delegate
            {
                MainConfig.LoadMusic = false;
                MainConfig.Save();
            }, "Toggle Load Music");

            /* Fly
            new NToggle("Flying Apple", Menu.GetMenu(), () =>
            {
                MelonLogger.Msg("On");
                Console.WriteLine("----");
                Nig.flytoggle = true;
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = false;
            }, () =>
            {
                MelonLogger.Msg("Off");
                Console.WriteLine("----");
                Nig.flytoggle = false;
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;

            }, Nig.flytoggle, true, 1, 7);
            */






            if (MainConfig.LoadMusic == true)
            { LoadMusicToggle.ClickMe(); }

        }

     

    }
}
