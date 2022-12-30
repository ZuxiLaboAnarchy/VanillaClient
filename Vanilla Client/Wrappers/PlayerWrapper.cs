using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.UI;

namespace Vanilla.Wrappers
{
    internal class PlayerWrapper
    {
        internal static readonly System.Collections.Generic.Dictionary<string, PlayerInformation> playerCachingList = new System.Collections.Generic.Dictionary<string, PlayerInformation>();
        internal static VRCPlayer GetLocalPlayer()
        {
            return VRCPlayer.field_Internal_Static_VRCPlayer_0;
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
            internal static VRC.Player targertuser;
            private static GameObject targetplate;
            internal static void Targetuser(string userid)
            {
                if (targetplate != null)
                    GameObject.DestroyImmediate(targetplate);

                var players = PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray().Where(player => player.field_Private_APIUser_0.id == userid).FirstOrDefault();
                targertuser = players;
            }
        }

        internal static void ChangePlayerAvatar(string avatarId)
        {
            new ApiAvatar() { id = avatarId }.Get(new System.Action<ApiContainer>(x =>
            {
                GameObject.Find("MenuContent/Screens/Avatar").GetComponent<PageAvatar>().field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = x.Model.Cast<ApiAvatar>();
                GameObject.Find("MenuContent/Screens/Avatar").GetComponent<PageAvatar>().ChangeToSelectedAvatar();
            }),
            new System.Action<ApiContainer>(x =>
            {
                Log("Player", $"Failed to switch to avatar: {avatarId} ({x.Error})");
            }), null, false);
        }

        internal static PlayerInformation GetPlayerInformationByInstagatorID(int index)
        {
            foreach (KeyValuePair<string, PlayerInformation> playerCaching in PlayerWrapper.playerCachingList)
            {
                if (playerCaching.Value.networkBehaviour.prop_Int32_0 == index)
                {
                    return playerCaching.Value;
                }
            }
            return null;
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
    }
}
