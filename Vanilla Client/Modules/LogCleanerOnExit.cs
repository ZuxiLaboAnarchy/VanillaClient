using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Config;

namespace Vanilla.Modules
{
    internal class LogCleanerOnExit : VanillaModule
    {
        internal override void Stop()
        {
            if (!MainConfig.GetInstance().ClearLogsOnExit) { return;  }
            string arguments = "--silent";
            foreach (string stringi in Environment.GetCommandLineArgs())
            {
                arguments += $"{stringi} ";
            }
            System.Diagnostics.Process vrcProcess = new System.Diagnostics.Process();
            vrcProcess.StartInfo.FileName = FileHelper.GetCheatFolder() + "\\Resources\\AnarchyLogCleaner.exe";
            vrcProcess.StartInfo.Arguments = arguments;
            vrcProcess.Start();

         //   Process.GetCurrentProcess().Kill();
        }
    }
}
