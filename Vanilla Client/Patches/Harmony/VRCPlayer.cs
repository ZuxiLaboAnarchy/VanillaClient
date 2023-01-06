
using UnityEngine;
using Vanilla.Config;
using VRC.Core;

namespace Vanilla.Patches.Harmony
{
    internal class VRCPlayerPatch : VanillaPatches
    {
        protected override string patchName => "VRCPlayerPatch";

        internal override void Patch()
        {
            InitializeLocalPatchHandler(typeof(VRCPlayerPatch));
            if (!BotHandle.CheckBotHandle())
            {
                PatchMethod(typeof(VRCPlayer).GetMethod(nameof(VRCPlayer.Method_Public_Static_String_APIUser_0)), GetLocalPatch(nameof(GetFriendlyDetailedNameForSocialRankPatch)), null);
                //PatchMethod(typeof(VRCPlayer).GetMethod("Method_Public_Static_String_APIUser_1"), GetLocalPatch("GetFriendlyDetailedNameForSocialRankPatch"), null);
                PatchMethod(typeof(VRCPlayer).GetMethod(nameof(VRCPlayer.Method_Public_Static_Color_APIUser_0)), GetLocalPatch(nameof(GetColorForSocialRankPatch)), null);

            }
            else
            {
                PatchMethod(typeof(VRCPlayer).GetMethod(nameof(VRCPlayer.Method_Public_Virtual_Final_New_Void_Single_Single_0)), GetLocalPatch(nameof(UnknownUpdatePatch)), null);
            }
        }

        private static bool UnknownUpdatePatch()
        {
            return false;
        }

        private static bool GetFriendlyDetailedNameForSocialRankPatch(APIUser __0, ref string __result)
        {
            if (__0 == null)
            {
                return true;
            }
            if (RuntimeConfig.RanksCustomRanks && PlayerUtils.playerCustomTags.ContainsKey(__0.id) && PlayerUtils.playerCustomTags[__0.id].customTagEnabled)
            {

                __result = PlayerUtils.playerCustomTags[__0.id].customTag;
                return false;
            }
            return true;
        }

        private static bool GetColorForSocialRankPatch(APIUser __0, ref Color __result)
        {
            if (__0 == null)
            {
                return true;
            }
            if (RuntimeConfig.RanksCustomRanks && PlayerUtils.playerCustomTags.ContainsKey(__0.id) && PlayerUtils.playerCustomTags[__0.id].customTagColorEnabled)
            {
                __result = PlayerUtils.playerCustomTags[__0.id].customTagColor;
                return false;
            }
            return true;
        }
    }
}
