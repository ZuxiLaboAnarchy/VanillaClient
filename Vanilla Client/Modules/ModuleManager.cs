using MelonLoader;
using System.Collections;
using System.Collections.Generic;
using Vanilla.Helpers;
using Vanilla.QM;
using Vanilla.ServerAPI;
using VRC;

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
            Modules.Add(new FlyManager());
            Modules.Add(new CameraModule());

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
            MelonCoroutines.Start(WaitForPlayer());
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

        protected internal static void LevelInit(int level)
        {
            for (int i = 0; i < Modules.Count; i++) Modules[i].WorldLoad(level);

        }

        protected internal static IEnumerator WaitForPlayer()
        {
            while (Player.prop_Player_0 == null) yield return null;
            Dev("ModuleManager", "Player Found");
            for (int i = 0; i < Modules.Count; i++) Modules[i].WaitForPlayer();
        }





    }
}
