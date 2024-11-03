// /*
//  *
//  * VanillaClient - Photon.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Reflection;
using UnhollowerBaseLib;
using Vanilla.Config;
using Vanilla.Modules.Photon;
using Vanilla.Patches.Manager;
using Vanilla.Wrappers;
using VRC.Core;

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

                PatchMethod(typeof(VRCNetworkingClient).GetMethod(nameof(VRCNetworkingClient.OnEvent)),
                    GetLocalPatch(nameof(OnEvent)), null);

                //PatchMethod(typeof(LoadBalancingClient).GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), GetLocalPatch("OnEventSent"), null);

                //  LoadBalancingClient

                //  PatchMethod(typeof().GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), GetLocalPatch("PhotonRaiseEventPatch"), null);
            }
            catch (Exception e)
            {
                ExceptionHandler(patchName, e);
            }
        }

        private static bool OnEventWrapper(ref EventData __0)
        {
            var state = OnEvent(ref __0);
            if (state)
            {
                var eventCode = (PhotonEventCodes.EventCodes)__0.Code;
                if (eventCode == PhotonEventCodes.EventCodes.PhotonHeartbeat)
                {
                    return true;
                }

                Log("PhotonDebug", eventCode.ToString() + " Returned " + state.ToString());
            }

            return state;
        }

        private static bool OnEvent(ref EventData __0)
        {
            var eventCode = (PhotonEventCodes.EventCodes)__0.Code;
            // Dev("Event", eventCode.ToString());
            if (__0 == null)
            {
                return true;
            }

            try
            {
                if (eventCode == PhotonEventCodes.EventCodes.VoiceData)
                {
                    if (RuntimeConfig.EventLogger1)
                    {
                        byte[] voiceData = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(__0.CustomData.Pointer);

                        Dev("Event1", Convert.ToBase64String(voiceData));
                    }
                }

                if (eventCode == PhotonEventCodes.EventCodes.VRChatRPC)
                {
                    if (RuntimeConfig.EventLogger6)
                    {
                        byte[] E6 = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(__0.CustomData.Pointer);

                        Dev("Event 6", Convert.ToBase64String(E6));
                    }
                }

                return eventCode switch
                {
                    PhotonEventCodes.EventCodes.VoiceData => !ProtectionHandler.IsEvent1Bad(__0),
                    PhotonEventCodes.EventCodes.Moderations => true, // ModerationManager.OnEvent(__0),
                    PhotonEventCodes.EventCodes.SetPlayerData => true, //MainHelper.AvatarLogHandler(),
                    PhotonEventCodes.EventCodes.AuthEvent => true,
                    PhotonEventCodes.EventCodes.VRChatRPC => RPC.IsGoodRPC(__0),
                    _ => true
                };
            }
            catch (Exception e)
            {
                ExceptionHandler("OnEvent", e);
                return true;
            }
        }


        internal static bool OnEventSent(byte eventCode, ref Il2CppSystem.Object eventData,
            ref RaiseEventOptions raiseEventOptions)
        {
            Log("Code", eventCode);
            Log("eventData", eventData.ToString());
            Log("raiseEventOptions", raiseEventOptions.ToString());


            return true;
        }
    }
}
