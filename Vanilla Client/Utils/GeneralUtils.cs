using MelonLoader;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace Vanilla.Utils
{
    internal class GeneralUtils
    {
        internal static string GetGameName()
        { return MelonLoader.InternalUtils.UnityInformationHandler.GameName; }

        internal static string GetCommandLine() {  return Environment.CommandLine.ToLower();  }

        internal static void CloseGame() { Process.GetCurrentProcess().Kill(); }

        internal static void Restart()
        {
            string arguments = "";
            foreach (string stringi in Environment.GetCommandLineArgs())
            {
                arguments += $"{stringi} ";
            }
            System.Diagnostics.Process WorldBoss = new System.Diagnostics.Process();
            WorldBoss.StartInfo.FileName = $"{Directory.GetCurrentDirectory()}\\RealVRChat.exe";
            WorldBoss.StartInfo.Arguments = arguments;
            WorldBoss.Start();
            Process.GetCurrentProcess().Kill();
        }

        internal static void Delay(int time, Action action)
            => MelonCoroutines.Start(WaitForDelayFinish(time, action));

        private static IEnumerator WaitForDelayFinish(int time, Action action)
        {
            yield return new WaitForSecondsRealtime(time);
            action.Invoke();
            yield break;
        }
    }


}
