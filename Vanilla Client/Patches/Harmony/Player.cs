using VRC.Core;

namespace Vanilla.Patches.Harmony
{
    internal class PlayerPatch : VanillaPatches
    {
        protected override string patchName => "PlayerEventPatch";
        internal override void Patch()
        {
            //   var instance = new HarmonyLib.Harmony("StartDONTGETRIDOFTag");

            InitializeLocalPatchHandler(typeof(PlayerPatch));

            PatchMethod(typeof(APIUser).GetProperty("allowAvatarCopying").GetSetMethod(), GetLocalPatch("ForceClone"), null);

        }


        private static void ForceClone(ref bool __0)
        {
            __0 = true;
        }







    }
}

