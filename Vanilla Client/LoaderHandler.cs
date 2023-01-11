﻿using System.Reflection;
using Vanilla;
using Vanilla.Config;
using Vanilla.Helpers;
using Vanilla.Protections;
using static Vanilla.Main;
using static Vanilla.ServerAPI.Server;
using static Vanilla.Utils.Performance;

namespace Cypher
{

    //  [Obfuscation(Exclude = true)]
    [Obfuscation(Feature = "-virtualization")]
    [Obfuscation(Feature = "-rename")]
    [Obfuscation(Feature = "-flow")]
    [Obfuscation(Feature = "-strenc")]


    // [Obfuscation(Feature = "-symbol renaming")]
    public class CoreMain
    {
        private static bool ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad { get; set; } = true;

        public static void OnApplicationStart(string LoaderID)
        { 
               
            try
            {
                Vanilla.Xrefs.Input.GetMethods();

                CypherEngineLog("Core", "Hello From Vanilla Engine Attempting to Set Up Vanilla Client", ConsoleColor.Cyan);
                CypherEngineLog("Core", $"Loading Vanilla Client {RuntimeConfig.ReleaseID}", ConsoleColor.Cyan);
                StartProfiling("OnStart");

                try
                {
                    //CypherEngine.Protections.CAntiReverse.AntiDump();
                    // ShouldLoad = CypherEngine.Protections.CAntiReverse.Run();
                    if (!ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad) return;
                }
                catch (Exception e) { ExceptionHandler("Erase", e); }

                ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad = LoaderProtections.CheckLoader(LoaderID);
                if (!ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad) return;

                if (GeneralUtils.GetGameName() != "VRChat")
                { ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad = false; return; }

                ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad = Compatibility.CheckGameVersion();
                if (!ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad) return;


                if (SendPostRequestInternal("login") == null)
                { ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad = false; return; }




                #region Compile Time & Setups
                try
                {
                    window = UnmanagedUtils.FindWindow(null, "VRChat");
                    string strCompTime = Vanilla.Properties.Resources.BuildTime.Replace("\n", "").Replace("  ", " ");

                    UnmanagedUtils.SetWindowText(window, $"Vanilla Client {RuntimeConfig.ReleaseID} | Build Time: {strCompTime} ");
                    Console.Title = $"Vanilla Client {RuntimeConfig.ReleaseID} | Build Time: " + strCompTime;

                    CypherEngineLog("Core", "Done Setting Up", ConsoleColor.Cyan);
                    Log("Core", "Build Time: " + strCompTime, ConsoleColor.Cyan);




                    #endregion
                }
                catch (Exception e) { Console.WriteLine(e); }

                //new Thread(() => { }).Start();


                CallOnStart(BotHandle.CheckBotHandle());



            }
            catch (Exception e) { ExceptionHandler("OnAppStart", e); }



        }


        #region Forward Declarations



        public static void OnApplicationLateStart()
        { if (!ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad) return; CallOnLateStart(); }

        public static void OnGUI()
        { if (!ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad) return; CallOnGUI(); }



        public static void OnUpdate()
        { if (!ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad) return; CallOnUpdate(); }




        public static void OnLevelWasInitialized(int Level, string levelname = null)
        {
            if (!ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad) return;

            CallOnLevelInit(Level);
        }


        public static void OnApplicationQuit()
        {
            if (!ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad) return;
            CallOnGameQuit();
            Log("Core", "Goodbye", ConsoleColor.Yellow);
            CypherEngineLog("Core", "Goodbye", ConsoleColor.Yellow);
            Pop();
        }


        public static void OnSceneWasUnloaded(int level, string levelname = null)
        {
            if (!ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad) return;
            CallOnLevelUnload(level);
        }



        public static void OnLateUpdate()
        {
            if (!ShosahkdfhaskfvbhaskscvjkasduldLASAdafabnsjklbfjlasfasoad) return;
            CallOnLateUpdate();
        }



        #endregion

        #region Extern

        private static IntPtr window;


        #endregion

    }



}
