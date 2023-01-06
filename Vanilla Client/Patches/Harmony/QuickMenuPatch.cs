using System.Collections.Generic;
using Vanilla.Config;
using Vanilla.Modules;

namespace Vanilla.Patches.Harmony
{
    internal class QuickMenuPatch : VanillaPatches
    {
        private static string lastOpenedMenu = string.Empty;

        protected override string patchName => "QuickMenuPatch";

        internal override void Patch()
        {
            InitializeLocalPatchHandler(typeof(QuickMenuPatch));
            PatchMethod(typeof(VRC.UI.Elements.QuickMenu).GetMethod(nameof(VRC.UI.Elements.QuickMenu.OnEnable)), null, GetLocalPatch(nameof(OnQuickMenuOpenPatch)));
            PatchMethod(typeof(VRC.UI.Elements.QuickMenu).GetMethod(nameof(VRC.UI.Elements.QuickMenu.OnDisable)), null, GetLocalPatch(nameof(OnQuickMenuClosePatch)));
        }

        private static void OnQuickMenuOpenPatch()
        {
            foreach (KeyValuePair<string, PlayerInformation> playerCaching in PlayerUtils.playerCachingList)
            {
                if (!playerCaching.Value.isLocalPlayer && playerCaching.Value.customNameplateTransform != null)
                {
                    playerCaching.Value.customNameplateTransform.localPosition = MiscUtils.GetNameplateOffset(open: true);
                }
            }
            CameraModule.ChangeCameraClipping(nearClipping: false);
            RuntimeConfig.isQuickMenuOpen = true;
        }

        private static void OnQuickMenuClosePatch()
        {
            foreach (KeyValuePair<string, PlayerInformation> playerCaching in PlayerUtils.playerCachingList)
            {
                if (!playerCaching.Value.isLocalPlayer && playerCaching.Value.customNameplateTransform != null)
                {
                    playerCaching.Value.customNameplateTransform.localPosition = MiscUtils.GetNameplateOffset(open: false);
                }
            }
            //  lastOpenedMenu = QuickMenuUtils.GetQuickMenu().prop_MenuStateController_0.field_Private_UIPage_0.field_Public_String_0;
            //CameraFeaturesHandler.ChangeCameraClipping(Configuration.GetGeneralConfig().MinimumCameraClippingDistance);
            RuntimeConfig.isQuickMenuOpen = false;
        }
    }
}
