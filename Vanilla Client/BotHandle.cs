using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Config;
using Vanilla.Helpers;
using Vanilla.Modules;
using Vanilla.Patches;
using Vanilla.Patches.Harmony;
using Vanilla.ServerAPI;

namespace Vanilla
{
    internal class BotHandle
    {

        internal static bool CheckBotHandle()
        {


            if (GeneralUtils.GetCommandLine().ToLower().Contains("--vanillabot"))
            {
                Log("VanillaBot", "Bot Mode Enabled");
                RuntimeConfig.isBot = true;
                return true;
            }


            return false;
        }

        internal static void InitBotHandle() {
            PatchManager.Patches.Add(new SteamworksPatch());
            PatchManager.Patches.Add(new HWIDPatch());
            PatchManager.Patches.Add(new PhotonPatch());
            ModuleManager.Modules.Add(new WSBase());
            ModuleManager.Modules.Add(new MainHelper());
            ModuleManager.Modules.Add(new PHelper());
            Log("Bot Manager", "Bot Manager Initilized =)", ConsoleColor.Green);
            Dev("VanillaBot", "BotHandler Setup");
        }


    }
}
