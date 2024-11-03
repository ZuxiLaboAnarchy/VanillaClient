using System.Runtime.CompilerServices;
using Vanilla.Modules;
using Vanilla.Patches;
using Vanilla.Patches.Harmony;
using Vanilla.ServerAPI;
using static Vanilla.Utils.Performance;

namespace Vanilla
{
    internal class Entry
    {
        [CompilerGenerated]
        protected internal static void CallOnStart()
        {
            Xrefs.Input.GetMethods();
            Console.WriteLine("OK");

            FileHelper.LoadResources();
            ModuleManager.InitModules();
         
            try { for (int i = 0; i < ModuleManager.Modules.Count; i++) ModuleManager.Modules[i].Start(); } catch (Exception e) { ExceptionHandler("Modules", e); }
            Dev("OnStart", "On App Start Complete");
            Log("Performance", $"Client Start Took: " + GetProfiling("OnStart").ToString() + " ms", ConsoleColor.Green);
        }
        // Forward Defs (these should really be in melonmod instance)
        internal protected static void CallOnGUI() => ModuleManager.OnGUI();
        internal protected static void CallOnGameQuit() => ModuleManager.Stop();
        internal protected static void CallOnUpdate() => ModuleManager.Update();
        internal protected static void CallOnLateStart() => ModuleManager.LateStart();
        internal protected static void CallOnLevelInit(int level) => ModuleManager.LevelInit(level);
        internal protected static void CallOnLevelUnload(int level) => ModuleManager.LevelUnload(level);
        internal protected static void CallOnLateUpdate() => ModuleManager.LateUpdates();
    }
}
