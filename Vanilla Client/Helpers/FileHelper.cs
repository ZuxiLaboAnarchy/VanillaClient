using MelonLoader;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Vanilla.Utils
{
    internal class FileHelper
    {

        static bool _isCleanup = false;
        internal static string GetMainFolder()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            string AppFolder = Path.Combine(folder, "HyperVanilla Labs");


            if (!Directory.Exists(AppFolder))
            {
                Directory.CreateDirectory(AppFolder);
            }

            return AppFolder;
        }

        internal static string GetCheatFolder()
        {

            if (!Directory.Exists(GetMainFolder() + $"\\Cheats\\{GetGameName()}"))
            {
                Directory.CreateDirectory(GetMainFolder() + $"\\Cheats\\{GetGameName()}");
            }
            return GetMainFolder() + $"\\Cheats\\{GetGameName()}\\";
        }




        internal static string GetGameName()
        { return MelonLoader.InternalUtils.UnityInformationHandler.GameName; }

        internal static string GetDependencyFolder()
        {
            if (!Directory.Exists(GetMainFolder() + "\\Common\\Dependencies"))
            {
                Directory.CreateDirectory(GetMainFolder() + "\\Common\\Dependencies");
            }

            return GetMainFolder() + "\\Common\\Dependencies";
        }

        internal static void Setup()
        {
            if (File.Exists(MelonUtils.BaseDirectory + "\\WSManager.dll"))
            { _isCleanup = true; }

            if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\discord-rpc.dll"))
            { File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\UserLibs\\discord-rpc.dll", Properties.Resources.discord_rpc); }

            if (!File.Exists(FileHelper.GetCheatFolder() + "\\FriendList.CE"))
            { File.Create(FileHelper.GetCheatFolder() + "\\FriendList.CE").Close(); }



            if (_isCleanup)
            {
                Log("Cleaner", "Cleaning up around this will only take a Minute");

                if (File.Exists(MelonUtils.BaseDirectory + "\\WSManager.dll"))
                {
                    File.Delete(MelonUtils.BaseDirectory + "\\WSManager.dll");
                }


                Log("Cleaner", "Done...");
            }
        }

        internal static void CheckDirs()
        {


            if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\discord-rpc.dll"))
            { Log("FileHelper", "Missing Important File DiscordRPC Will be re added on next start....", ConsoleColor.Yellow); }

            if (!File.Exists(FileHelper.GetCheatFolder() + "\\FriendList.CE"))
            { File.Create(FileHelper.GetCheatFolder() + "\\FriendList.CE"); }

            if (!File.Exists(GetDependencyFolder() + "\\VanillaClientHelper.exe"))
            { Process.GetCurrentProcess().Kill(); }



        }

        internal static string[] EmbededLibraryPaths = new string[1] { "Vanilla.Resources.WSManager.dll" };
        internal static void LoadResources()
        {
            for (int i = 0; i < EmbededLibraryPaths.Length; i++)
            {
                string EmbededName = EmbededLibraryPaths[i];
                try
                {
                    byte[] rawAssembly = ResourceUtils.ExtractResource(EmbededName);
                    Assembly assembly = Assembly.Load(rawAssembly);
                    Dev("RLoader", "Injected Embedded Library: " + EmbededName);
                }
                catch (Exception ex)
                {
                    ExceptionHandler("RLoader", ex);
                }
            }
        }

        internal static bool SaveTextureToDisk(Texture texture, string fileDirectory, string fileName, bool includeCRC32InFileName = false)
        {
            try
            {
                Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, mipChain: false);
                RenderTexture active = RenderTexture.active;
                RenderTexture temporary = RenderTexture.GetTemporary(texture.width, texture.height, 32);
                Graphics.Blit(texture, temporary);
                RenderTexture.active = temporary;
                texture2D.ReadPixels(new Rect(0f, 0f, temporary.width, temporary.height), 0, 0);
                texture2D.Apply();
                RenderTexture.active = active;
                RenderTexture.ReleaseTemporary(temporary);
                byte[] bytes = ImageConversion.EncodeToPNG(texture2D);
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }
                if (!includeCRC32InFileName)
                {
                    File.WriteAllBytes(fileDirectory + "/" + fileName + ".png", bytes);
                }
                else
                {
                    File.WriteAllBytes(fileDirectory + "/" + fileName + " - " + Crc32.CalculateCRC(bytes) + ".png", bytes);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }





    }
}
