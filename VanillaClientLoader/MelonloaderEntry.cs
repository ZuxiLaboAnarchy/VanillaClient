// /*
//  *
//  * VanillaClient - MelonloaderEntry.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using MelonLoader;
using MelonLoader.ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace VanillaClientLoader
{
    public class MelonloaderEntry : MelonLoader.MelonPlugin
    {
        // This ensures we load extremely early. While I could load it as a mod, this approach is much faster since it allows
        // loading as soon as possible, catching any issues early on.
        // For example, I used to manually drop all files, but the vanilla client has its own built-in DLL injector for its references
        // and self-extracts, loading directly into the game. So, there's no need for the DLL injection function anymore.
        // However, this can cause an issue when not using a proxy, resulting in warnings like "MELONS ARE MISSING DEPENDENCIES."
        public MelonloaderEntry()
        {
            if (File.Exists(Path.Combine(MelonLoader.MelonUtils.BaseDirectory, "mods", "VanillaClientCore.dll")))
            {
                return;
            }
            ExtractLoadingScreen();
            Main();
        }

        // This is how I actually load my proxy before hiding the core DLLs. It's short, simple, and effective.
        // With all the obfuscations and the proxy I added, it was difficult to figure out initially, but this method is
        // straightforward and works well.
        // @note: No authentication is involved—it's just fetching from GitHub. and or from my server This skips the step of loading the Melon mod proxy
        // (yes, the loader really was this simple).
        private static void Main()
        {
            WebClient webClient = new WebClient();
            webClient.Headers["User-Agent"] = "ZuxiUA.Anarchy-AAA7FB29";
            if (webClient.DownloadString("https://anarchy.zuxi.dev/dl/resources/VanillaLoaderVersion.txt").Contains("1.2.1"))
            {
                byte[] data = webClient.DownloadData("https://anarchy.zuxi.dev/dl/mods/VanillaClientCore.dll");
                MelonHandler.LoadFromByteArray(data);
            }
            else
            {
                MelonLogger.Error("[VANILLA LOADER]: ERROR LOADER OUT OF DATE. Please Update -> https://zuxi.dev/dl/VanillaLoader");

            }
        }

        // this did the custom loading screen
        private static void ExtractLoadingScreen()
        {
            string ThemesDir = Path.Combine(MelonUtils.UserDataDirectory, "MelonStartScreen", "Themes");
            if (!Environment.CommandLine.ToLower().Contains("-znlr"))
            {

                if (File.Exists(Path.Combine(ThemesDir, "Zuxi", "LoadingScreen.Zuxi")))
                {
                    if (File.ReadAllText(Path.Combine(ThemesDir, "Zuxi", "LoadingScreen.Zuxi")).Contains("2"))
                        return;

                }
                byte[] rawAssembly = ExtractResource("VanillaClientLoader.LoadingScreen.zip");
                ExtractZipFile(ThemesDir, rawAssembly);

                File.WriteAllText(Path.Combine(ThemesDir, "Zuxi", "LoadingScreen.Zuxi"), "2");

                File.WriteAllText(Path.Combine(MelonUtils.UserDataDirectory, "MelonStartScreen", "Config.cfg"),
                    "[General]\r\n# Toggles the Entire Start Screen  ( true | false )\r\nEnabled = true\r\n# Current Theme of the Start Screen\r\nTheme = \"Zuxi\"\r\n");


            }
        }

        internal static byte[] ExtractResource(string filename)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Stream stream = executingAssembly.GetManifestResourceStream(filename);
            if (stream == null)
            {
                return null;
            }
            byte[] array = new byte[stream.Length];
            stream.Read(array, 0, array.Length);
            return array;
        }

        internal static void ExtractZipFile(string destinationDirectory, byte[] compressedData)
        {
            using (MemoryStream memoryStream = new MemoryStream(compressedData))
            {
                using (ZipInputStream zipStream = new ZipInputStream(memoryStream))
                {
                    ZipEntry entry;
                    while ((entry = zipStream.GetNextEntry()) != null)
                    {
                        string entryDestinationPath = Path.Combine(destinationDirectory, entry.Name);

                        if (entry.IsDirectory)
                        {
                            Directory.CreateDirectory(entryDestinationPath);
                        }
                        else
                        {
                            using (FileStream fileStream = File.Create(entryDestinationPath))
                            {
                                byte[] buffer = new byte[4096];
                                int bytesRead;
                                while ((bytesRead = zipStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    fileStream.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
