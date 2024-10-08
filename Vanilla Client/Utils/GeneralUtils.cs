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
        { return MelonLoader.InternalUtils.UnityInformationHandler.GameName; }

        internal static string GetCommandLine() { return Environment.CommandLine.ToLower(); }

        internal static void CloseGame() { Process.GetCurrentProcess().Kill(); }

        internal static void Restart()
        {
            string arguments = "";
            foreach (string stringi in Environment.GetCommandLineArgs())
            {
                arguments += $"{stringi} ";
            }
            System.Diagnostics.Process vrcProcess = new System.Diagnostics.Process();
            vrcProcess.StartInfo.FileName = $"{Directory.GetCurrentDirectory()}\\VRChat.exe";
            vrcProcess.StartInfo.Arguments = arguments;
            vrcProcess.Start();
            Process.GetCurrentProcess().Kill();
        }

        internal static string GetClipboard() => Clipboard.GetText();

        internal static void SetClipboard(string Set) => Clipboard.SetText(Set);

        internal static void Delay(int time, Action action) => MelonCoroutines.Start(WaitForDelayFinish(time, action));

        private static IEnumerator WaitForDelayFinish(int time, Action action)
        {
            yield return new WaitForSecondsRealtime(time);
            action.Invoke();
            yield break;
        }

        internal static string GetLaunchParameter(string name)
        {
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            for (int i = 0; i < commandLineArgs.Length; i++)
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
            AssetBundleDownloadManager assetBundleDownloadManager = AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0;
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            PlayerManager playerManager = PlayerManager.prop_PlayerManager_0;
            Player[] P = playerManager.field_Private_List_1_Player_0.ToArray();
            for (int i = 0; i < P.Length; i++)
            {
                if (P[i] != null && P[i].prop_ApiAvatar_0 != null)
                {
                    list.Add(P[i].prop_ApiAvatar_0.assetUrl);
                }
            }
            System.Collections.Generic.Dictionary<string, ObjectPublicInStInCoBoUnInStObBoUnique> dictionary = new System.Collections.Generic.Dictionary<string, ObjectPublicInStInCoBoUnInStObBoUnique>();
            Il2CppSystem.Collections.Generic.Dictionary<string, ObjectPublicInStInCoBoUnInStObBoUnique>.KeyCollection.Enumerator enumerator = assetBundleDownloadManager.field_Private_Dictionary_2_String_ObjectPublicInStInCoBoUnInStObBoUnique_0.Keys.GetEnumerator();      //field_Private_Dictionary_2_String_AssetBundleDownload_0.Keys.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string current = enumerator.Current;
                dictionary.Add(current, assetBundleDownloadManager.field_Private_Dictionary_2_String_ObjectPublicInStInCoBoUnInStObBoUnique_0[current]);
            }
            foreach (string key in dictionary.Keys)
            {
                ObjectPublicInStInCoBoUnInStObBoUnique assetBundleDownload = assetBundleDownloadManager.field_Private_Dictionary_2_String_ObjectPublicInStInCoBoUnInStObBoUnique_0[key];
                if (!assetBundleDownload.field_Private_String_0.Contains("wrld_") && !list.Contains(key))
                {
                    if (assetBundleDownload.prop_GameObject_0 != null)
                    {
                        UnityEngine.Object.DestroyImmediate(assetBundleDownload.prop_GameObject_0, allowDestroyingAssets: true);
                    }
                    assetBundleDownload.field_Private_AssetBundle_0?.Unload(unloadAllLoadedObjects: true);
                    assetBundleDownloadManager.field_Private_Dictionary_2_String_ObjectPublicInStInCoBoUnInStObBoUnique_0.Remove(key);
                }
            }
            dictionary.Clear();
            list.Clear();
            Resources.UnloadUnusedAssets();
            Il2CppSystem.GC.Collect(0, Il2CppSystem.GCCollectionMode.Forced, blocking: true, compacting: true);
            Il2CppSystem.GC.Collect(1, Il2CppSystem.GCCollectionMode.Forced, blocking: true, compacting: true);
            System.GC.Collect(0, System.GCCollectionMode.Forced, blocking: true, compacting: true);
            System.GC.Collect(1, System.GCCollectionMode.Forced, blocking: true, compacting: true);
            System.GC.WaitForPendingFinalizers();
        }
    }

   


}
