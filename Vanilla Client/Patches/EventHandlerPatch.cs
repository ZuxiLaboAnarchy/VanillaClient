// /*
//  *
//  * VanillaClient - EventHandlerPatch.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using System.Collections.Generic;
using Vanilla.Patches.Manager;

namespace Vanilla.Patches
{
    internal class EventHandlerPatch : VanillaPatches
    {
        private static readonly Dictionary<int, Dictionary<int, float>> eventFilter = new();

        //private static readonly float EventFilterTimer = 30f;

        private static readonly Dictionary<int, float> lastSpawnEmojiTime = new();

        private static readonly List<string> whitelistedRpcs = new()
        {
            "initUSpeakSenderRPC", "SendVoiceSetupToPlayerRPC", "ReceiveVoiceStatsSyncRPC", "InteractWithStationRPC",
            "_InstantiateObject", "_SendOnSpawn", "_DestroyObject", "SanityCheck", "ChangeVisibility", "Respawn",
            "ReapObject", "TakeOwnership", "SendStrokeRPC", "SpawnEmojiRPC", "PlayEmoteRPC", "TeleportRPC",
            "IncrementPortalPlayerCountRPC", "SyncWorldInstanceIdRPC", "SetTimerRPC", "CancelRPC",
            "ConfigurePortal", "UdonSyncRunProgramAsRPC", "PhotoCapture", "TimerBloop", "PlayEffect",
            "ReloadAvatarNetworkedRPC", "InternalApplyOverrideRPC", "InformOfBadConnection", "AddURL", "Play",
            "Pause", "Clear", "RemoteClear", "NetworkedQg", "NetworkedQuack", "NetworkedException", "NetworkedCowboy",
            "NetworkedAllah", "NetworkedGay", "Zuxi_Networked_Join_VanillaClient"
        };

        protected override string patchName => "EventHandlerPatch";

        internal override void Patch()
        {
            /*
                InitializeLocalPatchHandler(typeof(EventHandlerPatch));
                PatchMethod(typeof(VRC_EventHandler).GetMethod("InternalTriggerEvent"), GetLocalPatch("OnEventDataSentPatch"), null);
                PatchMethod(typeof(FlatBufferNetworkSerializer).GetMethod("Method_Public_Void_EventData_0"), GetLocalPatch("FlatBufferNetworkSerializeReceivePatch"), null);
                MethodInfo[] methods = typeof(VRC_EventLog.EventReplicator).GetMethods(BindingFlags.Instance | BindingFlags.Public);
                foreach (MethodInfo methodInfo in methods)
                {
                    if (methodInfo.Name.StartsWith("Method_Public_Virtual_Final_New_Void_EventData_"))
                    {
                        PatchMethod(methodInfo, GetLocalPatch("OnEventDataReceivedPatch"), null);
                    }
                }
                */
        }
        /*

        private static bool OnEventDataSentPatch(ref VRC_EventHandler.VrcBroadcastType __1)
        {
            if (Configuration.GetGeneralConfig().WorldTriggers && (__1 != 0 || __1 != VRC_EventHandler.VrcBroadcastType.AlwaysBufferOne || __1 != VRC_EventHandler.VrcBroadcastType.AlwaysUnbuffered))
            {
                __1 = VRC_EventHandler.VrcBroadcastType.Always;
            }
            return true;
        }

        private static bool FlatBufferNetworkSerializeReceivePatch(EventData __0)
        {
            if (__0.Code != 9)
            {
                return true;
            }
            if (MainConfig.GetInstance().AntiFreezeExploit && !IsGoodSerializedData(__0))
            {
                return false;
            }
            return true;
        }

        private static bool IsGoodSerializedData(EventData eventData)
        {
            if (IsPlayerFiltered(eventData.Sender, eventData.Code))
            {
                return false;
            }
            Il2CppArrayBase<byte> il2CppArrayBase = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.CustomData.Pointer);
            if (il2CppArrayBase.Length <= 10)
            {
                FilterPlayer(eventData.Sender, eventData.Code);
                return false;
            }
            if (System.BitConverter.ToInt32(il2CppArrayBase, 4) == 0)
            {
                FilterPlayer(eventData.Sender, eventData.Code);
                return false;
            }
            return true;
        }
          * /
       private static bool OnEventDataReceivedPatch(EventData __0)


        {
            if (__0.Code != 6)
            {
                return true;
            }
          //  return IsGoodRPC(__0);
            /*
            if (__0.Code != 6)
            {
                return true;
            }
            if (Configuration.GetGeneralConfig().AntiFreezeExploit && !IsGoodRPC(__0))
            {
                return false;
            }
            return true;*/
    }
}
