using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Reflection;
using Vanilla.Helpers;
using Vanilla.Wrappers;

namespace Vanilla.Patches.Harmony
{
    [Obfuscation(Feature = "-flow")]
    [Obfuscation(Feature = "-strenc")]
    [Obfuscation(Feature = "-virtualization")]
    [Obfuscation(Feature = "-rename")]
    internal class PhotonPatch : VanillaPatches
    {
        protected override string patchName => "PhotonPatch";

        internal override void Patch()
        {
            try
            {
                InitializeLocalPatchHandler(typeof(PhotonPatch));

                PatchMethod(typeof(LoadBalancingClient).GetMethod(nameof(LoadBalancingClient.OnEvent)), GetLocalPatch(nameof(OnEvent)), null);


                //PatchMethod(typeof().GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), GetLocalPatch("PhotonRaiseEventPatch"), null);



            }
            catch (Exception e)
            {
                Utils.LogHandler.ExceptionHandler(patchName, e);
            }
        }



        private static bool OnEvent(EventData __0)
        {
            try
            {
                var eventCode = __0.Code;

                return eventCode switch
                {
                    1 => !ProtectionHandler.IsEvent1Bad(__0),
                    42 => MainHelper.AvatarLogHandler(),
                    223 => MainHelper.AvatarLogHandler(),
                    _ => true,
                };
            }
            catch (Exception e) { ExceptionHandler("OnEvent", e); return true; }
        }



    }
}
