using ExitGames.Client.Photon;
using UnhollowerBaseLib;
using Vanilla.QM.Menu;

namespace Vanilla.Wrappers
{
    internal class ProtectionHandler
    {
        internal static bool IsEvent1Bad(EventData eventData)
        {
            if (Safetymenu.Antirape)
            {
                byte[] voiceData = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.CustomData.Pointer);
                return (AudioHandler.IsVoiceDataBad(eventData.Sender, voiceData));
            }
            return false;
        }
    }
}
