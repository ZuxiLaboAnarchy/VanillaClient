// /*
//  *
//  * VanillaClient - SteamPatch.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using MelonLoader;
using System.Reflection;
using Vanilla.Patches.Manager;

namespace Vanilla.Patches.Native
{
    [Obfuscation(Feature = "-virtualization")]
    internal class SteamworksPatch : VanillaPatches

    {
        private static bool steamPatched;

        protected override string patchName => "SteamworksPatch";

        internal override unsafe void Patch()
        {
            InitializeLocalPatchHandler(typeof(SteamworksPatch));
            //  if (!Configuration.GetGeneralConfig().SpooferSteamID || steamPatched)
            //{
            //  return;
            // }
            var intPtr = IntPtr.Zero;
            try
            {
                intPtr = UnmanagedUtils.LoadLibrary(MelonUtils.GetGameDataDirectory() +
                                                    "\\Plugins\\x86_64\\steam_api64.dll");
            }
            catch (Exception e)
            {
                ExceptionHandler("Steam Spoofer", e);
            }

            if (intPtr == IntPtr.Zero)
            {
                Dev("Steam Spoofer", "SteamAPI library returned no valid address - Steam Spoofer won't work",
                    ConsoleColor.Gray);
                return;
            }

            try
            {
                var procAddress = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_Init");
                var procAddress2 = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_RestartAppIfNecessary");
                var procAddress3 = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_GetHSteamUser");
                var procAddress4 = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_RegisterCallback");
                var procAddress5 = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_UnregisterCallback");
                var procAddress6 = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_RunCallbacks");
                var procAddress7 = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_Shutdown");
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress),
                    GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress2),
                    GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress3),
                    GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress4),
                    GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress5),
                    GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress6),
                    GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress7),
                    GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
            }
            catch (Exception e2)
            {
                ExceptionHandler("Steam Spoofer", e2);
            }

            // Dev("Steam Spoofer", "Patching Steam Succeeded", ConsoleColor.Gray);
            Dev("Hooks", "Hooked Steam API Succesfully");
            PatchManager.PatchedMethods++;
            steamPatched = true;
        }

        private static bool ShouldCallOriginalSteamFunction()
        {
            return false;
        }

        internal static bool IsSteamPatched()
        {
            return steamPatched;
        }
    }
}
