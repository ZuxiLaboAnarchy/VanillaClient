using System.Reflection;
using VRC;
using VRC.Core;

namespace Vanilla.Patches.Harmony
{
    [Obfuscation(Feature = "-flow")]
    [Obfuscation(Feature = "-strenc")]
    [Obfuscation(Feature = "-virtualization")]
    [Obfuscation(Feature = "-rename")]
    internal class PlayerPatch : VanillaPatches
    {
        protected override string patchName => "PlayerEventPatch";
        internal override void Patch()
        {
            //   var instance = new HarmonyLib.Harmony("StartDONTGETRIDOFTag");

            InitializeLocalPatchHandler(typeof(PlayerPatch));

            PatchMethod(typeof(APIUser).GetProperty("allowAvatarCopying").GetSetMethod(), GetLocalPatch(nameof(ForceClone)), null);

        }


        private static void ForceClone(ref bool __0)
        {
            __0 = true;
        }







    }
}

