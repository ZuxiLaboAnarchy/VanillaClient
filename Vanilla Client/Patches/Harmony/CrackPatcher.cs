// /*
//  *
//  * VanillaClient - CrackPatcher.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Vanilla.Patches.Manager;


namespace Vanilla.Patches.Harmony
{
    internal class CrackPatcher : VanillaPatches
    {
        //TODO: Fix UnityWebRequest since some clients use that instead not many but still important!
        // TODO: Opttimise some of these functions again cba rn

        private static string[] WhitelistedUrlPassthough = new[]
        {
            "anarchy.zuxi.dev",
            "i.imgur.com",
            "raw.githubusercontent.com",
            "?t=" //unix has patched this so we can ignore for the noto crack LMAO
        };

        protected override string patchName => "CrackPatcher";

        internal override void Patch()
        {
            try
            {
                InitializeLocalPatchHandler(typeof(CrackPatcher));
                // common to download data
                PatchMethod(typeof(WebClient).GetMethod(nameof(WebClient.DownloadString), new[] { typeof(string) }),
                    GetLocalPatch(nameof(ReturnEmptyForGet)), null);
                PatchMethod(typeof(WebClient).GetMethod(nameof(WebClient.DownloadData), new[] { typeof(string) }),
                    GetLocalPatch(nameof(ReturnEmptyBytesForGet)), null);
                PatchMethod(
                    typeof(WebClient).GetMethod(nameof(WebClient.UploadData), new[] { typeof(string), typeof(byte[]) }),
                    GetLocalPatch(nameof(ReturnEmptyForUploadData)), null);
                PatchMethod(
                    typeof(WebClient).GetMethod(nameof(WebClient.UploadString),
                        new[] { typeof(string), typeof(string) }), GetLocalPatch(nameof(ReturnEmptyForUploadSData)),
                    null);
                PatchMethod(
                    typeof(WebClient).GetMethod(nameof(WebClient.UploadValues),
                        new[] { typeof(string), typeof(NameValueCollection) }),
                    GetLocalPatch(nameof(ReturnEmptyForUploadValues)), null);

                PatchMethod(
                    typeof(WebClient).GetMethod(nameof(HttpClient.PostAsync),
                        new[] { typeof(string), typeof(NameValueCollection) }),
                    GetLocalPatch(nameof(ReturnEmptyForUploadValues)), null);

                PatchMethod(typeof(HttpClient).GetMethod(nameof(HttpClient.GetAsync), new[] { typeof(string) }),
                    GetLocalPatch(nameof(ReturnEmptyTaskForGetAsync)), null);

                PatchMethod(typeof(HttpClient).GetMethod(nameof(HttpClient.PostAsync), new[] { typeof(string), typeof(HttpContent) }),
                    GetLocalPatch(nameof(ReturnEmptyTaskForPostAsync)), null);
                // PatchMethod(typeof(UnityWebRequest).GetMethod(nameof(UnityWebRequest.Put), new[] { typeof(string), typeof(string) }), GetLocalPatch(nameof(UnityWebRequestPut)), null);
                //  PatchMethod(typeof(UnityWebRequest).GetMethod(nameof(UnityWebRequest.Get), new[] { typeof(string) }), GetLocalPatch("UnityWebRequestGet"), null);
                //  PatchMethod(typeof(UnityWebRequest).GetMethod(nameof(UnityWebRequest.Post), new[] { typeof(string) }), GetLocalPatch("UnityWebRequestPut"), null);
            }
            catch (Exception e)
            {
                ExceptionHandler(patchName, e);
            }
        }


        private static bool ReturnEmptyTaskForGetAsync(ref Task<HttpResponseMessage> __result, string requestUri)
        {
            return ProcessHttpClientRequest(ref __result, requestUri);
        }

        private static bool ReturnEmptyTaskForPostAsync(ref Task<HttpResponseMessage> __result, string requestUri, HttpContent content)
        {
            string data = content != null ? content.ReadAsStringAsync().Result : string.Empty;
            return ProcessHttpClientRequest(ref __result, requestUri, data);
        }

