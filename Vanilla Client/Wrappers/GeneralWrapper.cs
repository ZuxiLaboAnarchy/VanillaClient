using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;
using VRC;
using VRC.UserCamera;

namespace Vanilla.Wrappers
{
    internal static class GeneralWrappers
    {
        public static Player LocalPlayer() => Player.prop_Player_0;
        private static Player[] GetAllPlayer() => PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray();
        internal static bool IsFriend(this VRC.Player player) => APIUser.CurrentUser.friendIDs.Contains(player.field_Private_APIUser_0.id);
        private static Camera uiCamera;
        private static Camera photoCamera;
        private static GameObject reticleObj;
        internal static Camera GetPlayerCamera()
        {
            return VRCVrCamera.field_Private_Static_VRCVrCamera_0.field_Public_Camera_0;
        }
        internal static Camera GetUICamera()
        {
            if (uiCamera == null)
            {
                uiCamera = GetPlayerCamera().transform.Find("StackedCamera : Cam_InternalUI").GetComponent<Camera>();
            }
            return uiCamera;
        }

        internal static Camera GetPhotoCamera()
        {
            if (photoCamera == null)
            {
                photoCamera = UserCameraController.field_Public_Static_UserCameraController_0.field_Public_GameObject_0.GetComponent<Camera>();
            }
            return photoCamera;
        }

        internal static GameObject GetReticle()
        {
            if (reticleObj == null)
            {
                reticleObj = GameObject.Find("UserInterface/UnscaledUI/HudContent_Old/Hud/ReticleParent");
            }
            return reticleObj;
        }
    }
}
