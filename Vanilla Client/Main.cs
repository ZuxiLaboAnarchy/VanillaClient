
using Vanilla.Modules;
using Vanilla.Patches;
using System.Runtime.CompilerServices;
using static Vanilla.Utils.Performance;
using UnityEngine;

namespace Vanilla
{
    internal class Main
    {
        [CompilerGenerated]
        internal protected static void CallOnStart()
        {
            FileHelper.Setup();

            PatchManager.Patch();

            ModuleManager.InitModules();

            AssetLoader.LoadAssetBundle();


            
            try { for (int i = 0; i < PatchManager.Patches.Count; i++) PatchManager.Patches[i].Patch(); } catch (Exception e) { ExceptionHandler("Patches", e); }

            Log("Patch Manager", $"Patched {PatchManager.PatchedMethods} Methods", ConsoleColor.Green);

            try { for (int i = 0; i < ModuleManager.Modules.Count; i++) ModuleManager.Modules[i].Start(); } catch (Exception e) { ExceptionHandler("Modules", e); }

            Dev("OnStart", "On App Start Complete");

            Log("Performance", $"Client Init Took: " + GetProfiling("OnStart").ToString() + " ms", ConsoleColor.Green);

        }
        public class Nig
        {
            public static bool flytoggle = false;
        }
        internal protected static void CallOnGUI()
        {
            ModuleManager.OnGUI();
        }

        internal protected static void CallOnUpdate()
        {
            ModuleManager.Update();
        }
        internal protected static void CallOnLateStart()
        {
            ModuleManager.LateStart();
        }
        internal protected static void CallOnGameQuit()
        {
            PatchManager.Stop();
            ModuleManager.Stop();

            LogHandler.Pop();


        }

    }
}
