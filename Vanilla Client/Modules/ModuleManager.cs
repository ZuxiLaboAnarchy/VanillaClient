using System.Collections.Generic;
using Vanilla.Helpers;
using Vanilla.QM;
using Vanilla.ServerAPI;

namespace Vanilla.Modules
{
    internal class ModuleManager
    {
        internal static List<VanillaModule> Modules = new();
        public static void InitModules()
        {
            Modules.Add(new ConfigHelper());
            Modules.Add(new WSBase());
            Modules.Add(new WSHelper());
            Modules.Add(new ProtectionHelper());
            Modules.Add(new DiscordManager());
            Modules.Add(new LoadMusic());
            Modules.Add(new KeybindManager());
            Modules.Add(new ButtonLoader());

            Dev("ScriptManager", $"Current ModuleCount {Modules.Count}");
            Log("Script Manager", "Script Manager Initilized =)", ConsoleColor.Green);
        }


        public static void Update()
        {
            for (int i = 0; i < Modules.Count; i++) { Modules[i].Update(); }
        }

        protected internal static void LateStart()
        {
            for (int i = 0; i < Modules.Count; i++) Modules[i].LateStart();
        }

        protected internal static void OnGUI()
        {
            for (int i = 0; i < Modules.Count; i++) Modules[i].OnGUI();
        }

        protected internal static void Stop()
        {
            for (int i = 0; i < Modules.Count; i++) Modules[i].Stop();
            Modules.Clear();
            Log("Script Manager", "Script Manager Destroyed =( See you Next Time", System.ConsoleColor.Yellow);
            Pop();

        }



    }
}
