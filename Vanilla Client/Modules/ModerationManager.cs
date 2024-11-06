// /*
//  *
//  * VanillaClient - ModerationManager.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using ExitGames.Client.Photon;
using Il2CppSystem.Collections.Generic;
using Vanilla.Modules.Photon;


namespace Vanilla.Modules
{
    internal class ModerationManager
    {
        internal static bool OnEvent(EventData eventData)
        {

            var dictionary = eventData.Parameters[eventData.CustomDataKey]
                .Cast<Dictionary<byte, Il2CppSystem.Object>>();
            var moderationEventCode = (PhotonModerationCodes)dictionary[0].Unbox<byte>();

            return moderationEventCode switch
            {
                PhotonModerationCodes.MuteAndBlock => false,
                //  PhotonEventCodes.EventCodes.Moderations => false,
                //  PhotonEventCodes.EventCodes.SetPlayerData => MainHelper.AvatarLogHandler(),
                //  PhotonEventCodes.EventCodes.AuthEvent => MainHelper.AvatarLogHandler(),
                _ => true
            };
        }
    }
}
