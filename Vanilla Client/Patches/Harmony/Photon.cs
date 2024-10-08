using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using UnhollowerBaseLib;
using Vanilla.Config;
using Vanilla.Helpers;
using Vanilla.Misc;
using Vanilla.Modules;
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
              
                PatchMethod(typeof(LoadBalancingClient).GetMethod(nameof(LoadBalancingClient.OnEvent)), GetLocalPatch(Strings.OnEvent), null);

         
                //PatchMethod(typeof(LoadBalancingClient).GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), GetLocalPatch("OnEventSent"), null);

              //  LoadBalancingClient

                //PatchMethod(typeof().GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), GetLocalPatch("PhotonRaiseEventPatch"), null);



            }
            catch (Exception e)
            {
                Utils.LogHandler.ExceptionHandler(patchName, e);
            }
        }



        private static bool OnEvent(ref EventData __0)
        {
            return true;
            int A = 0;
            Dev("PhotonPatch", A); A++;
            PhotonEventCodes.EventCodes eventCode = (PhotonEventCodes.EventCodes)__0.Code;
            Dev("Event", eventCode.ToString());
            if (__0 == null) return true;
            Dev("PhotonPatch", A); A++;
            try
            {
                
                Dev("PhotonPatch", A); A++;
                if (eventCode == PhotonEventCodes.EventCodes.VoiceData)
                {
                    Dev("PhotonPatch", A); A++;
                    if (RuntimeConfig.EventLogger1)
                    {
                        byte[] voiceData = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(__0.CustomData.Pointer);

                        Dev("Event1", Convert.ToBase64String(voiceData));
                    }
                }

                if (eventCode == PhotonEventCodes.EventCodes.VRChatRPC)
                {
                    Dev("PhotonPatch", A); A++;
                    if (RuntimeConfig.EventLogger6)
                    {
                        byte[] E6 = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(__0.CustomData.Pointer);

                        Dev("Event 6", Convert.ToBase64String(E6));
                    }
                }
                Dev("PhotonPatch", A); A++;
                return eventCode switch
                {
                    PhotonEventCodes.EventCodes.VoiceData => !ProtectionHandler.IsEvent1Bad(__0),
                    PhotonEventCodes.EventCodes.Moderations => ModerationManager.OnEvent(__0),
                    PhotonEventCodes.EventCodes.SetPlayerData => true,//MainHelper.AvatarLogHandler(),
                    PhotonEventCodes.EventCodes.AuthEvent => true,
                    _ => true,
                };
            }
            catch (Exception e) { ExceptionHandler("OnEvent", e); return true; }
        }

        
           internal static bool OnEventSent(byte eventCode, ref Il2CppSystem.Object eventData, ref RaiseEventOptions raiseEventOptions)
        {
            Log("Code", eventCode);
            Log("eventData", eventData.ToString());
            Log("raiseEventOptions", raiseEventOptions.ToString());

            
            return true;
        }
   


    }
}
