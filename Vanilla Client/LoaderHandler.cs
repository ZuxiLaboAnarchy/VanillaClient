// /*
//  *
//  * VanillaClient - LoaderHandler.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using MelonLoader;
using Vanilla.Config;
using Vanilla.Helpers;
using static Vanilla.Entry;
using static Vanilla.Utils.Performance;

namespace Vanilla
{
    public class CoreMain : MelonMod
    {
        private static bool _shouldLoad = true;

        public override void OnInitializeMelon()
        {
            try
            {
                Log("Core", $"Loading Abandon Ware {RuntimeConfig.ReleaseID}", ConsoleColor.Cyan);
                StartProfiling("OnStart");

                if (GeneralUtils.GetGameName() != "VRChat")
                {
                    _shouldLoad = false;
                    return;
                }

                _shouldLoad = Compatibility.CheckGameVersion();
                if (!_shouldLoad)
                {
                    return;
                }

                #region Compile Time & Setups

                try
                {
                    /// CAN LEAD TO BAN IN REGULAR VRCHAT IF WINDOW TITLE IS TOUCHED DONT RECOMEND UNCOMMENTING THIS
                    // IntPtr window = UnmanagedUtils.FindWindow(null, "VRChat");
                    // UnmanagedUtils.SetWindowText(window, $"Vanilla Client {RuntimeConfig.ReleaseID} | Build Time: {strCompTime} ");
                    var strCompTime = Properties.Resources.BuildTime.Replace("\n", "").Replace("  ", " ");
                    Console.Title = $"Abandon Ware {RuntimeConfig.ReleaseID} | Build Time: " + strCompTime;

                    CypherEngineLog("Core", "Done Setting Up", ConsoleColor.Cyan);
                    Log("Core", "Build Time: " + strCompTime, ConsoleColor.Cyan);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                #endregion

                CallOnStart();
            }
            catch (Exception e)
            {
                ExceptionHandler("OnInitializeMelon", e);
            }
        }

        #region Forward Declarations

        public override void OnLateInitializeMelon()
        {
            if (!_shouldLoad)
            {
                return;
            }

            CallOnLateStart();
        }

        public override void OnGUI()
        {
            if (!_shouldLoad)
            {
                return;
            }

            CallOnGUI();
        }

        public override void OnUpdate()
        {
            if (!_shouldLoad)
            {
                return;
            }

            CallOnUpdate();
        }

        public override void OnSceneWasLoaded(int level, string _)
        {
            if (!_shouldLoad)
            {
                return;
            }

            CallOnLevelInit(level);
        }

        public override void OnApplicationQuit()
        {
            if (!_shouldLoad)
            {
                return;
            }

            CallOnGameQuit();
            Log("Core", "Goodbye", ConsoleColor.Yellow);
            Pop();
        }

        public override void OnSceneWasUnloaded(int level, string levelname = null)
        {
            if (!_shouldLoad)
            {
                return;
            }

            CallOnLevelUnload(level);
        }

        public override void OnLateUpdate()
        {
            if (!_shouldLoad)
            {
                return;
            }

            CallOnLateUpdate();
        }

        #endregion
    }
}
