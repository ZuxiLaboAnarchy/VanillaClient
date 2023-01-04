using MelonLoader;
using System.Collections;
using System.Collections.Generic;
using Vanilla.Helpers;
using Vanilla.QM;
using Vanilla.ServerAPI;
using Vanilla.Wrappers;
using VRC;
using VRC.Core;

namespace Vanilla.Modules
{
    internal class ModuleManager
    {
        internal static List<VanillaModule> Modules = new();
        internal static void InitModules()
        {
           
            Modules.Add(new WSBase());
            Modules.Add(new DiscordManager());
            Modules.Add(new MainHelper());
            Modules.Add(new LoadMusic());
            Modules.Add(new KeybindManager());
            Modules.Add(new ButtonLoader());
            Modules.Add(new FlyManager());
            Modules.Add(new CameraModule());
            Modules.Add(new PlayerController());
            Modules.Add(new ESPModule());
            Modules.Add(new VanillaObject());
            Modules.Add(new PHelper());
            Modules.Add(new VRCPlus());
            Modules.Add(new JoinLoggerModule());
            Modules.Add(new PlayerHandler());

            Dev("ScriptManager", $"Current ModuleCount {Modules.Count}");
            Log("Script Manager", "Script Manager Initilized =)", ConsoleColor.Green);
        }

        protected internal static void LateStart()
        {
            for (int i = 0; i < Modules.Count; i++) Modules[i].LateStart();
            MelonCoroutines.Start(WaitForAPIUser());
            MelonCoroutines.Start(WaitForPlayer());
        }

        protected internal static void OnGUI()
        {
            for (int i = 0; i < Modules.Count; i++) try { Modules[i].OnGUI(); }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
        }

        internal static void Update()
        {
            for (int i = 0; i < Modules.Count; i++) try { Modules[i].Update(); }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
        }

        protected internal static void LevelInit(int level)
        {
            for (int i = 0; i < Modules.Count; i++) try { Modules[i].WorldLoad(level); } catch (Exception e) { ExceptionHandler("Modules", e, Modules[i].GetModuleName()); }
        }

        protected internal static void LateUpdates()
        {
            for (int i = 0; i < Modules.Count; i++) try { Modules[i].LateUpdate(); } catch (Exception e) { ExceptionHandler("Modules", e, Modules[i].GetModuleName()); }
        }


        protected internal static void LevelUnload(int level)
        { for (int i = 0; i < Modules.Count; i++) try { Modules[i].WorldUnload(level); } catch (Exception e) { ExceptionHandler("Modules", e, Modules[i].GetModuleName()); } }

        protected internal static void PlayerJoin(Player __0)
        {
            for (int i = 0; i < Modules.Count; i++) try { Modules[i].PlayerJoin(__0); }
                catch (Exception e){ ExceptionHandler("Modules", e, Modules[i].GetModuleName()); }
        }

        protected internal static void PlayerLeave(Player __0)
        {
            for (int i = 0; i < Modules.Count; i++) try { Modules[i].PlayerLeave(__0); }
                catch (Exception e) { ExceptionHandler("Modules", e, Modules[i].GetModuleName()); }
        }


        protected internal static void OnApplicationFocus(bool __0)
        { for (int i = 0; i < Modules.Count; i++) try { Modules[i].AppFocus(__0); } catch (Exception e) { ExceptionHandler("Modules", e, Modules[i].GetModuleName()); } }

        protected internal static void DebugKey()
        { for (int i = 0; i < Modules.Count; i++) try { Modules[i].Debug(); } catch (Exception e) { ExceptionHandler("Modules", e, Modules[i].GetModuleName()); } }

        protected internal static void Stop()
        {
            for (int i = 0; i < Modules.Count; i++) try { Modules[i].Stop(); } catch (Exception e) { ExceptionHandler("Modules", e, Modules[i].GetModuleName()); }
            Modules.Clear();
            Log("Script Manager", "Script Manager Destroyed =( See you Next Time", System.ConsoleColor.Yellow);
        }
        protected internal static IEnumerator WaitForAPIUser()
        {
            while (PlayerWrapper.GetLocalAPIUser() == null) yield return null;
            Dev("ModuleManager", "User logged in");
            for (int i = 0; i < Modules.Count; i++) try { Modules[i].WaitForAPIUser(); } catch (Exception e) { ExceptionHandler("Modules", e, Modules[i].GetModuleName()); }
        }

        protected internal static IEnumerator WaitForPlayer()
        {
            while (PlayerWrapper.PlayerObject() == null) yield return null;
            Dev("ModuleManager", "Player Found");
            for (int i = 0; i < Modules.Count; i++) try { Modules[i].WaitForPlayer(); } catch (Exception e) { ExceptionHandler("Modules", e, Modules[i].GetModuleName()); }
        }




    }
}
