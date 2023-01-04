using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests.SkeinEngine;
using UnhollowerBaseLib;
using ExitGames.Client.Photon;
using Vanilla.QM.Menu;

namespace Vanilla.Wrappers
{
    internal class ProtectionHandler
    {
        internal static bool IsEvent1Bad(ref EventData eventData)
        {
            if (Safetymenu.Antirape)
            {
                byte[] voiceData = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.CustomData.Pointer);
                if (AudioHandler.IsVoiceDataBad(eventData.Sender, voiceData))
                {
                    PlayerInformation playerInformationByInstagatorID = PlayerWrapper.GetPlayerInformationByInstagatorID(eventData.Sender);
                }
            }
            return false;
        }
    }
}
