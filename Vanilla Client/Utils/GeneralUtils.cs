using Harmony;
using MelonLoader;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using VRC;


namespace Vanilla.Utils
{
    internal class GeneralUtils
    {
        internal static string GetGameName()
        {
            return MelonLoader.InternalUtils.UnityInformationHandler.GameName;
        }

        internal static string GetCommandLine()
        {
            return Environment.CommandLine.ToLower();
        }

        internal static void CloseGame()
        {
            Process.GetCurrentProcess().Kill();
        }

        internal static void Restart()
        {
            var arguments = "";
            foreach (var stringi in Environment.GetCommandLineArgs())
            {
                arguments += $"{stringi} ";
            }

            var vrcProcess = new Process();
            vrcProcess.StartInfo.FileName = $"{Directory.GetCurrentDirectory()}\\VRChat.exe";
            vrcProcess.StartInfo.Arguments = arguments;
            vrcProcess.Start();
            Process.GetCurrentProcess().Kill();
        }

        internal static string GetClipboard()
        {
            return Clipboard.GetText();
        }

        internal static void SetClipboard(string Set)
        {
            Clipboard.SetText(Set);
        }

        internal static void Delay(int time, Action action)
        {
            MelonCoroutines.Start(WaitForDelayFinish(time, action));
        }

        private static IEnumerator WaitForDelayFinish(int time, Action action)
        {
            yield return new WaitForSecondsRealtime(time);
            action.Invoke();
            yield break;
        }

        internal static string GetLaunchParameter(string name)
        {
            var commandLineArgs = Environment.GetCommandLineArgs();
            for (var i = 0; i < commandLineArgs.Length; i++)
            {
                if (commandLineArgs[i].StartsWith(name))
                {
                    if (commandLineArgs[i].Contains("="))
                    {
                        return commandLineArgs[i].Substring(commandLineArgs[i].LastIndexOf("=") + 1);
                    }

                    return commandLineArgs[i];
                }
            }

            return string.Empty;
        }

        internal static void ClearVRAM()
        {
            var assetBundleDownloadManager = AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0;
            var list = new System.Collections.Generic.List<string>();
            var playerManager = PlayerManager.prop_PlayerManager_0;
            Player[] P = playerManager.field_Private_List_1_Player_0.ToArray();
            for (var i = 0; i < P.Length; i++)
            {
                if (P[i] != null && P[i].prop_ApiAvatar_0 != null)
                {
                    list.Add(P[i].prop_ApiAvatar_0.assetUrl);
                }
            }

            var dictionary = new System.Collections.Generic.Dictionary<string, AssetBundleDownload>();
            Il2CppSystem.Collections.Generic.Dictionary<string, AssetBundleDownload>.KeyCollection.Enumerator
                enumerator = assetBundleDownloadManager.field_Private_Dictionary_2_String_AssetBundleDownload_0.Keys
                    .GetEnumerator(); //field_Private_Dictionary_2_String_AssetBundleDownload_0.Keys.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string current = enumerator.Current;
                dictionary.Add(current,
                    assetBundleDownloadManager.field_Private_Dictionary_2_String_AssetBundleDownload_0[current]);
            }

            foreach (var key in dictionary.Keys)
            {
                var assetBundleDownload =
                    assetBundleDownloadManager.field_Private_Dictionary_2_String_AssetBundleDownload_0[key];
                if (!assetBundleDownload.field_Private_String_0.Contains("wrld_") && !list.Contains(key))
                {
                    if (assetBundleDownload.prop_GameObject_0 != null)
                    {
                        UnityEngine.Object.DestroyImmediate(assetBundleDownload.prop_GameObject_0, true);
                    }

                    assetBundleDownload.field_Private_AssetBundle_0?.Unload(true);
                    assetBundleDownloadManager.field_Private_Dictionary_2_String_AssetBundleDownload_0.Remove(key);
                    assetBundleDownloadManager.field_Private_Dictionary_2_String_AssetBundleDownload_0.Remove(key);
                }
            }

            dictionary.Clear();
            list.Clear();
            Resources.UnloadUnusedAssets();
            Il2CppSystem.GC.Collect(0, Il2CppSystem.GCCollectionMode.Forced, true, true);
            Il2CppSystem.GC.Collect(1, Il2CppSystem.GCCollectionMode.Forced, true, true);
            GC.Collect(0, GCCollectionMode.Forced, true, true);
            GC.Collect(1, GCCollectionMode.Forced, true, true);
            GC.WaitForPendingFinalizers();
        }
    }
}