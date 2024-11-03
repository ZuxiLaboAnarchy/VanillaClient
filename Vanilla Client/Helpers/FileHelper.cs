// /*
//  *
//  * VanillaClient - FileHelper.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using MelonLoader;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;
using Vanilla.Modules.Manager;

namespace Vanilla.Helpers
{
    internal class FileHelper : VanillaModule
    {
        protected override string ModuleName => "FileManager";

        internal override void Start()
        {
            if (!Directory.Exists(Path.Combine(GetCheatFolder())))
            {
                Directory.CreateDirectory(Path.Combine(GetCheatFolder()));
            }

            if (File.Exists(MelonUtils.BaseDirectory + "\\WSManager.dll"))
            {
                _isCleanup = true;
            }

            if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\discord-rpc.dll"))
            {
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\UserLibs\\discord-rpc.dll",
                    Properties.Resources.discord_rpc);
            }

            if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\NLua.dll"))
            {
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\UserLibs\\NLua.dll", Properties.Resources.NLua);
            }

            if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\KeraLua.dll"))
            {
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\UserLibs\\KeraLua.dll",
                    Properties.Resources.KeraLua);
            }

            if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\lua54.dll"))
            {
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\UserLibs\\lua54.dll",
                    Properties.Resources.lua54);
            }

            if (!Directory.Exists(GetCheatFolder() + "\\Resources"))
            {
                Directory.CreateDirectory(GetCheatFolder() + "\\Resources");
            }

            if (!File.Exists(GetCheatFolder() + "\\Resources\\AnarchyLogCleaner.exe"))
            {
                File.WriteAllBytes(GetCheatFolder() + "\\Resources\\AnarchyLogCleaner.exe",
                    Properties.Resources.AnarchyLogCleaner);
            }

            if (!File.Exists(GetCheatFolder() + "\\FriendList.CE"))
            {
                File.Create(GetCheatFolder() + "\\FriendList.CE").Close();
            }

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

        private static bool _isCleanup = false;

        internal static string GetMainFolder()
        {
            if (!Directory.Exists(MelonUtils.BaseDirectory + $"\\VanillaClient"))
            {
                Directory.CreateDirectory(MelonUtils.BaseDirectory + $"\\VanillaClient");
            }

            return MelonUtils.BaseDirectory + $"\\VanillaClient";


            var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            var AppFolder = Path.Combine(folder, "Vanilla");


            if (!Directory.Exists(AppFolder))
            {
                Directory.CreateDirectory(AppFolder);
            }

            return AppFolder;
        }

        internal static string GetCheatFolder()
        {
            /*
            if (!Directory.Exists(GetMainFolder() + $"\\Cheats\\{GetGameName()}"))
            {
                Directory.CreateDirectory(GetMainFolder() + $"\\Cheats\\{GetGameName()}");
            }
            return GetMainFolder() + $"\\Cheats\\{GetGameName()}\\";*/

            if (!Directory.Exists(MelonUtils.BaseDirectory + $"\\VanillaClient"))
            {
                Directory.CreateDirectory(MelonUtils.BaseDirectory + $"\\VanillaClient");
            }

            return MelonUtils.BaseDirectory + $"\\VanillaClient";
        }


        internal static string GetGameName()
        {
            return MelonLoader.InternalUtils.UnityInformationHandler.GameName;
        }

        internal static string GetDependencyFolder()
        {
            if (!Directory.Exists(GetMainFolder() + "\\Common\\Dependencies"))
            {
                Directory.CreateDirectory(GetMainFolder() + "\\Common\\Dependencies");
            }

            return GetMainFolder() + "\\Common\\Dependencies";
        }

        internal static void CheckDirs()
        {
            if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\discord-rpc.dll"))
            {
                Log("FileHelper", "Missing Important File DiscordRPC Will be re added on next start....",
                    ConsoleColor.Yellow);
            }

            if (!File.Exists(GetCheatFolder() + "\\FriendList.CE"))
            {
                File.Create(GetCheatFolder() + "\\FriendList.CE");
            }

            if (!File.Exists(GetDependencyFolder() + "\\VanillaClientHelper.exe"))
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        internal static string[] EmbededLibraryPaths = new string[4]
        {
            "Vanilla.Resources.WSManager.dll", "Vanilla.Resources.Vanilla.Tomlyn.dll",
            "Vanilla.Resources.Vanilla.JSON.dll", "Vanilla.Resources.Vanilla.Refs.dll"
        };

        internal static void LoadResources()
        {
            for (var i = 0; i < EmbededLibraryPaths.Length; i++)
            {
                var EmbededName = EmbededLibraryPaths[i];
                try
                {
                    var rawAssembly = ResourceUtils.ExtractResource(EmbededName);
                    var assembly = Assembly.Load(rawAssembly);
                    Dev("RLoader", "Injected Embedded Library: " + EmbededName);
                }
                catch (Exception ex)
                {
                    ExceptionHandler("RLoader", ex);
                }
            }
        }

        internal static bool SaveTextureToDisk(Texture texture, string fileDirectory, string fileName,
            bool includeCRC32InFileName = false)
        {
            try
            {
                var texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
                var active = RenderTexture.active;
                var temporary = RenderTexture.GetTemporary(texture.width, texture.height, 32);
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
                    File.WriteAllBytes(fileDirectory + "/" + fileName + " - " + Crc32.CalculateCRC(bytes) + ".png",
                        bytes);
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
