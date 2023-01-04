using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnhollowerRuntimeLib.XrefScans;
using Vanilla.Patches.Harmony;

namespace Vanilla.Patches
{
    internal class PatchManager
    {
        protected virtual string patchName => "Undefined Patch";
        internal static int PatchedMethods = 0;
        internal static List<VanillaPatches> Patches = new();
        internal static void Patch()
        {
            //BotPatches
            Patches.Add(new SteamworksPatch());
            Patches.Add(new HWIDPatch());
            Patches.Add(new PhotonPatch());
            Patches.Add(new VRCPlayerPatch());
            Patches.Add(new NetworkManagerPatch());
            Patches.Add(new PlayerPatch());
            // Patches.Add(new Scanner());
            // Patches.Add(new UnityExplorerPatch());
#if DEBUG
            // Patches.Add(new CurserPatch());
#endif
            for (int i = 0; i < PatchManager.Patches.Count; i++) { try { PatchManager.Patches[i].Patch(); } catch (Exception e) { ExceptionHandler("Patches", e, Patches[i].GetPatchName()); } }

            Dev("PatchManager", "Initilized");
        }

        internal static void Stop()
        {
            UnpatchAllMethods();

            Dev("PatchManager", $"Unpatched {PatchManager.PatchedMethods} Methods");
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
            for (int i = 0; i < patchedMethods.Count; i++)
            {
                EncryptedPatch.Unpatch(patchedMethods[i], HarmonyPatchType.All, EncryptedPatch.Id);
            }
            patchedMethods.Clear();
        }

        internal static HarmonyMethod GetLocalPatch(Type patchType, string name)
        {
            if (patchType == null)
            {
                Log("PatchManager", "Cannot use GetLocalPatch as LocalPatchHandler hasn't been called yet", ConsoleColor.Gray);
                return null;
            }
            MethodInfo method = patchType.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
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
                foreach (XrefInstance item in XrefScanner.XrefScan(methodBase))
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
                foreach (XrefInstance item in XrefScanner.UsedBy(methodBase))
                {
                    MethodBase methodBase2 = item.TryResolve();
                    if (methodBase2 == null || !methodBase2.Name.Contains(methodName) || (maxMethodNameLength > 0 && (maxMethodNameLength <= 0 || methodBase2.Name.Length > maxMethodNameLength)))
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
            foreach (XrefInstance item in XrefScanner.XrefScan(methodBase))
            {
                if (item.Type == XrefType.Method)
                {
                    MethodBase methodBase2 = item.TryResolve();
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
                int num = 0;
                foreach (XrefInstance item in XrefScanner.XrefScan(methodBase))
                {
                    if (item.Type == XrefType.Global)
                    {
                        Log("AnalyzeFunction", "XrefScan Global: " + item.ReadAsObject().ToString(), ConsoleColor.Gray);
                    }
                    else
                    {
                        MethodBase methodBase2 = item.TryResolve();
                        if (methodBase2 == null)
                        {
                            continue;
                        }
                        Log("AnalyzeFunction", "XrefScan Method: " + methodBase2.Name, ConsoleColor.Gray);
                    }
                    num++;
                }
                Log("AnalyzeFunction", $"XrefScan Instances: {num}", ConsoleColor.Gray);
                int num2 = 0;
                foreach (XrefInstance item2 in XrefScanner.UsedBy(methodBase))
                {
                    MethodBase methodBase3 = item2.TryResolve();
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
                foreach (XrefInstance item in XrefScanner.XrefScan(methodBase))
                {
                    if (item.Type == XrefType.Method)
                    {
                        MethodBase methodBase2 = item.TryResolve();
                        if (!(methodBase2 == null) && methodBase2.Name.Contains(methodName) && (maxMethodNameLength <= 0 || (maxMethodNameLength > 0 && methodBase2.Name.Length <= maxMethodNameLength)))
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




        private static readonly HarmonyLib.Harmony EncryptedPatch = new HarmonyLib.Harmony("VanillaClient");

        private static readonly List<MethodBase> patchedMethods = new List<MethodBase>();


    }
}
