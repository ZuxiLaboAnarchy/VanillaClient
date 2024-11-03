// /*
//  *
//  * VanillaClient - ProtectionHelper.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using Vanilla.Modules.Manager;

namespace Vanilla.Helpers
{
    internal class PHelper : VanillaModule
    {
        protected override string ModuleName => "PHelper";
        private static Timer ProtectionHelperTimer;

        internal override void Start()
        {
            // new Thread(() => { PThreadStart(); }).Start();
            /// CAntiReverse.AntiDump();
        }

        private static Process p = new();

        internal static void PThreadStart()
        {
#if DEBUG
            return;
#endif
#pragma warning disable CS0162 // Unreachable code detected
            ProtectionHelperTimer = new Timer(1000);

            ProtectionHelperTimer.Elapsed += CheckConnection;
            ProtectionHelperTimer.AutoReset = true;
            ProtectionHelperTimer.Enabled = true;


            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.FileName = FileHelper.GetDependencyFolder() + "\\VanillaClientHelper.exe";
            p.StartInfo.Arguments = "--dbd24d2110581d2fe04028fb0c9a1088615d15f0fb0d87b964e74b2ac9d3add5";
            //  p.Start();

            Dev("ProtectionAPI", "Started Protection Helper");
        }

        private static void CheckConnection(object source, ElapsedEventArgs e)
        {
            if (p.HasExited && p.ExitCode == 14)
            {
                Console.WriteLine(p.ExitCode);
                Task.Delay(15000);
                Process.GetCurrentProcess().Kill();
            }
            else if (p.HasExited)
            {
                FileHelper.CheckDirs();
                p.Start();
                Task.Delay(15000);
            }


            var output = p.StandardOutput.ReadToEnd();

            if (output.Contains("WaitingForID"))
            {
                var CurrentID = Process.GetCurrentProcess().Id.ToString();
                var myStreamWriter = p.StandardInput;
                myStreamWriter.WriteLine(CurrentID);
            }

            if (output.Contains("Unhandled Exception"))
            {
                throw new Exception(output);
            }
        }

        internal override void LateStart()
        {
            return;
            try
            {
                // Vanilla.Protections.CAntiReverse.AntiDump();
            }
            catch (Exception e)
            {
                ExceptionHandler("Erase", e);
            }
        }

        internal override void Stop()
        {
            return;
            try
            {
                p.Kill();
            }
            catch (Exception e)
            {
                ExceptionHandler("Helper", e);
            }
        }
    }


#pragma warning restore CS0162 // Unreachable code detected
}
