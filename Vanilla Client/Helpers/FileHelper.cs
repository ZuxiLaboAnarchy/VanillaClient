using MelonLoader;
using System.Diagnostics;
using System.IO;

namespace VanillaClient.Utils
{
    internal class FileHelper
    {
        public static string GetMainFolder()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            string AppFolder = Path.Combine(folder, "HyperVoid Labs");


            if (!Directory.Exists(AppFolder))
            {
                Directory.CreateDirectory(AppFolder);
            }

            return AppFolder;
        }

        public static string GetCheatFolder()
        {

            if (!Directory.Exists(GetMainFolder() + $"\\Cheats\\{GetGameName()}"))
            {
                Directory.CreateDirectory(GetMainFolder() + $"\\Cheats\\{GetGameName()}");
            }
            return GetMainFolder() + $"\\Cheats\\{GetGameName()}";
        }




        public static string GetGameName()
        { return MelonLoader.InternalUtils.UnityInformationHandler.GameName; }

        public static string GetDependencyFolder()
        {
            if (!Directory.Exists(GetMainFolder() + "\\Common\\Dependencies"))
            {
                Directory.CreateDirectory(GetMainFolder() + "\\Common\\Dependencies");
            }

            return GetMainFolder() + "\\Common\\Dependencies";
        }

        public static void Setup()
        {
            if (!File.Exists(MelonUtils.BaseDirectory + "\\WSManager.dll"))
            { File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\WSManager.dll", Properties.Resources.WSManager); }

            if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\discord-rpc.dll"))
            { File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\UserLibs\\discord-rpc.dll", Properties.Resources.discord_rpc); }

           /* if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\UniverseLib.IL2CPP.dll"))
            { File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\UserLibs\\UniverseLib.IL2CPP.dll", Properties.Resources.UniverseLib_IL2CPP); }

            if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\UniverseLib.IL2CPP.Unhollower.dll"))
            { File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\UserLibs\\UniverseLib.IL2CPP.Unhollower.dll", Properties.Resources.UniverseLib_IL2CPP_Unhollower); }

            */



        }

        public static void CheckDirs()
        {
            if (!File.Exists(MelonUtils.BaseDirectory + "\\WSManager.dll"))
            { Process.GetCurrentProcess().Kill(); }

            if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\discord-rpc.dll"))
            { Process.GetCurrentProcess().Kill(); }

            /*  if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\UniverseLib.IL2CPP.dll"))
             { Process.GetCurrentProcess().Kill(); }

             if (!File.Exists(MelonUtils.BaseDirectory + "\\UserLibs\\UniverseLib.IL2CPP.Unhollower.dll"))
             { Process.GetCurrentProcess().Kill(); }

             if (!File.Exists(GetDependencyFolder() + "\\VanillaClientHelper.exe"))
              { Process.GetCurrentProcess().Kill(); } */



        }



    }
}
