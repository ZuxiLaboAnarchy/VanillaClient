using System.Reflection;

namespace Vanilla.Patches.Harmony
{
    [Obfuscation(Feature = "-flow")]
    [Obfuscation(Feature = "-strenc")]
    [Obfuscation(Feature = "-virtualization")]
    [Obfuscation(Feature = "-rename")]
    internal class Avatars : VanillaPatches
    {
        protected override string patchName => "AvatarPatch";

        internal override void Patch()
        {
            try
            {
                InitializeLocalPatchHandler(typeof(Avatars));


                //  PatchMethod(typeof(VRC_Player).GetMethod(nameof(VRC_Player.Awake)), null, GetLocalPatch("OnAvatarChanged")); // Post So It Exists.
            }
            catch (Exception e)
            {
                ExceptionHandler(patchName, e);
            }
        }

        /*
        private static void OnAvatarChanged(VRCPlayer __instance)
        {

            if (__instance == null) return;
            //__instance.Method_Public_add_Void_MulticastDelegateNPublicSealedVoUnique_0(new Action(() =>

            __instance.Method_Public_add_Void_OnAvatarIsReady_0(new Action(() =>
            {
                if (__instance._player != null && __instance._player.field_Private_APIUser_0 != null && __instance.field_Private_ApiAvatar_0 != null)
                {

                    try
                    {
                        var a = __instance.field_Private_ApiAvatar_0;

                        var senda = new Logavi()
                        {
                            AvatarName = a.name,

                            Author = a.authorName,

                            Authorid = a.authorId,

                            Avatarid = a.id,

                            Description = a.description,

                            Asseturl = a.assetUrl,

                            Image = a.imageUrl,

                            Platform = a.platform,

                            Status = a.releaseStatus,

                            code = "9",

                        };
                        connect.sendmsg($"{JsonConvert.SerializeObject(senda)}");
                    }
                    catch { }

                }
            }));
        }

        */
    }
}