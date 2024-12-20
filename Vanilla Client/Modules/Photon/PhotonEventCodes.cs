﻿// /*
//  *
//  * VanillaClient - PhotonEventCodes.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

namespace Vanilla.Modules.Photon
{
    internal class PhotonEventCodes
    {
        internal enum EventCodes : byte
        {
            VoiceData = 1,
            ServerMessage = 2,
            MasterClientSync = 3,
            CachedEvent = 4,
            MasterClientSyncFinished = 5,
            VRChatRPC = 6,
            SerializedData = 7,
            InterestManagement = 8,
            SerializedDataReliable = 9,
            Moderations = 33,
            PhotonEventLimits = 34,
            PhotonHeartbeat = 35,
            AvatarRefresh = 40,
            SetPlayerData = 42,
            PhysBonesPermissions = 60,
            PhotonRPC = 200,
            SendSerialize = 201,
            Instantiate = 202,
            CloseConnection = 203,
            Destroy = 204,
            RemoveCachedRPCs = 205,
            SendSerializeReliable = 206,
            DestroyPlayer = 207,
            AssignMasterClient = 208,
            OwnershipRequest = 209,
            OwnershipTransfer = 210,
            VacantViewIds = 211,
            LevelReload = 212,
            PhotonAppId = 220,
            AuthEvent = 223,
            LobbyStats = 224,
            AppStats = 226,
            Match = 227,
            QueueState = 228,
            GameListUpdate = 229,
            GameList = 230,
            CacheSliceChanged = 250,
            ErrorInfo = 251,
            SetProperties = 253,
            LeavePhoton = 254,
            JoinPhoton = byte.MaxValue
        }
    }
}
