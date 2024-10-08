using MelonLoader;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vanilla.Helpers;
using Vanilla.QM;
using Vanilla.ServerAPI;
using Vanilla.Utils;
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
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            IEnumerable<Type> InternalTypes = currentAssembly.GetTypes()
           .Where(type => type.IsSubclassOf(typeof(VanillaModule)));

            foreach (Type type in InternalTypes)
            {
              //  Console.WriteLine(type.FullName);
                Modules.Add((VanillaModule)Activator.CreateInstance(type));
            }

            /*
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
            Modules.Add(new PerfModule());
            */


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

        // TODO: fix this
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
            Testing.Test();



            
            for (int i = 0; i < Modules.Count; i++) try { Modules[i].WaitForAPIUser(); } catch (Exception e) { ExceptionHandler("Modules", e, Modules[i].GetModuleName()); }
        }

        protected internal static IEnumerator WaitForPlayer()
        {
            while (PlayerWrapper.PlayerObject() == null) yield return null;
            Dev("ModuleManager", "Player Found");
            for (int i = 0; i < Modules.Count; i++) try { Modules[i].WaitForPlayer(); } catch (Exception e) { ExceptionHandler("Modules", e, Modules[i].GetModuleName()); }
            yield return null;
        }




    }
}
