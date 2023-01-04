using MelonLoader;
using Photon.Pun;
using static BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests.SkeinEngine;
using TMPro;
using UnityEngine;
using Vanilla.Wrappers;
using VRC.Core;
using VRC.SDKBase.Validation.Performance;
using VRC;
using Vanilla.Config;
using UnityEngine.UI;

namespace Vanilla.Modules
{
    internal class PlayerController : VanillaModule
    {
        protected override string ModuleName => "PlayerController";


        #region Nameplates
        public static Transform GetBasePlate(VRC.Player PlayerData, NameplateManager[] Nameplates)
        {
            foreach (NameplateManager NameplateContainer in Nameplates)
            {
                string PUID = NameplateContainer.field_Public_VRCPlayer_0._player.field_Private_APIUser_0.id;
                if (PUID == PlayerData.prop_APIUser_0.id) { return NameplateContainer.gameObject.transform; }
            }
            MelonLogger.Msg("Nameplate search returned null");
            return null;
        }

        internal override void WorldUnload(int level)
        {
            PlayerUtils.playerCachingList.Clear();
        }

        internal override void PlayerJoin(VRC.Player __0)
        {
            if ( __0 == null || PlayerWrapper.GetPlayerInformation(__0) != null) { return; }
            bool flag = __0.prop_APIUser_0.id == APIUser.CurrentUser.id;
            GameObject canvas = null;
            ImageThreeSlice nameplateBackground = null;
            Image nameplateIconBackground = null;
            GameObject CreatedPlate = null;
            RectTransform rectTransform = null;
            TextMeshProUGUI textMeshProUGUI = null;
            try //total hours wasted debugging this code: 8
            { //im never touching this code again
                NameplateManager[] PlateManager = UnityEngine.Object.FindObjectsOfType<NameplateManager>();
                canvas = GetBasePlate(__0, PlateManager).Find("PlayerNameplate/Canvas").gameObject;
                //returning the transform and dropping the static variable was the solution
                nameplateBackground = canvas.transform.Find("NameplateGroup/Nameplate/Contents/Main/Background").GetComponent<ImageThreeSlice>();
                nameplateIconBackground = canvas.transform.Find("NameplateGroup/Nameplate/Contents/Icon/Background").GetComponent<Image>();
                if (!flag)
                {
                    Transform transform = canvas.transform.Find("NameplateGroup/Nameplate/Contents");
                    GameObject gameObject3 = transform.transform.Find("Quick Stats").gameObject;

                    CreatedPlate = UnityEngine.Object.Instantiate(gameObject3, transform);
                    rectTransform = CreatedPlate.GetComponent<RectTransform>();
                    rectTransform.localPosition = MiscUtils.GetNameplateOffset(RuntimeConfig.isQuickMenuOpen);
                    foreach (RectTransform componentsInChild in CreatedPlate.GetComponentsInChildren<RectTransform>())
                    {
                        if (componentsInChild.name != "Trust Text")
                        {
                            componentsInChild.gameObject.SetActive(value: false);
                            continue;
                        }
                        textMeshProUGUI = componentsInChild.GetComponent<TextMeshProUGUI>();
                        textMeshProUGUI.text = "VanillaPlate";
                    }
                    if (MainConfig.NameplateMoreInfo)
                    {
                        CreatedPlate.SetActive(value: true);
                    }
                    else
                    {
                        CreatedPlate.SetActive(value: false);
                    }
                }
                if (MainConfig.NameplateWallhack && CameraModule.GetCameraSetup() == 0)
                {
                    canvas.layer = 19;
                }
            }
            catch (Exception e)
            {
               ExceptionHandler("PlateController", e);
            }
            PlayerInformation playerInformation;
            try
            {
                playerInformation = new PlayerInformation
                {
                    actorId = ((VRCNetworkBehaviour)__0.prop_VRCPlayer_0).prop_Int32_1,
                    actorIdData = ((VRCNetworkBehaviour)__0.prop_VRCPlayer_0).prop_Int32_0 * PhotonNetwork.field_Public_Static_Int32_0 + 1,
                    actorIdDataOther = ((VRCNetworkBehaviour)__0.prop_VRCPlayer_0).prop_Int32_0 * PhotonNetwork.field_Public_Static_Int32_0 + 3,
                    id = __0.prop_APIUser_0.id,
                    displayName = __0.prop_APIUser_0.displayName,
                    isLocalPlayer = flag,
                    isInstanceMaster = __0.prop_VRCPlayerApi_0.isMaster,
                    isVRChatStaff = false,
                    isVRUser = __0.prop_VRCPlayerApi_0.IsUserInVR(),
                    isQuestUser = (__0.prop_APIUser_0.last_platform != "standalonewindows"),
                    isClientUser = false,
                    blockedLocalPlayer = false,
                    rankStatus = PlayerRankStatus.Unknown,
                    player = __0,
                    playerApi = __0.prop_VRCPlayerApi_0,
                    vrcPlayer = __0.prop_VRCPlayer_0,
                    apiUser = __0.prop_APIUser_0,
                    networkBehaviour = __0.prop_VRCPlayer_0,
                    uSpeaker = __0.prop_VRCPlayer_0.prop_USpeaker_0,
                    input = __0.prop_VRCPlayer_0.GetComponent<GamelikeInputController>(),
                    airstuckDetections = 0,
                    lastNetworkedUpdatePacketNumber = __0.prop_PlayerNet_0.field_Private_Int32_0,
                    lastNetworkedUpdateTime = Time.realtimeSinceStartup,
                    lastNetworkedVoicePacket = 0f,
                    lagBarrier = 0,
                    nameplateCanvas = canvas,
                    nameplateBackground = nameplateBackground,
                    nameplateIconBackground = nameplateIconBackground,
                    customNameplateObject = CreatedPlate,
                    customNameplateTransform = rectTransform,
                    customNameplateText = textMeshProUGUI
                };
            }
            catch (Exception e2)
            {
                ExceptionHandler("PatchManager", e2);
                return;
            }
            try
            {

                
                if (!PlayerUtils.playerColorCache.ContainsKey(__0.prop_APIUser_0.displayName))
                {
                    PlayerUtils.playerColorCache.Add(__0.prop_APIUser_0.displayName, VRCPlayer.Method_Public_Static_Color_APIUser_0(__0.prop_APIUser_0));
                }
                else
                {
                    PlayerUtils.playerColorCache[__0.prop_APIUser_0.displayName] = VRCPlayer.Method_Public_Static_Color_APIUser_0(__0.prop_APIUser_0);
                }
                PlayerUtils.playerCachingList.Add(playerInformation.displayName, playerInformation);
            }
            catch (Exception e3)
            {
                ExceptionHandler("PlayerJoinModule", e3);
                return;
            }
          //  ModuleManager.OnPlayerJoin(playerInformation);
            try
            {
                if (__0.prop_APIUser_0.tags.Contains("admin_moderator") || __0.prop_APIUser_0.developerType == APIUser.DeveloperType.Internal || __0.prop_APIUser_0.developerType == APIUser.DeveloperType.Moderator)
                {
                    playerInformation.isVRChatStaff = true;
                    if (MainConfig.LogModerations)
                    {
                        string text = "<color=red>WARNING: <color=white>VRChat Staff joined: <color=purple>" + playerInformation.displayName;
                        HudLog("JOIN", text);
                        Log("Join", text, ConsoleColor.Red);
                    }
                }
            }
            catch (Exception e4)
            {
                ExceptionHandler("PlayerJoinModule", e4);
            }
        }
        #endregion


       internal override void PlayerLeave(VRC.Player __0)
        {
            if (__0 == null)
            {
                return;
            }
            PlayerInformation playerInformation = PlayerWrapper.GetPlayerInformation(__0);
            if (playerInformation != null && PlayerUtils.playerCachingList.ContainsKey(playerInformation.displayName))
            {
                PlayerUtils.playerCachingList.Remove(playerInformation.displayName);
            }
        }
    }
}
