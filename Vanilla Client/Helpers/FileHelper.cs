using MelonLoader;
using System.Diagnostics;
using System.IO;

namespace Vanilla.Utils
{
    internal class FileHelper
    {
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
            return GetMainFolder() + $"\\Cheats\\{GetGameName()}";
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
            if (!File.Exists(MelonUtils.BaseDirectory + "\\WSManager.dll"))
            { File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\WSManager.dll", Properties.Resources.WSManager); }

            if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\discord-rpc.dll"))
            { File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\UserLibs\\discord-rpc.dll", Properties.Resources.discord_rpc); }
           
            if (!File.Exists(FileHelper.GetCheatFolder() + "\\FriendList.CE"))
            { File.Create(FileHelper.GetCheatFolder() + "\\FriendList.CE").Close(); }
        }

        internal static void CheckDirs()
        {
            if (!File.Exists(MelonUtils.BaseDirectory + "\\WSManager.dll"))
            {
                Log("FileHelper", "Missing Core Files will attempt to add back", ConsoleColor.Yellow);
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\WSManager.dll", Properties.Resources.WSManager);
            }

            if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\discord-rpc.dll"))
            { Log("FileHelper", "Missing Important File DiscordRPC Will be re added on next start....", ConsoleColor.Yellow); }

            if (!File.Exists(FileHelper.GetCheatFolder() + "\\FriendList.CE"))
            { File.Create(FileHelper.GetCheatFolder() + "\\FriendList.CE"); }

            if (!File.Exists(GetDependencyFolder() + "\\VanillaClientHelper.exe"))
            { Process.GetCurrentProcess().Kill(); }



        }



    }
}
