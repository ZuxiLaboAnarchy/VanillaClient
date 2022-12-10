using System;
using MelonLoader;
using static BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests.SkeinEngine;

namespace Vanilla.Patches
{
    internal class SteamworksPatch : VanillaPatches

    {
        private static bool steamPatched;

        protected override string patchName => "SteamworksPatch";

        internal unsafe override void Patch()
        {
            InitializeLocalPatchHandler(typeof(SteamworksPatch));
          //  if (!Configuration.GetGeneralConfig().SpooferSteamID || steamPatched)
            //{
              //  return;
           // }
            IntPtr intPtr = IntPtr.Zero;
            try
            {
                intPtr = UnmanagedUtils.LoadLibrary(MelonUtils.GetGameDataDirectory() + "\\Plugins\\x86_64\\steam_api64.dll");
            }
            catch (Exception e)
            {
                ExceptionHandler("Steam Spoofer", e);
            }
            if (intPtr == IntPtr.Zero)
            {
               Dev("Steam Spoofer", "SteamAPI library returned no valid address - Steam Spoofer won't work", ConsoleColor.Gray);
                return;
            }
            try
            {
                IntPtr procAddress = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_Init");
                IntPtr procAddress2 = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_RestartAppIfNecessary");
                IntPtr procAddress3 = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_GetHSteamUser");
                IntPtr procAddress4 = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_RegisterCallback");
                IntPtr procAddress5 = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_UnregisterCallback");
                IntPtr procAddress6 = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_RunCallbacks");
                IntPtr procAddress7 = UnmanagedUtils.GetProcAddress(intPtr, "SteamAPI_Shutdown");
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress), GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress2), GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress3), GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress4), GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress5), GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress6), GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
                MelonUtils.NativeHookAttach((IntPtr)(&procAddress7), GetLocalPatch("ShouldCallOriginalSteamFunction").method.MethodHandle.GetFunctionPointer());
            }
            catch (Exception e2)
            {
                ExceptionHandler("Steam Spoofer", e2);
            }
           
            // Dev("Steam Spoofer", "Patching Steam Succeeded", ConsoleColor.Gray);
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