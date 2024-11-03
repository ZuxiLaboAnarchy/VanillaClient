// /*
//  *
//  * VanillaClient - LogCleanerOnExit.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System.Diagnostics;
using Vanilla.Helpers;
using Vanilla.Modules.Manager;

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
