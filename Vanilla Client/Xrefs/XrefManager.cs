﻿// /*
//  *
//  * VanillaClient - XrefManager.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using Harmony;
using System.Linq;
using System.Reflection;
using UnhollowerRuntimeLib.XrefScans;

namespace Vanilla.Xrefs
{
    internal static class XRefManager
    {
        internal static bool CheckUsed(MethodBase methodBase, string methodName)
        {
            try
            {
                return XrefScanner.UsedBy(methodBase).Where(instance =>
                    instance.TryResolve() != null && instance.TryResolve().Name.Contains(methodName)).Any();
            }
            catch
            {
            }

            return false;
        }

        public static bool CheckMethod(MethodInfo method, string match)
        {
            try
            {
                foreach (var instance in XrefScanner.XrefScan(method))
                {
                    if (instance.Type == XrefType.Global && instance.ReadAsObject().ToString().Contains(match))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
            }

            return false;
        }

        internal static bool CheckUsedBy(MethodInfo method, string methodName, Type type = null)
        {
            foreach (var instance in XrefScanner.UsedBy(method))
            {
                if (instance.Type == XrefType.Method)
                {
                    try
                    {
                        if ((type == null || instance.TryResolve().DeclaringType == type) &&
                            instance.TryResolve().Name.Contains(methodName))
                        {
                            return true;
                        }
                    }
                    catch
                    {
                    }
                }
            }

            return false;
        }

        internal static bool CheckUsing(MethodInfo method, string methodName, Type type = null)
        {
            foreach (var instance in XrefScanner.XrefScan(method))
            {
                if (instance.Type == XrefType.Method)
                {
                    try
                    {
                        if ((type == null || instance.TryResolve().DeclaringType == type) &&
                            instance.TryResolve().Name.Contains(methodName))
                        {
                            return true;
                        }
                    }
                    catch
                    {
                    }
                }
            }

            return false;
        }

        [Obsolete("Obsolete")]
        internal static void DumpXRefs(this Type type)
        {
            Dev("Xref", $"{type.Name} XRefs:");
            foreach (var m in AccessTools.GetDeclaredMethods(type))
            {
                m.DumpXRefs(1);
            }
        }

        internal static void DumpXRefs(this MethodInfo method, int depth = 0)
        {
            var indent = new string('\t', depth);
            Dev("Xref", $"{indent}{method.Name} XRefs:");
            foreach (var x in XrefScanner.XrefScan(method))
            {
                if (x.Type == XrefType.Global)
                {
                    Dev("Xref", $"\tString = {x.ReadAsObject()?.ToString()}");
                }
                else
                {
                    var resolvedMethod = x.TryResolve();
                    if (resolvedMethod != null)
                    {
                        Dev("Xref", $"{indent}\tMethod -> {resolvedMethod.DeclaringType?.Name}.{resolvedMethod.Name}");
                    }
                }
            }
        }

        internal static bool XRefScanForMethod(this MethodBase methodBase, string methodName = null,
            string reflectedType = null)
        {
            var found = false;
            foreach (var xref in XrefScanner.XrefScan(methodBase))
            {
                if (xref.Type != XrefType.Method)
                {
                    continue;
                }

                var resolved = xref.TryResolve();
                if (resolved == null)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(methodName))
                {
                    found = !string.IsNullOrEmpty(resolved.Name) &&
                            resolved.Name.IndexOf(methodName, StringComparison.OrdinalIgnoreCase) >= 0;
                }

                if (!string.IsNullOrEmpty(reflectedType))
                {
                    found = !string.IsNullOrEmpty(resolved.ReflectedType?.Name)
                            && resolved.ReflectedType.Name.IndexOf(reflectedType, StringComparison.OrdinalIgnoreCase) >=
                            0;
                }

                if (found)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
