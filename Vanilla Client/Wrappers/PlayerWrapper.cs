// /*
//  *
//  * VanillaClient - PlayerWrapper.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using MelonLoader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.UI;
using VRC.UI.Elements.Menus;

namespace Vanilla.Wrappers
{
    internal static class PlayerWrapper
    {
        private static SelectedUserMenuQM _selectedUserMenu;

        internal static readonly Dictionary<string, PlayerInformation> playerCachingList = new();

        internal static VRCPlayer GetLocalPlayer()
        {
            return VRCPlayer.field_Internal_Static_VRCPlayer_0;
        }

        public static VRCPlayer GetSelectedUser()
        {
            if (_selectedUserMenu == null)
            {
                _selectedUserMenu = GameObject
                    .Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local")
                    .GetComponent<SelectedUserMenuQM>();
            }

            if (_selectedUserMenu == null)
            {
                Log("Selected User", "_selectedUserMenu is null!");
                return null;
            }

            var iUser = _selectedUserMenu.field_Private_IUser_0.Cast<Object1PublicOb1ApStBo1SiStBoTeUnique>();
            return GetPlayerInformationByID(iUser.prop_String_0).vrcPlayer;
        }


        internal static Player LocalPlayer()
        {
            return Player.prop_Player_0;
        }

        internal static Quaternion GetPlayerRotation()
        {
            return GetLocalPlayer().transform.rotation;
        }

        internal static Vector3 GetPlayerPosition()
        {
            return GetLocalPlayer().transform.position;
        }

        internal static VRCPlayer GetCurrentPlayerObject()
        {
            return VRCPlayer.field_Internal_Static_VRCPlayer_0;
        }

        // internal static APIUser GetLocal() { return APIUser.CurrentUser;  }
        public static void SendToLocation(Vector3 pos, Quaternion rot)
        {
            GetLocalPlayer().transform.position = pos;
            GetLocalPlayer().transform.rotation = rot;
        }

        internal static bool IsLocalPlayer(Player player)
        {
            return player.prop_APIUser_0.id == APIUser.CurrentUser.id;
        }

        internal class Target
        {
            internal static Player targertuser;

            internal static void Targetuser(string userid)
            {
                var players = GetSelectedUser();
            }
        }

        internal static void ChangePlayerAvatar(string avatarId)
        {
            new ApiAvatar() { id = avatarId }.Get(new Action<ApiContainer>(x =>
                {
                    GameObject.Find("MenuContent/Screens/Avatar").GetComponent<PageAvatar>()
                        .field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = x.Model.Cast<ApiAvatar>();
                    GameObject.Find("MenuContent/Screens/Avatar").GetComponent<PageAvatar>().ChangeToSelectedAvatar();
                }),
                new Action<ApiContainer>(x =>
                {
                    Log("Player", $"Failed to switch to avatar: {avatarId} ({x.Error})");
                }), null, false);
        }


        internal static PlayerInformation GetPlayerInformationByInstagatorID(int index)
        {
            foreach (var playerCaching in playerCachingList)
            {
                if (playerCaching.Value.networkBehaviour.prop_Int32_0 == index)
                {
                    return playerCaching.Value;
                }
            }

            return null;
        }

        internal static List<VRCPlayer> GetAllVRCPlayers()
        {
            List<VRCPlayer> vrcPlayer = new();
            foreach (var playerCaching in playerCachingList)
            {
                vrcPlayer.Add(playerCaching.Value.vrcPlayer);
            }

            return vrcPlayer;
        }

        internal static Il2CppSystem.Collections.Generic.List<Player> GetAllPlayers()
        {
            return PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0;
        }

        internal static void Start(this IEnumerator instance)
        {
            MelonCoroutines.Start(instance);
        }

        internal static void HideSelf(bool state)
        {
            AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0.gameObject.SetActive(!state);
            GetAvatarPreviewBase().SetActive(!state);
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCAvatarManager_0.gameObject.SetActive(!state);
            //  if (!state) { PlayerUtils.ReloadAvatar(PlayerRankStatus.Local); }
        }

        internal static Player PlayerObject()
        {
            return Player.prop_Player_0;
        }

        internal static APIUser GetLocalAPIUser()
        {
            return APIUser.CurrentUser;
        }

        internal static GameObject GetAvatarPreviewBase()
        {
            if (avatarPreviewBase == null)
            {
                avatarPreviewBase = GameObject.Find("MenuContent/Screens/Avatar/AvatarPreviewBase");
            }

            return avatarPreviewBase;
        }

        private static GameObject avatarPreviewBase;

        internal static VRCPlayer GetCurrentPlayer()
        {
            return VRCPlayer.field_Internal_Static_VRCPlayer_0;
        }

        internal static PlayerInformation GetPlayerInformationByName(string displayName)
        {
            if (displayName == APIUser.CurrentUser.displayName)
            {
                return GetLocalPlayerInformation();
            }

            if (playerCachingList.ContainsKey(displayName))
            {
                return playerCachingList[displayName];
            }

            return null;
        }

        internal static Player LoclPayer => Player.prop_Player_0;

        internal static PlayerInformation localPlayerInfo = null;

        internal static PlayerInformation GetLocalPlayerInformation()
        {
            if (localPlayerInfo == null)
            {
                if (APIUser.CurrentUser != null && playerCachingList.ContainsKey(APIUser.CurrentUser.displayName))
                {
                    localPlayerInfo = playerCachingList[APIUser.CurrentUser.displayName];
                    return playerCachingList[APIUser.CurrentUser.displayName];
                }

                return null;
            }

            return localPlayerInfo;
        }

        internal static PlayerInformation GetPlayerInformation(Player player)
        {
            var text = string.Empty;
            if (player != null)
            {
                if (player.prop_APIUser_0 != null)
                {
                    text = player.prop_APIUser_0.displayName;
                }
                else if (player.prop_VRCPlayerApi_0 != null)
                {
                    text = player.prop_VRCPlayerApi_0.displayName;
                }
            }

            if (text == string.Empty)
            {
                return null;
            }

            if (APIUser.CurrentUser != null && APIUser.CurrentUser.displayName == text)
            {
                return GetLocalPlayerInformation();
            }

            if (PlayerUtils.playerCachingList.ContainsKey(text))
            {
                return PlayerUtils.playerCachingList[text];
            }

            return null;
        }

        internal static PlayerInformation GetPlayerInformationByID(string id)
        {
            if (id == APIUser.CurrentUser?.id)
            {
                return GetLocalPlayerInformation();
            }

            if (PlayerUtils.playerCachingList.Count == 0)
            {
                return null;
            }

            foreach (var playerCaching in PlayerUtils.playerCachingList)
            {
                if (playerCaching.Value.id == id)
                {
                    return playerCaching.Value;
                }
            }

            return null;
        }
    }
}
