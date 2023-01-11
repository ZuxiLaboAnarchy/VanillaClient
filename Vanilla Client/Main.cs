using System.Runtime.CompilerServices;
using Vanilla.Modules;
using Vanilla.Patches;
using Vanilla.Patches.Harmony;
using Vanilla.ServerAPI;
using static Vanilla.Utils.Performance;

namespace Vanilla
{
    internal class Main
    {
        [CompilerGenerated]
        internal protected static void CallOnStart(bool isBot = false)
        {
            Xrefs.Input.GetMethods();//      SetMethods();
            Console.WriteLine("OK");
            //            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(ResourceUtils.Resolver);

            FileHelper.LoadResources();

            FileHelper.Setup();

            // MelonCoroutines.Start(UserInterface.WaitForUI());
            if (!isBot)
            {
                PatchManager.Patch();
                ModuleManager.InitModules();
                AssetLoader.LoadAssetBundle();
            }
            else
            {
                BotHandle.InitBotHandle();
            }


            Log("Patch Manager", $"Patched {PatchManager.PatchedMethods} Methods", ConsoleColor.Green);

            try { for (int i = 0; i < ModuleManager.Modules.Count; i++) ModuleManager.Modules[i].Start(); } catch (Exception e) { ExceptionHandler("Modules", e); }

            Dev("OnStart", "On App Start Complete");

            Log("Performance", $"Client Start Took: " + GetProfiling("OnStart").ToString() + " ms", ConsoleColor.Green);
           
        }

        internal protected static void CallOnGUI() => ModuleManager.OnGUI();




        internal protected static void CallOnGameQuit()
        {
            PatchManager.Stop();
            ModuleManager.Stop();
            WSBase.Pop();
            LogHandler.Pop();
        }
        internal protected static void CallOnUpdate() => ModuleManager.Update();
        internal protected static void CallOnLateStart() => ModuleManager.LateStart();

        internal protected static void CallOnLevelInit(int level) => ModuleManager.LevelInit(level);

        internal protected static void CallOnLevelUnload(int level) => ModuleManager.LevelUnload(level);


        internal protected static void CallOnLateUpdate() => ModuleManager.LateUpdates();


    }
}
