// /*
//  *
//  * VanillaClient - ProtectionHandler.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using ExitGames.Client.Photon;
using UnhollowerBaseLib;

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
