using ExitGames.Client.Photon;
using UnhollowerBaseLib;
using Vanilla.Config;
using Vanilla.QM.Menu;

namespace Vanilla.Wrappers
{
    internal class ProtectionHandler
    {
        internal static bool IsEvent1Bad(EventData eventData)
        {
            if (GetInstance().AntiE1)
            {
                byte[] voiceData = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.CustomData.Pointer);
                return AudioHandler.IsVoiceDataBad(eventData.Sender, voiceData);
            }

            return false;
        }
    }
}