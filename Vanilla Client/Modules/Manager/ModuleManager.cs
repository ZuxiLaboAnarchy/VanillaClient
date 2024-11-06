// /*
//  *
//  * VanillaClient - ModuleManager.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using MelonLoader;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Vanilla.Wrappers;
using VRC;

namespace Vanilla.Modules.Manager
{
    internal class ModuleManager
    {
        internal static List<VanillaModule> Modules = new();

        internal static void InitModules()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            var InternalTypes = currentAssembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(VanillaModule)));

            foreach (var type in InternalTypes)
            {
                //  Console.WriteLine(type.FullName);
                var _ = (VanillaModule)Activator.CreateInstance(type);
                Modules.Add(_);
                if (_.GetModuleName() == "Undefined Module")
                {
                    Dev("construct modules on start", $"Type: {type.Name} doesnt have a module name");
                }
                else
                {
                    Dev("construct modules on start", $"loaded module: {_.GetModuleName()}");
                }
            }

            Dev("ScriptManager", $"Current ModuleCount {Modules.Count}");
            Log("ScriptManager", "Script Manager Initialized =)", ConsoleColor.Green);
        }

        protected internal static void LateStart()
        {
            for (var i = 0; i < Modules.Count; i++)
            {
                Modules[i].LateStart();
            }

            MelonCoroutines.Start(WaitForAPIUser());
            MelonCoroutines.Start(WaitForPlayer());
            MelonCoroutines.Start(WaitForUiManager());
            MelonCoroutines.Start(WaitForQMLoad());
        }

        protected internal static void OnGUI()
        {
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].OnGUI();
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }
        }

        internal static void Update()
        {
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].Update();
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }
        }

        protected internal static void LevelInit(int level)
        {
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].WorldLoad(level);
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }
        }

        protected internal static void LateUpdates()
        {
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].LateUpdate();
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }
        }

        protected internal static void LevelUnload(int level)
        {
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].WorldUnload(level);
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }
        }

        protected internal static void PlayerJoin(Player __0)
        {
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].PlayerJoin(__0);
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }
        }

        protected internal static void PlayerLeave(Player __0)
        {
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].PlayerLeave(__0);
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }
        }

        protected internal static void OnApplicationFocus(bool __0)
        {
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].AppFocus(__0);
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }
        }

        protected internal static void DebugKey()
        {
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].Debug();
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }
        }

        protected internal static void Stop()
        {
            if (Modules.Count != 0)
            {
                for (var i = 0; i < Modules.Count; i++)
                {
                    try
                    {
                        Modules[i].Stop();
                    }
                    catch (Exception e)
                    {
                        ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                    }
                }

                Modules.Clear();
                Log("Script Manager", "Script Manager Destroyed =( See you Next Time", ConsoleColor.Yellow);
            }
        }

        protected internal static IEnumerator WaitForAPIUser()
        {
            while (PlayerWrapper.GetLocalAPIUser() == null)
            {
                yield return null;
            }

            Dev("ModuleManager", "User logged in");
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].WaitForAPIUser();
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }
        }

        protected internal static IEnumerator WaitForPlayer()
        {
            while (PlayerWrapper.PlayerObject() == null)
            {
                yield return null;
            }

            Dev("ModuleManager", "Player Found");
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].WaitForPlayer();
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }

            yield return null;
        }

        protected internal static IEnumerator WaitForUiManager()
        {
            while (VRCUiManager.field_Private_Static_VRCUiManager_0 == null)
            {
                yield return null;
            }

            Dev("ModuleManager", "UI manager initialized");
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].OnUiManagerInit();
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }

            yield return null;
        }

        internal static IEnumerator WaitForQMLoad()
        {
            while (GameObject.Find($"UserInterface/Canvas_QuickMenu(Clone)") == null)
            {
                yield return null;
            }

            Dev("ModuleManager", "Quick Menu Loaded");
            for (var i = 0; i < Modules.Count; i++)
            {
                try
                {
                    Modules[i].OnQuickMenuLoaded();
                }
                catch (Exception e)
                {
                    ExceptionHandler("Modules", e, Modules[i].GetModuleName());
                }
            }

            yield return null;
        }
    }
}
