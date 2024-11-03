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
            if (!GetInstance().ClearLogsOnExit)
            {
                return;
            }

            var arguments = "--silent";
            foreach (var stringi in Environment.GetCommandLineArgs())
            {
                arguments += $"{stringi} ";
            }

            var vrcProcess = new Process();
            vrcProcess.StartInfo.FileName = FileHelper.GetCheatFolder() + "\\Resources\\AnarchyLogCleaner.exe";
            vrcProcess.StartInfo.Arguments = arguments;
            vrcProcess.Start();

            //   Process.GetCurrentProcess().Kill();
        }
    }
}