        private static bool ProcessHttpClientRequest(ref Task<HttpResponseMessage> result, string addr, string data = "")
        {
            // Stop Logging Out My URI not trying to hide it but its requested every minute lol
            if (!addr.Contains("dev/api/getdata"))
                Log("CrackManager", $"Patching HttpClient URL: {addr}", ConsoleColor.Yellow);


            if (!string.IsNullOrEmpty(data) && data.Length < 100)
            {
                Log("CrackManager", $"Sending Data: {data}", ConsoleColor.Yellow);
            }

            if (WhitelistedUrlPassthough.Any(x => addr.Contains(x)))
            {
                return true; // Allow whitelisted URLs
            }

            // Return an empty successful response for blocked requests
            var emptyResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(string.Empty)
            };
            result = Task.FromResult(emptyResponse);

            return false;
        }

        private static bool ReturnEmptyForGet(ref string __result, string address)
        {

            return ProcessWebRequest(ref __result, address, "");
        }

        private static bool ReturnEmptyBytesForGet(ref byte[] __result, string address)
        {
            return ProcessWebRequest(ref __result, address, "");
        }

        private static bool ReturnEmptyForUploadData(ref byte[] __result, string address, byte[] data)
        {
            return ProcessWebRequest(ref __result, address, Encoding.UTF8.GetString(data));
        }

        private static bool ReturnEmptyForUploadSData(ref string __result, string address, string data)
        {
            return ProcessWebRequest(ref __result, address, data);
        }

        private static bool ReturnEmptyForUploadValues(ref byte[] __result, string address, NameValueCollection data)
        {
            var dataString = string.Empty;

            foreach (string key in data)
            {
                foreach (var value in data.GetValues(key))
                {
                    dataString += $"{key}: {value}\n";
                }
            }

            return ProcessWebRequest(ref __result, address, dataString);
        }
        // UnityWebrequest cannot be patched directly aparently lol

        private static void UnityWebRequestPut(UnityWebRequest __instance, string uri, string postData)
        {
            // simply pass though active servers // my url so we can still exchange data
            if (WhitelistedUrlPassthough.Any(x => __instance.url.Contains(x)))
            {
                return;
            }

            Log("Crack Manager", $"Patching WebRequest URL: {__instance.url}");
            __instance.url = $"https://anarchy.zuxi.dev/api/respond?code=200&data=";
        }

        private static void UnityWebRequestGet(UnityWebRequest __instance, string uri)
        {
            // simply pass though active servers // my url so we can still exchange data
            if (WhitelistedUrlPassthough.Any(x => __instance.url.Contains(x)))
            {
                return;
            }

            Log("Crack Manager", $"Patching WebRequest URL: {__instance.url}");
            __instance.url = $"https://anarchy.zuxi.dev/api/respond?code=200&data=";
        }

        private static bool ProcessWebRequest(ref byte[] result, string addr, string data)
        {
            // Stop Logging Out My URI not trying to hide it but its requested every minute lol
            if (!addr.Contains("anarchy.zuxi.dev/api/getdata"))
                Log("CrackManager", $"Patching URL: {addr}", ConsoleColor.Yellow);

            if (!string.IsNullOrEmpty(data) &&
                data.Length < 100) // Check if its a mb in size dont want to output that lol
            {
                Log("CrackManager", $"Sending Data: {data}", ConsoleColor.Yellow);
            }

            // simply pass though active servers // my url so we can still exchange data
            if (WhitelistedUrlPassthough.Any(x => addr.Contains(x)))
            {
                return true;
            }

            result = new byte[0];
            return false;
        }

        private static bool ProcessWebRequest(ref string result, string addr, string data)
        {
            byte[] byteArrayResult = null;

            var returnVal = ProcessWebRequest(ref byteArrayResult, addr, data);

            result = byteArrayResult != null ? Encoding.UTF8.GetString(byteArrayResult) : string.Empty;

            return returnVal;
        }
    }
}
