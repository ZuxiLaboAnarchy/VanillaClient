using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;
using VRC;
using VRC.UserCamera;
using VRC.SDKBase;
using System.Windows.Forms;
using MelonLoader;

namespace Vanilla.Wrappers
{
    internal static class GeneralWrappers
    {
        private static VRC_EventHandler.VrcEvent _vrcEvent;

        private static VRC_Trigger _vrc_Trigger;
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
        public static void CopyInstanceToClipboard()
        {
            ApiWorldInstance field_Internal_Static_ApiWorldInstance_ = RoomManager.field_Internal_Static_ApiWorldInstance_0;
            Clipboard.SetText(field_Internal_Static_ApiWorldInstance_.id);
            MelonLogger.Msg("Copied \"" + field_Internal_Static_ApiWorldInstance_.id + "\".");
        }
        public static void GoToRoom(string id)
        {
            Networking.GoToRoom(id);
        }
        public static void JoinInstanceFromClipboard()
        {
            string text = Clipboard.GetText();
            if (string.IsNullOrEmpty(text))
            {
                MelonLogger.Error("Clipboard oof");
            }
            else
            {
                GoToRoom(text);
            }
        }
    }
}
