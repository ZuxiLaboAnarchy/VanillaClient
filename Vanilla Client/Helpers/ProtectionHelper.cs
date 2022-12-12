using Vanilla.Modules;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Vanilla.Helpers
{
    internal class ProtectionHelper : VanillaModule
    {
        private static System.Timers.Timer ProtectionHelperTimer;

        public override void Start()
        {
            new Thread(() => { PThreadStart(); }).Start();
        }

        static Process p = new Process();
        public static void PThreadStart()
        {
#if !PUBLIC
            return;
#endif
#pragma warning disable CS0162 // Unreachable code detected
            ProtectionHelperTimer = new System.Timers.Timer(1000);
#pragma warning restore CS0162 // Unreachable code detected
            ProtectionHelperTimer.Elapsed += CheckConnection;
            ProtectionHelperTimer.AutoReset = true;
            ProtectionHelperTimer.Enabled = true;


            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.FileName = Utils.FileHelper.GetDependencyFolder() + "\\VanillaClientHelper.exe";
            p.StartInfo.Arguments = "--dbd24d2110581d2fe04028fb0c9a1088615d15f0fb0d87b964e74b2ac9d3add5";
            p.Start();

            Dev("ProtectionAPI", "Started Protection Helper");
        }

        private static void CheckConnection(Object source, ElapsedEventArgs e)
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


            string output = p.StandardOutput.ReadToEnd();

            if (output.Contains("WaitingForID"))
            {
                string CurrentID = Process.GetCurrentProcess().Id.ToString();
                StreamWriter myStreamWriter = p.StandardInput;
                myStreamWriter.WriteLine(CurrentID);
            }

            if (output.Contains("Unhandled Exception"))
            { throw new Exception(output); }





        }

        public override void LateStart()
        {

            try
            {

                // Vanilla.Protections.CAntiReverse.AntiDump();

            }
            catch (Exception e) { ExceptionHandler("Erase", e); }


        }

        public override void Stop()
        {
            p.Kill();
        }


    }



}
