﻿// /*
//  *
//  * VanillaClient - PatchManager.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnhollowerRuntimeLib.XrefScans;
using Vanilla.Modules.Manager;

namespace Vanilla.Patches.Manager
{
    internal class PatchManager : VanillaModule
    {
        protected virtual string patchName => "Undefined Patch";
        internal static int PatchedMethods = 0;
        internal static List<VanillaPatches> Patches = new();
        internal static string[] Ignore = { };

        internal override void Start()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            var InternalTypes = currentAssembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(VanillaPatches)));

            foreach (var type in InternalTypes)
            {
                //  Console.WriteLine(type.FullName);
                if (!Ignore.Contains<string>(type.FullName.ToLower()))
                {
                    Patches.Add((VanillaPatches)Activator.CreateInstance(type));
                }
            }

            /*    //BotPatches
                Patches.Add(new SteamworksPatch());
                Patches.Add(new HWIDPatch());
                Patches.Add(new PhotonPatch());
                Patches.Add(new VRCPlayerPatch());
                Patches.Add(new NetworkManagerPatch());
                Patches.Add(new PlayerPatch());
                Patches.Add(new ImageDownloaderPatch());
                // Patches.Add(new Scanner());
                // Patches.Add(new UnityExplorerPatch());*/
#if DEBUG
            // Patches.Add(new CurserPatch());
#endif
            for (var i = 0; i < Patches.Count; i++)
            {
                try
                {
                    Patches[i].Patch();
                }
                catch (Exception e)
                {
                    ExceptionHandler("Patches", e, Patches[i].GetPatchName());
                }
            }

            Dev("PatchManager", "Initialized");
            Log("PatchManager", $"Patched {PatchedMethods} methods.", ConsoleColor.Green);
        }

        internal override void Stop()
        {
            UnpatchAllMethods();

            Dev("PatchManager", $"Unpatched {PatchedMethods} Methods");
            Patches.Clear();
            Dev("PatchManager", $"Patch Manager Destroyed");
        }


        internal static void PatchMethod(MethodBase targetMethod, HarmonyMethod preMethod, HarmonyMethod postMethod)
        {
            EncryptedPatch.Patch(targetMethod, preMethod, postMethod);
            patchedMethods.Add(targetMethod);
            PatchedMethods++;
            Dev("Patches", "Successfully Patched: " + targetMethod.Name);
        }

        internal static void UnpatchAllMethods()
        {
            for (var i = 0; i < patchedMethods.Count; i++)
            {
                EncryptedPatch.Unpatch(patchedMethods[i], HarmonyPatchType.All, EncryptedPatch.Id);
            }

            patchedMethods.Clear();
        }

        internal static HarmonyMethod GetLocalPatch(Type patchType, string name)
        {
            if (patchType == null)
            {
                Log("PatchManager", "Cannot use GetLocalPatch as LocalPatchHandler hasn't been called yet",
                    ConsoleColor.Gray);
                return null;
            }

            var method = patchType.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
            if (method == null)
            {
                Log("PatchManager", "Failed to find valid method named: " + name, ConsoleColor.Gray);
                return null;
            }

            return new HarmonyMethod(method);
        }

        internal static bool CheckMethod(MethodBase methodBase, string match)
        {
            try
            {
                foreach (var item in XrefScanner.XrefScan(methodBase))
                {
                    if (item.Type != 0 || !item.ReadAsObject().ToString().Contains(match))
                    {
                        continue;
                    }

                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        internal static bool CheckUsed(MethodBase methodBase, string methodName, int maxMethodNameLength = 0)
        {
            try
            {
                foreach (var item in XrefScanner.UsedBy(methodBase))
                {
                    var methodBase2 = item.TryResolve();
                    if (methodBase2 == null || !methodBase2.Name.Contains(methodName) || (maxMethodNameLength > 0 &&
                            (maxMethodNameLength <= 0 || methodBase2.Name.Length > maxMethodNameLength)))
                    {
                        continue;
                    }

                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        internal static bool CheckUsing(MethodInfo methodBase, string match, Type type)
        {
            foreach (var item in XrefScanner.XrefScan(methodBase))
            {
                if (item.Type == XrefType.Method)
                {
                    var methodBase2 = item.TryResolve();
                    if (!(methodBase2 == null) && methodBase2.DeclaringType == type && methodBase2.Name.Contains(match))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal static void AnalyzeFunction(MethodInfo methodBase)
        {
            Log("AnalyzeFunction", "-------------------------------------------", ConsoleColor.Gray);
            Log("AnalyzeFunction", "Starting analyze of function: " + methodBase.Name, ConsoleColor.Gray);
            try
            {
                var num = 0;
                foreach (var item in XrefScanner.XrefScan(methodBase))
                {
                    if (item.Type == XrefType.Global)
                    {
                        Log("AnalyzeFunction", "XrefScan Global: " + item.ReadAsObject().ToString(), ConsoleColor.Gray);
                    }
                    else
                    {
                        var methodBase2 = item.TryResolve();
                        if (methodBase2 == null)
                        {
                            continue;
                        }

                        Log("AnalyzeFunction", "XrefScan Method: " + methodBase2.Name, ConsoleColor.Gray);
                    }

                    num++;
                }

                Log("AnalyzeFunction", $"XrefScan Instances: {num}", ConsoleColor.Gray);
                var num2 = 0;
                foreach (var item2 in XrefScanner.UsedBy(methodBase))
                {
                    var methodBase3 = item2.TryResolve();
                    if (!(methodBase3 == null))
                    {
                        Log("AnalyzeFunction", "UsedBy: " + methodBase3.Name, ConsoleColor.Gray);
                        num2++;
                    }
                }

                Log("AnalyzeFunction", $"UsedBy Instances: {num2}", ConsoleColor.Gray);
            }
            catch (Exception e)
            {
                ExceptionHandler("AnalyzeFunction", e, "AnalyzeFunction");
                Log("AnalyzeFunction", "Analyze Complete!", ConsoleColor.Gray);
                Log("AnalyzeFunction", "-------------------------------------------", ConsoleColor.Gray);
            }
        }

        internal static bool CheckNonGlobalMethod(MethodBase methodBase, string methodName, int maxMethodNameLength = 0)
        {
            try
            {
                foreach (var item in XrefScanner.XrefScan(methodBase))
                {
                    if (item.Type == XrefType.Method)
                    {
                        var methodBase2 = item.TryResolve();
                        if (!(methodBase2 == null) && methodBase2.Name.Contains(methodName) &&
                            (maxMethodNameLength <= 0 ||
                             (maxMethodNameLength > 0 && methodBase2.Name.Length <= maxMethodNameLength)))
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
            }

            return false;
        }


        private static readonly HarmonyLib.Harmony EncryptedPatch = new("VanillaClient");

        private static readonly List<MethodBase> patchedMethods = new();
    }
}
