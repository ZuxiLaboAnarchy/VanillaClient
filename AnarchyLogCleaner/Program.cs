// /*
//  *
//  * VanillaClient - Program.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AnarchyLogCleaner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string processName = "vrchat";
            var vrchatProcess = Process.GetProcessesByName(processName).FirstOrDefault();

            if (vrchatProcess != null)
            {
                Console.WriteLine("VRChat process is running. Waiting for it to exit...");

                // Wait for the process to exit
                vrchatProcess.WaitForExit();

                Console.WriteLine("VRChat process has exited.");
            }
            DeleteLogFiles();

            if (!Environment.CommandLine.ToLower().Contains("--silent"))
                Console.ReadLine();
        }

        private static void DeleteLogFiles()
        {

            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low", "VRChat", "VRChat");
            Console.WriteLine(folderPath);
            foreach (var file in Directory.GetFiles(folderPath).Where(f => f.EndsWith(".txt")))
            {
                if (Path.GetFileName(file).Length == 23)
                {
                    Console.WriteLine("deleting logfile " + file);
                    File.Delete(file);
                }
            }
        }
    }
}
