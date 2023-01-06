using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

namespace Vanilla.Utils
{ 
    internal class PhotonUtils
    {
        internal static void OpRaiseEvent(byte code, object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
        {
            Il2CppSystem.Object customObject2 = SerializationUtils.FromManagedToIL2CPP<Il2CppSystem.Object>(customObject);
            OpRaiseEvent(code, customObject2, RaiseEventOptions, sendOptions);
        }

        internal static void OpRaiseEvent(byte code, Il2CppSystem.Object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
        {
            PhotonNetwork.field_Public_Static_LoadBalancingClient_0.Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0(code, customObject, RaiseEventOptions, sendOptions);
        }
    }
}
