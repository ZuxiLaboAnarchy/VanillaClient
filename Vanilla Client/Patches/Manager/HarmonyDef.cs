// /*
//  *
//  * VanillaClient - HarmonyDef.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using HarmonyLib;
using System.Reflection;

namespace Vanilla.Patches.Manager
{
    internal abstract class VanillaPatches
    {
        private Type patchType = null;

        protected virtual string patchName => "Undefined Patch";

        internal virtual void Patch()
        {
        }

        internal virtual void Unpatch()
        {
        }

        internal string GetPatchName()
        {
            return patchName;
        }

        protected void InitializeLocalPatchHandler(Type type)
        {
            patchType = type;
        }

        protected void PatchMethod(MethodBase targetMethod, HarmonyMethod preMethod, HarmonyMethod postMethod)
        {
            if (targetMethod == null)
            {
                Log("PatchManager", $"Cannot patch null method Patch Name {patchName}", ConsoleColor.Red);
            }
            else if (preMethod == null && postMethod == null)
            {
                Log("PatchManager",
                    "Cannot patch " + targetMethod.Name + $" since no valid Pre/Post method was found {patchName}",
                    ConsoleColor.Red);
            }
            else
            {
                PatchManager.PatchMethod(targetMethod, preMethod, postMethod);
            }
        }

        protected HarmonyMethod GetLocalPatch(string name)
        {
            return PatchManager.GetLocalPatch(patchType, name);
        }

        protected bool CheckMethod(MethodBase methodBase, string match)
        {
            return PatchManager.CheckMethod(methodBase, match);
        }

        protected bool CheckUsing(MethodInfo method, string match, Type type)
        {
            return PatchManager.CheckUsing(method, match, type);
        }

        protected bool CheckUsed(MethodBase methodBase, string methodName)
        {
            return PatchManager.CheckUsed(methodBase, methodName);
        }

        protected bool CheckNonGlobalMethod(MethodBase methodBase, string match)
        {
            return PatchManager.CheckNonGlobalMethod(methodBase, match);
        }
    }
}
