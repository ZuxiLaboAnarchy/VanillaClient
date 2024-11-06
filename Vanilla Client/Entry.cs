// /*
//  *
//  * VanillaClient - Entry.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using System.Runtime.CompilerServices;
using Vanilla.Helpers;
using Vanilla.Modules.Manager;
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

            try
            {
                for (var i = 0; i < ModuleManager.Modules.Count; i++)
                {
                    ModuleManager.Modules[i].Start();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler("Modules", e);
            }

            Dev("OnStart", "On App Start Complete");
            Log("Performance", $"Client Start Took: " + GetProfiling("OnStart").ToString() + " ms", ConsoleColor.Green);
        }

        // Forward Defs (these should really be in melonmod instance)
        protected internal static void CallOnGUI()
        {
            ModuleManager.OnGUI();
        }

        protected internal static void CallOnGameQuit()
        {
            ModuleManager.Stop();
        }

        protected internal static void CallOnUpdate()
        {
            ModuleManager.Update();
        }

        protected internal static void CallOnLateStart()
        {
            ModuleManager.LateStart();
        }

        protected internal static void CallOnLevelInit(int level)
        {
            ModuleManager.LevelInit(level);
        }

        protected internal static void CallOnLevelUnload(int level)
        {
            ModuleManager.LevelUnload(level);
        }

        protected internal static void CallOnLateUpdate()
        {
            ModuleManager.LateUpdates();
        }
    }
}
