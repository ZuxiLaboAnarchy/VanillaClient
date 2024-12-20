﻿// /*
//  *
//  * VanillaClient - GeneralWrapper.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using MelonLoader;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;
using VRC.UI;

namespace Vanilla.Wrappers
{
    internal static class GeneralWrappers
    {
        public static Player LocalPlayer()
        {
            return Player.prop_Player_0;
        }

        private static Player[] GetAllPlayers()
        {
            return PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray();
        }

        internal static bool IsFriend(this Player player)
        {
            return APIUser.CurrentUser.friendIDs.Contains(player.field_Private_APIUser_0.id);
        }

        private static Camera uiCamera;
        private static Camera photoCamera;
        private static GameObject reticleObj;


        internal static ApiWorld GetWorld()
        {
            return RoomManager.field_Internal_Static_ApiWorld_0;
        }

        internal static ApiWorldInstance GetWorldInstance()
        {
            return RoomManager.field_Internal_Static_ApiWorldInstance_0;
        }

        internal static bool IsInWorld()
        {
            if (GetWorld() == null)
            {
                return GetWorldInstance() != null;
            }

            return true;
        }

        internal static void GoToRoom(string id)
        {
            Networking.GoToRoom(id);
        }


        internal static VRCUiManager GetVRCUiManager()
        {
            return VRCUiManager.prop_VRCUiManager_0;
        }

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
                photoCamera = FPVCameraController.field_Public_Static_FPVCameraController_0.field_Public_GameObject_0
                    .GetComponent<Camera>();
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
            if (RoomManager.field_Internal_Static_ApiWorldInstance_0 is null)
            {
                return;
            }

            GeneralUtils.SetClipboard(RoomManager.field_Internal_Static_ApiWorldInstance_0.id);
            Log("World", "Copied \"" + RoomManager.field_Internal_Static_ApiWorldInstance_0.id + "\".");
        }

        public static void JoinInstanceFromClipboard()
        {
            var text = Clipboard.GetText();
            if (string.IsNullOrEmpty(text))
            {
                Log("JoinWorld", "World ID Was empty", ConsoleColor.Red);
            }
            else
            {
                GoToRoom(text);
            }
        }

        internal static bool IsClientUser(PlayerInformation player)
        {
            if (player.isClientUser)
            {
                return true;
            }

            if (player.lastNetworkedUpdatePacketNumber <= 1)
            {
                return false;
            }

            if (player.GetPing() < 10)
            {
                Log("Detector", player.displayName + " is a client user (2)", ConsoleColor.Gray, "IsClientUser");
                player.isClientUser = true;
                return true;
            }

            if (player.GetFPS() < 1)
            {
                Log("Detector", player.displayName + " is a client user (3)", ConsoleColor.Gray, "IsClientUser");
                player.isClientUser = true;
                return true;
            }

            if (player.isQuestUser)
            {
                if (player.GetFPS() > 120 || !player.isVRUser)
                {
                    Log("Detector", player.displayName + " is a client user (4)", ConsoleColor.Gray, "IsClientUser");
                    player.isClientUser = true;
                    return true;
                }
            }
            else if (player.GetFPS() > 144 || player.GetPing() > 3000)
            {
                Log("Detector", player.displayName + " is a client user (5)", ConsoleColor.Gray, "IsClientUser");
                player.isClientUser = true;
                return true;
            }

            if (player.detectedFirstGround)
            {
                if (player.GetVelocity().y == 0f && !player.IsGrounded())
                {
                    player.airstuckDetections++;
                    if (player.airstuckDetections >= 5)
                    {
                        Log("Detector", player.displayName + " is a client user (6)", ConsoleColor.Gray,
                            "IsClientUser");
                        player.isClientUser = true;
                        return true;
                    }
                }
                else if (player.airstuckDetections > 0)
                {
                    player.airstuckDetections--;
                }
            }
            else if (player.IsGrounded())
            {
                player.detectedFirstGround = true;
            }

            return false;
        }

        internal static List<T> FindAllComponentsInGameObject<T>(GameObject gameObject, bool includeInactive = true,
            bool searchParent = true, bool searchChildren = true) where T : class
        {
            var list = new List<T>();
            if (gameObject == null)
            {
                return list;
            }

            try
            {
                foreach (var component in gameObject.GetComponents<T>())
                {
                    list.Add(component);
                }

                if (searchParent && gameObject.transform.parent != null)
                {
                    foreach (var item in gameObject.GetComponentsInParent<T>(includeInactive))
                    {
                        list.Add(item);
                    }
                }

                if (searchChildren && gameObject.transform.childCount > 0)
                {
                    foreach (var componentsInChild in gameObject.GetComponentsInChildren<T>(includeInactive))
                    {
                        list.Add(componentsInChild);
                    }
                }
            }
            catch (Exception e)
            {
                MelonLogger.Error("Misc", "FindAllComponentsInGameObject", e, "FindAllComponentsInGameObject", 388);
            }

            return list;
        }

        private static PageUserInfo pageUserInfo;

        internal static PageUserInfo GetPageUserInfo()
        {
            if (pageUserInfo == null)
            {
                pageUserInfo =
                    GameObject.Find("MenuContent/Screens/UserInfo")
                        .GetComponent<PageUserInfo>(); //adjusted for guid change
            }

            return pageUserInfo;
        }
    }
}
