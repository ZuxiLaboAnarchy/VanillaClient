// /*
//  *
//  * VanillaClient - BotHandle.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using Vanilla.APIs.ServerAPI;
using Vanilla.Config;
using Vanilla.Helpers;
using Vanilla.Modules.Manager;
using Vanilla.Patches.Harmony;
using Vanilla.Patches.Manager;
using Vanilla.Patches.Native;

namespace Vanilla.Misc
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

        internal static void InitBotHandle()
        {
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
