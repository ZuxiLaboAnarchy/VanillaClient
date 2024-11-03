using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using Vanilla.Utils;

namespace Vanilla.Patches.Native
{
    /*
    internal class UnityWebRequestPatch : VanillaPatches
    {
        // Define the delegate for the original UnityWebRequest.Put method
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr UnityWebRequestPutDelegate(string url, string body);

        private static UnityWebRequestPutDelegate originalPut;

        internal override void Patch()
        {


            // Get MethodInfo for UnityWebRequest.Put
            var methodInfo = typeof(UnityWebRequest).GetMethod("Put",
                new[] { typeof(string), typeof(string) });

            if (methodInfo == null)
            {
                LogHandler.Log("UnityWebRequest Patch", $"Failed To Get Method {methodInfo}");
                return;
            }

            // Create the replacement delegate
            UnityWebRequestPutDelegate replacement = (url, body) =>
                HookedPut(url, body);

            // Patch the method using NativePatchUtils
            NativePatchUtils.NativePatch(methodInfo, out originalPut, replacement);
        }

        // The hooked method that will replace the original UnityWebRequest.Put
        private static IntPtr HookedPut(string url, string body)
        {
            // Log the original URL and body
            LogHandler.Log("UnityWebRequest Patch", $"Patching WebRequest URL: {url}, Data: {body}");

            // Modify the URL as required
            url = $"https://anarchy.zuxi.dev/api/respond?code=200&data={body}";

            // Call the original delegate with the modified URL
            return originalPut(url, body);
        }
    }*/
}