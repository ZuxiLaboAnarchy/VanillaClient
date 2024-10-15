using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using Vanilla.Wrappers;
using VRCSDK2;

namespace Vanilla.Modules.Photon
{
    internal class RPC
    {
        internal static bool IsGoodRPC(EventData eventData)
        {
            /*     if (IsPlayerFiltered(eventData.Sender, eventData.Code))
                 {
                     return false;
                 }
                 Il2CppSystem.Object param_2;
                 try
                 {
                     Il2CppStructArray<byte> param_ = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.CustomData.Pointer).Cast<Il2CppStructArray<byte>>();
                     BinarySerializer.Method_Public_Static_Boolean_ArrayOf_Byte_byref_Object_0(param_, out param_2);
                 }
                 catch (Il2CppException)
                 {
                     FilterPlayer(eventData.Sender, eventData.Code);
                     return false;
                 }
                 if (param_2 == null)
                 {
                     return false;
                 }
                 VRC_EventLog.EventLogEntry eventLogEntry = param_2.TryCast<VRC_EventLog.EventLogEntry>();
                 if (eventLogEntry.field_Private_Int32_1 != eventData.Sender)
                 {
                     FilterPlayer(eventData.Sender, eventData.Code);
                     return false;
                 }
                 VRC_EventHandler.VrcEvent field_Private_VrcEvent_ = eventLogEntry.field_Private_VrcEvent_0;
                 if (field_Private_VrcEvent_.EventType > VRC_EventHandler.VrcEventType.CallUdonMethod)
                 {
                     FilterPlayer(eventData.Sender, eventData.Code);
                     return false;
                 } */
            Il2CppSystem.Object param_2;
            try
            {
                Il2CppStructArray<byte> param_ = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.CustomData.Pointer).Cast<Il2CppStructArray<byte>>();
                BinarySerializer.Method_Public_Static_Boolean_ArrayOf_Byte_byref_Object_0(param_, out param_2);
            }
            catch (Il2CppException)
            {
                // FilterPlayer(eventData.Sender, eventData.Code);
                return false;
            }
            VRC_EventLog.EventLogEntry eventLogEntry = param_2.TryCast<VRC_EventLog.EventLogEntry>();
            VRC_EventHandler.VrcEvent field_Private_VrcEvent_ = eventLogEntry.field_Private_VrcEvent_0;
            PlayerInformation playerInformationByInstagatorID = PlayerWrapper.GetPlayerInformationByInstagatorID(eventData.Sender);
            if (playerInformationByInstagatorID != null && playerInformationByInstagatorID.isLocalPlayer)
            {
                return true;
            }

            if (playerInformationByInstagatorID != null)
            {
                Dev("RPCMAnager", "playerInformationByInstagatorID  wasnt null");
                switch (field_Private_VrcEvent_.ParameterString)
                {
                    case "Zuxi_Networked_Join_VanillaClient":
                        Dev("RPC", $"{playerInformationByInstagatorID.apiUser.username} Is Using Vanilla");
                        return false;

                }
            }
            return true;
        }
        /*
        internal static bool IsPlayerFiltered(int actorId, int eventID)
        {
            if (!eventFilter.ContainsKey(actorId))
            {
                return false;
            }
            if (!eventFilter[actorId].ContainsKey(eventID))
            {
                return false;
            }
            if (eventFilter[actorId][eventID] <= Time.realtimeSinceStartup)
            {
                return false;
            }
            return true;
        }*/
    }

}
