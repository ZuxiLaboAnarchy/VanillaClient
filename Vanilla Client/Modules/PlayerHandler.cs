using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using MelonLoader;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Vanilla.Config;
using Vanilla.Wrappers;
using VRC.SDKBase;
using static BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests.SkeinEngine;

namespace Vanilla.Modules
{
    internal class PlayerHandler : VanillaModule
    {

        private static int currentPlayerNameplateUpdatingIndex = 0;

        protected override string ModuleName => "Player Handler";

        private static readonly int playerLayerMask = -527361;




        internal override void Update() //error here
        {
            if (!RuntimeConfig.isConnectedToInstance)
            {
                return;
            }
            PlayerInformation localPlayerInformation = PlayerWrapper.GetLocalPlayerInformation();
            if (localPlayerInformation == null)
            {
                return;
            }
           
            
        }

       

   

        internal override void LateUpdate()
        {
            UpdateNextPlayer();
        }

        private static void UpdateNextPlayer()
        {
            if (PlayerUtils.playerCachingList.Count == 0)
            {
                return;
            }
            currentPlayerNameplateUpdatingIndex++;
            if (currentPlayerNameplateUpdatingIndex > PlayerUtils.playerCachingList.Count - 1)
            {
                currentPlayerNameplateUpdatingIndex = 0;
            }
            PlayerInformation value = PlayerUtils.playerCachingList.ElementAt(currentPlayerNameplateUpdatingIndex).Value;
            if (value != null)
            {
                bool flag = UpdatePlayerVisibility(value);
               UpdatePlayerNameplate(value, flag);
              
            }
        }

        private static bool UpdatePlayerVisibility(PlayerInformation playerInfo)
        {
          
            if (playerInfo.isLocalPlayer)
            {
                return false;
            }
            if (playerInfo.GetAvatar() == null)
            {
                return false;
            }
            float num = Vector3.Distance(playerInfo.GetAvatar().transform.position, CameraModule.GetActiveCamera().transform.position);
            if (num > 25f)
            {
                //SetAvatarVisibility(playerInfo, state: false);
                return true;
            }
            if (num > 2f)
            {
                Vector3 direction = CameraModule.GetActiveCamera().transform.TransformDirection(Vector3.forward);
                RaycastHit hitInfo;
                bool flag = !Physics.Raycast(CameraModule.GetActiveCamera().transform.position, direction, out hitInfo, 5f, playerLayerMask);
                if (!flag)
                {
                    VRC_MirrorReflection component = hitInfo.transform.GetComponent<VRC_MirrorReflection>();
                    flag = component == null;
                }
                if (flag)
                {
                    Vector3 from = playerInfo.GetAvatar().transform.position + playerInfo.GetAvatar().transform.up - CameraModule.GetActiveCamera().transform.position;
                    float num2 = Vector3.Angle(from, CameraModule.GetActiveCamera().transform.forward);
                    if (num2 < -90f || num2 > 90f)
                    {
                      //  SetAvatarVisibility(playerInfo, state: false);
                        return true;
                    }
                }
            }
            Vector3 end = CameraModule.GetActiveCamera().transform.position + -CameraModule.GetActiveCamera().transform.right / 4f;
            RaycastHit hitInfo2;
            bool flag2 = Physics.Linecast(playerInfo.GetAvatar().transform.position + Vector3.up, end, out hitInfo2, playerLayerMask);
            if (flag2)
            {
                flag2 = hitInfo2.transform.name.Contains("mirror");
            }
            Vector3 end2 = CameraModule.GetActiveCamera().transform.position + CameraModule.GetActiveCamera().transform.right / 4f;
            RaycastHit hitInfo3;
            bool flag3 = Physics.Linecast(playerInfo.GetAvatar().transform.position + Vector3.up, end2, out hitInfo3, playerLayerMask);
            if (flag3)
            {
                flag3 = hitInfo3.transform.name.Contains("mirror");
            }
            Vector3 end3 = CameraModule.GetActiveCamera().transform.position + CameraModule.GetActiveCamera().transform.up / 4f;
            RaycastHit hitInfo4;
            bool flag4 = Physics.Linecast(playerInfo.GetAvatar().transform.position + Vector3.up, end3, out hitInfo4, playerLayerMask);
            if (flag4)
            {
                flag4 = hitInfo4.transform.name.Contains("mirror");
            }
            if (flag2 && flag3 && flag4)
            {
              //  SetAvatarVisibility(playerInfo, state: false);
                return true;
            }
          //  SetAvatarVisibility(playerInfo, state: true);
            return false;
        }


     

  

