// /*
//  *
//  * VanillaClient - Player.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using System.Reflection;
using Vanilla.Patches.Manager;
using VRC.Core;

namespace Vanilla.Patches.Harmony
{
    [Obfuscation(Feature = "-flow")]
    [Obfuscation(Feature = "-strenc")]
    [Obfuscation(Feature = "-virtualization")]
    [Obfuscation(Feature = "-rename")]
    internal class PlayerPatch : VanillaPatches
    {
        protected override string patchName => "PlayerPatch";

        internal override void Patch()
        {
            //   var instance = new HarmonyLib.Harmony("StartDONTGETRIDOFTag");

            InitializeLocalPatchHandler(typeof(PlayerPatch));

            PatchMethod(typeof(APIUser).GetProperty(nameof(Strings.allowAvatarCopying)).GetSetMethod(),
                GetLocalPatch(Strings.ForceClone), null);
        }


        private static void ForceClone(ref bool __0)
        {
            __0 = true;
        }
    }
}
