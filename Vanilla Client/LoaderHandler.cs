﻿using MelonLoader;
using System.Runtime.InteropServices;
using Vanilla;
using static Vanilla.Main;
using static Vanilla.Utils.Performance;
using static Vanilla.Utils.Server;

namespace Cypher
{
    internal class CoreMain
    {
        private static bool ShouldLoad { get; set; } = true;
        private readonly static string GameVER = "2022.4.2p1-1275--Release";
        internal static string ReleaseID = "Public";
        public static void OnApplicationStart(string LoaderID)
        {

            try
            {
#if DEBUG
                ReleaseID = "Debug";
#elif RELEASE
                ReleaseID = "Release";
#endif




                CypherEngineLog("Core", "Hello From CypherEngine Attempting to Set Up Vanilla Client", ConsoleColor.Cyan);
                CypherEngineLog("Core", $"Loading Vanilla Client {ReleaseID}", ConsoleColor.Cyan);
                StartProfiling("OnStart");

                try
                {
                    //CypherEngine.Protections.CAntiReverse.AntiDump();
                    // ShouldLoad = CypherEngine.Protections.CAntiReverse.Run();
                    if (!ShouldLoad) return;
                }
                catch (Exception e) { ExceptionHandler("Erase", e); }





                ShouldLoad = Vanilla.Protections.LoaderProtections.CheckLoader(LoaderID);
                if (!ShouldLoad) return;

                if (MelonLoader.InternalUtils.UnityInformationHandler.GameName != "VRChat")
                { ShouldLoad = false; return; }

               // if (SendPostRequestInternal("login") == null)
                //{ ShouldLoad = false; return; }

                
#if DEBUG
                if (MelonLoader.InternalUtils.UnityInformationHandler.GameVersion != GameVER)
                {
                    MelonLogger.Error("Game Has Updated. Update Me you Fucking Whore");
                    MelonLogger.Error("CURRENT GAMEVERSION");
                    MelonLogger.Msg(MelonLoader.InternalUtils.UnityInformationHandler.GameVersion);
                }
#else
            if (MelonLoader.InternalUtils.UnityInformationHandler.GameVersion != GameVER)
            {
                MelonLogger.Error("Game Has Updated Please Wait For Cypher To Update");
                MelonLogger.Error("Skipping Load...");
                ShouldLoad = false;
                return;
            }
#endif
                
                #region Compile Time & Setups
                try
                {
                    window = FindWindow(null, "VRChat");
                    string strCompTime = Vanilla.Properties.Resources.BuildTime.Replace("\n", "").Replace("  ", " ");

                    SetWindowText(window, $"Vanilla Client {ReleaseID} | Build Time: {strCompTime} ");
                    Console.Title = $"Vanilla Client {ReleaseID} | Build Time: " + strCompTime;
                    
                    CypherEngineLog("Core", "Done Setting Up", ConsoleColor.Cyan);
                    Log("Core", "Build Time: " + strCompTime, ConsoleColor.Cyan);
                   


                  
                    #endregion
                }
                catch (Exception e) { Console.WriteLine(e); }

                //new Thread(() => { }).Start();
               if (BotHandle.CheckBotHandle())
                {
                    CallOnStart(true);
                    return;
                }

                CallOnStart();



            }
            catch (Exception e) { ExceptionHandler("OnAppStart", e); }



        }


        #region Forward Declarations

        public static void OnApplicationLateStart()
        { if (!ShouldLoad) return; CallOnLateStart(); }

        public static void OnGUI()
        { if (!ShouldLoad) return; CallOnGUI(); }

        public static void OnUpdate()
        { if (!ShouldLoad) return; CallOnUpdate(); }


        public static void OnLevelWasInitialized(int Level, string levelname = null)
        {
            if (!ShouldLoad) return;

            CallOnLevelInit(Level);
        }

        public static void OnApplicationQuit()
        {
            if (!ShouldLoad) return;
            CallOnGameQuit();
            Log("Core", "Goodbye", ConsoleColor.Yellow);
            CypherEngineLog("Core", "Goodbye", ConsoleColor.Yellow);
            Pop();
        }

        public static void OnSceneWasUnloaded(int level, string levelname = null)
        {
            if (!ShouldLoad) return;
            CallOnLevelUnload(level);
        }


        #endregion

        #region Inports

        [DllImport("user32.dll", EntryPoint = "SetWindowText")]
        internal static extern bool SetWindowText(System.IntPtr hwnd, System.String lpString);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        internal static extern System.IntPtr FindWindow(System.String className, System.String windowName);
        private static IntPtr window;

        #endregion

    }



}
