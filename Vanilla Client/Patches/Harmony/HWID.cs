using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Vanilla.Patches.Harmony
{
    internal class HWIDPatch : VanillaPatches
    {

        protected override string patchName => "HWIDPatch";

        internal override void Patch()
        {
            try
            {
                InitializeLocalPatchHandler(typeof(HWIDPatch));
                PatchMethod(typeof(SystemInfo).GetProperty("deviceUniqueIdentifier").GetGetMethod(), GetLocalPatch("FakeHWID"), null);
               



            }
            catch (Exception e)
            {
                Utils.LogHandler.ExceptionHandler(patchName, e);
            }
        }


        private static bool FakeHWID(ref string __result)
        {
            if (newHWID == string.Empty)
            {
                newHWID = KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}A-{1}{2}-{3}{4}-{5}{6}-3C-1F", new object[]
                {
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9) }))).Select(delegate (byte x)
                    {
                        byte b = x;
                        return b.ToString("x2");
                    }).Aggregate((string x, string y) => x + y);

               Log($"Spoofer", $" Success Patched HWID {newHWID}", ConsoleColor.Green);
            }
            __result = newHWID;
            return false;
        }







        public static string newHWID = "";







      
    }
}
