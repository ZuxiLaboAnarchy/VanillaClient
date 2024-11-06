// /*
//  *
//  * VanillaClient - ConsoleLogSuppresser.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using MelonLoader;
using System.Reflection;
using Vanilla.Patches.Manager;

namespace Vanilla.Patches.Harmony
{
    internal class ConsoleLogSuppresser : VanillaPatches
    {
        protected override string patchName => "ConsoleLogSuppresser";

        internal override void Patch()
        {
            InitializeLocalPatchHandler(typeof(ConsoleLogSuppresser));
            PatchMethod(typeof(MelonLogger).GetMethod(
                "NativeError", // Use the exact method name as a string
                BindingFlags.NonPublic |
                BindingFlags.Static // Use BindingFlags.Instance if NativeError is an instance method
            ), GetLocalPatch(nameof(SuppressError)), null);
        }

        private static void SuppressError(ref string namesection, ref string txt)
        {
            if (txt.Contains("Exception"))
            {
                var exceptionMessage = txt.Split('\n')[0].Split(new[] { ": " }, 2, StringSplitOptions.None)[1];
                txt = $"Suppressed Error. => {exceptionMessage}";
            }
        }
    }
}