        private static void UpdatePlayerNameplate(PlayerInformation playerInfo, bool isOffscreen) //broken
        {
             
           /* if (Configuration.GetGeneralConfig().NameplateRankColor)
              {
                  Color color = PlayerUtils.playerColorCache[playerInfo.displayName];
                  Color color2 = new Color(color.r, color.g, color.b, 0.5f);
                  playerInfo.vrcPlayer.field_Public_PlayerNameplate_0.field_Public_TextMeshProUGUI_0.color = color;
                  playerInfo.vrcPlayer.field_Public_PlayerNameplate_0.field_Public_TextMeshProUGUI_2.color = color;
                  playerInfo.nameplateBackground.color = color2;
                  playerInfo.nameplateIconBackground.color = color2;
              }
            */
            if (playerInfo.isLocalPlayer)
            {
                return;
            }
         
            int field_Private_Int32_ = playerInfo.vrcPlayer.prop_PlayerNet_0.field_Private_Int16_0;
            float num2 = Time.realtimeSinceStartup - playerInfo.lastNetworkedUpdateTime;
            int ping = playerInfo.GetPing();
            float num3 = 0.5f + 0.011f * (float)PlayerUtils.playerCachingList.Count + Mathf.Min(MathUtils.Clamp(ping, 0, 1000), 500f) / 1000f;
            if (num2 > num3 && playerInfo.lagBarrier < 5)
            {
                playerInfo.lagBarrier++;
            }
            if (playerInfo.lastNetworkedUpdatePacketNumber != field_Private_Int32_)
            {
                playerInfo.lastNetworkedUpdatePacketNumber = field_Private_Int32_;
                playerInfo.lastNetworkedUpdateTime = Time.realtimeSinceStartup;
                playerInfo.lagBarrier--;
            }

            if (isOffscreen || !MainConfig.NameplateMoreInfo)
            {
                return;
            }
            StringBuilder stringBuilder = new StringBuilder();
            if (MainConfig.ShowActorID)
            {
                stringBuilder.Append($"ActorID: <color=red>{playerInfo.actorId}<color=white> | ");
            }
            if (playerInfo.isVRChatStaff)
            {
                stringBuilder.Append("<color=white>[<color=#00FFFF>VRChat Staff<color=white>] | ");
            }
           
            if (playerInfo.blockedLocalPlayer)
            {
                stringBuilder.Append("<color=white>[<color=red>Blocked<color=white>] | ");
            }
            if (playerInfo.isInstanceMaster)
            {
                stringBuilder.Append("<color=white>[<color=blue>Host<color=white>] | ");
            }
            if (!playerInfo.isQuestUser)
            {
                if (playerInfo.isVRUser)
                {
                    stringBuilder.Append("<color=white>[<color=green>VR<color=white>] | ");
                }
                else
                {
                    stringBuilder.Append("<color=white>[<color=green>PC<color=white>] | ");
                }
            }
            else
            {
                stringBuilder.Append("<color=white>[<color=green>Quest<color=white>] | ");
            }
            if (playerInfo.vrcPlayer.prop_VRCAvatarManager_0.field_Private_ApiAvatar_0 != null)
            {
                if (playerInfo.vrcPlayer.prop_VRCAvatarManager_0.field_Private_ApiAvatar_0.releaseStatus == "private")
                {
                    stringBuilder.Append("<color=white>[<color=red>Private<color=white>] | ");
                }
                else
                {
                    stringBuilder.Append("<color=white>[<color=green>Public<color=white>] | ");
                }
            }
            if (MainConfig.DetectLagOrCrash)
            {
                if (num2 > 10f)
                {
                    stringBuilder.Append("<color=white>[<color=red>Crashed<color=white>] | ");
                }
                else if (playerInfo.lagBarrier > 6)
                {
                    stringBuilder.Append("<color=white>[<color=yellow>Lagging<color=white>] | ");
                }
                else
                {
                    stringBuilder.Append("<color=white>[<color=green>Stable<color=white>] | ");
                }
                playerInfo.lagBarrier = MelonUtils.Clamp(playerInfo.lagBarrier, 0, 5);
            }
            if (ping > 200)
            {
                stringBuilder.Append($"Ping: <color=red>{ping}<color=white> | ");
            }
            else if (ping > 130)
            {
                stringBuilder.Append($"Ping: <color=yellow>{ping}<color=white> | ");
            }
            else
            {
                stringBuilder.Append($"Ping: <color=green>{ping}<color=white> | ");
            }

            int fPS = playerInfo.GetFPS();
            if (fPS < 25)
            {
                stringBuilder.Append($"FPS: <color=red>{fPS}<color=white>");
            }
            else if (fPS < 50)
            {
                stringBuilder.Append($"FPS: <color=yellow>{fPS}<color=white>");
            }
            else
            {
                stringBuilder.Append($"FPS: <color=green>{fPS}<color=white>");
            }
        
            playerInfo.customNameplateText.text = stringBuilder.ToString();
        }

     

      
      
    }
}