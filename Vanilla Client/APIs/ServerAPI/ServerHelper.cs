using Microsoft.Win32;
using System.IO;
using static Vanilla.Utils.FileHelper;

namespace Vanilla.Utils
{
    internal class ServerHelper
    {
        private static string JWT = null;

        internal static string GetKey()
        {
            if (!File.Exists(GetMainFolder() + "\\Vanilla.Auth"))
            {
                if (File.Exists(GetMainFolder() + "\\..\\HyperVanilla Labs\\HyperVanilla.Auth"))
                {
                    File.Copy(GetMainFolder() + "\\..\\HyperVanilla Labs\\HyperVanilla.Auth",
                        GetMainFolder() + "\\Vanilla.Auth");
                }
                else
                {
                    throw new Exception("Vanilla Auth File Not Found");
                }
            }

            if (new FileInfo(GetMainFolder() + "\\Vanilla.Auth").Length <= 0)
            {
                throw new Exception("No Key Found");
            }

            return File.ReadAllText(GetMainFolder() + "\\Vanilla.Auth").Trim();
        }

        internal static string GetHWID()
        {
            var HWID = "";

            var name = "SOFTWARE\\Microsoft\\Cryptography";
            var name2 = "MachineGuid";
            using (var registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (var registryKey2 = registryKey.OpenSubKey(name))
                {
                    if (registryKey2 != null)
                    {
                        var value = registryKey2.GetValue(name2);
                        if (value != null)
                        {
                            HWID = value.ToString();
                        }
                    }
                }

                return HWID;
            }
        }

        internal static long GetCurrentTimeInEpoch()
        {
            return Convert.ToInt64(
                (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
        }

        internal static void SetJWT(string NEWJWT)
        {
            JWT = NEWJWT;
        }

        internal static string GetJWT()
        {
            return JWT;
        }
    }
}