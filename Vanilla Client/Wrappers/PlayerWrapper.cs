using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;
using VRC.UI;

namespace Vanilla.Wrappers
{
    internal class PlayerWrapper
    {
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
                targetplate = players._vrcplayer.GeneratePlate("<color=red>Selected");
            }
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

        internal static void HideSelf(bool state)
        {
            AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0.gameObject.SetActive(!state);
            GetAvatarPreviewBase().SetActive(!state);
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCAvatarManager_0.gameObject.SetActive(!state);
           
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
    }
