using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace VanillaClient.Modules
{
    internal class KeybindManager : VanillaModule
    {
        private static bool UIActive = true;
        public override void Update()
        {

            if (UnityEngine.Input.GetKeyDown(KeyCode.L))
            {
                LogHandler.RePop();
            }


            if (UnityEngine.Input.GetKeyDown(KeyCode.L))
            {
                // UniversalUI.SetUIActive("VanillaClient", IsGUIActive());
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.RightControl))
            {
                Process.GetCurrentProcess().Kill();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.RightAlt))
            {
                string arguments = "";
                foreach (string stringi in Environment.GetCommandLineArgs())
                {
                    arguments += $"{stringi} ";
                }
                System.Diagnostics.Process WorldBoss = new System.Diagnostics.Process();
                WorldBoss.StartInfo.FileName = $"{Directory.GetCurrentDirectory()}\\Worldboss.exe";
                WorldBoss.StartInfo.Arguments = arguments;
                WorldBoss.Start();
                Process.GetCurrentProcess().Kill();
            }







        }

        static bool IsGUIActive()
        {

            return !UIActive;
        }

    }
}
