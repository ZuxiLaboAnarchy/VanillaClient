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
            if (!File.Exists(GetMainFolder() + "\\HyperVanilla.Auth"))
            {
                throw new Exception("HyperVanilla Auth File Not Found");
            }
            if (new FileInfo(GetMainFolder() + "\\HyperVanilla.Auth").Length <= 0)
            {
                throw new Exception("No Key Found");
            }
            return File.ReadAllText(GetMainFolder() + "\\HyperVanilla.Auth").Trim();
        }

        internal static string GetHWID()
        {
            string HWID = "";

            string name = "SOFTWARE\\Microsoft\\Cryptography";
            string name2 = "MachineGuid";
            using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey registryKey2 = registryKey.OpenSubKey(name))
                {
                    if (registryKey2 != null)
                    {
                        object value = registryKey2.GetValue(name2);
                        if (value != null)
                            HWID = value.ToString();

                    }
                }
                return HWID;
            }
        }

        internal static long GetCurrentTimeInEpoch()
        { return Convert.ToInt64((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds); }

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
