// /*
//  *
//  * VanillaClient - HWID.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using Vanilla.Patches.Manager;

namespace Vanilla.Patches.Harmony
{
    [Obfuscation(Feature = "-flow")]
    [Obfuscation(Feature = "-strenc")]
    [Obfuscation(Feature = "-virtualization")]
    [Obfuscation(Feature = "-rename")]
    internal class HWIDPatch : VanillaPatches
    {
        protected override string patchName => "HWIDPatch";

        internal override void Patch()
        {
            try
            {
                InitializeLocalPatchHandler(typeof(HWIDPatch));
                PatchMethod(typeof(SystemInfo).GetProperty(Strings.deviceUniqueIdentifier).GetGetMethod(),
                    GetLocalPatch(Strings.FakeHWID), null);
            }
            catch (Exception e)
            {
                ExceptionHandler(patchName, e);
            }
        }


        private static bool FakeHWID(ref string __result)
        {
            if (newHWID == string.Empty)
            {
                newHWID = KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format(
                    "{0}A-{1}{2}-{3}{4}-{5}{6}-3C-1F", new object[]
                    {
                        new System.Random().Next(0, 9),
                        new System.Random().Next(0, 9),
                        new System.Random().Next(0, 9),
                        new System.Random().Next(0, 9),
                        new System.Random().Next(0, 9),
                        new System.Random().Next(0, 9),
                        new System.Random().Next(0, 9)
                    }))).Select(delegate (byte x)
                {
                    var b = x;
                    return b.ToString("x2");
                }).Aggregate((string x, string y) => x + y);

                Log($"Spoofer", $"Success Patched HWID {newHWID}", ConsoleColor.Green);
            }

            __result = newHWID;
            return false;
        }

        internal static string newHWID = "";
    }
}
