using MelonLoader;
using Vanilla;
using Vanilla.Config;
using Vanilla.Helpers;
using static Vanilla.Main;
using static Vanilla.ServerAPI.Server;
using static Vanilla.Utils.Performance;

namespace Cypher
{


    public class CoreMain : MelonMod
    {
        private static bool _shouldLoad = true;

      
        public override void OnInitializeMelon()
        {
            try
            {
                Log("Core", $"Loading Vanilla Client {RuntimeConfig.ReleaseID}", ConsoleColor.Cyan);
                StartProfiling("OnStart");
             
                if (GeneralUtils.GetGameName() != "VRChat")
                { _shouldLoad = false; return; }

                _shouldLoad = Compatibility.CheckGameVersion();
                if (!_shouldLoad) return;

                #region Compile Time & Setups
                try
                {
                    IntPtr window = UnmanagedUtils.FindWindow(null, "VRChat");
                    string strCompTime = Vanilla.Properties.Resources.BuildTime.Replace("\n", "").Replace("  ", " ");

                    UnmanagedUtils.SetWindowText(window, $"Vanilla Client {RuntimeConfig.ReleaseID} | Build Time: {strCompTime} ");
                    Console.Title = $"Vanilla Client {RuntimeConfig.ReleaseID} | Build Time: " + strCompTime;

                    CypherEngineLog("Core", "Done Setting Up", ConsoleColor.Cyan);
                    Log("Core", "Build Time: " + strCompTime, ConsoleColor.Cyan);
                  
                }
                catch (Exception e) { Console.WriteLine(e); }
                #endregion
                CallOnStart(BotHandle.CheckBotHandle());
            }
            catch (Exception e) { ExceptionHandler("OnAppStart", e); }
        }


        #region Forward Declarations

       

        public override void OnLateInitializeMelon()
        { if (!_shouldLoad) return; CallOnLateStart(); }

        public override void OnGUI()
        { if (!_shouldLoad) return; CallOnGUI(); }



        public override void OnUpdate()
        { if (!_shouldLoad) return; CallOnUpdate(); }

        public override void OnSceneWasLoaded(int level, string _)
        { if (!_shouldLoad) return; CallOnLevelInit(level); }
        
        public override void OnApplicationQuit()
        {
            if (!_shouldLoad) return;
            CallOnGameQuit();
            Log("Core", "Goodbye", ConsoleColor.Yellow);
            Pop();
        }
        
        public override void OnSceneWasUnloaded(int level, string levelname = null)
        {
            if (!_shouldLoad) return;
            CallOnLevelUnload(level);
        }
        
        public override void OnLateUpdate()
        {
            if (!_shouldLoad) return;
            CallOnLateUpdate();
        }
        #endregion
    }
}
