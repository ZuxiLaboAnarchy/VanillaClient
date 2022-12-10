using HarmonyLib;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;
using static MelonLoader.MelonLogger;
using static VRC.SDKBase.VRC_SpecialLayer;

namespace VanillaClient.Patches.Harmony
{
    internal class PlayerEvents : VanillaPatches
    {
        protected override string patchName => "PlayerEventPatch";
        internal override void Patch()
        {
         //   var instance = new HarmonyLib.Harmony("StartDONTGETRIDOFTag");
            InitializeLocalPatchHandler(typeof(PlayerEvents));



           PatchMethod(typeof(VRC_Player).GetMethod(nameof(VRC_Player.Awake), AccessTools.all),null , GetLocalPatch("VRCPlayer_Awake")); // Post So It Exists.

          // instance.Patch(typeof(MonoBehaviourPublicAPOb_v_pObBo_UBoVRObUnique).GetMethod(nameof(MonoBehaviourPublicAPOb_v_pObBo_UBoVRObUnique.OnDestroy), AccessTools.all), new HarmonyMethod(typeof(PlayerEvents).GetMethod(nameof(Player_OnDestroyM), BindingFlags.NonPublic | BindingFlags.Static)));

       

            // PatchMethod(AccessTools.Method(typeof(MonoBehaviourPrivateAc1AcOb2AcInStHa2Unique), nameof(.player     .Method_Public_Void_Player_1), GetLocalPatch("PlayerJoin")));

            // PatchMethod(AccessTools.Method(typeof(MonoBehaviourPrivateAc1AcOb2AcInStHa2Unique)), nameof(MonoBehaviourPublicAPOb_v_pObBo_UBoVRObUnique.Awake), GetLocalPatch("OnAvatarChanged"));

            // Nebula.Patch(AccessTools.Method(typeof(NetworkManager), nameof(NetworkManager.Method_Public_Void_Player_1)), GetPatch(nameof(playev)));

            //Nebula.Patch(AccessTools.Method(typeof(NetworkManager), nameof(NetworkManager.Method_Public_Void_Player_0)), GetPatch(nameof(playevleave)));
        }




        /*  private static bool playevleave(VRC.Player __0)
          {




                  try
                  {
                      string user = __0.field_Private_APIUser_0.displayName;
                      string UID = __0.field_Private_APIUser_0.id;
                      bool Quest = __0.field_Private_APIUser_0.IsOnMobile;

                      if (user == "orchestrapyro")
                      { user = "HyperV"; }

                      if (UID == "usr_e49984a4-14de-482d-9899-62d710c7ead8")
                      { UID = "IM HYPERV DONT WORRY ABOUT MY UID LOL"; }

                      Console.ForegroundColor = ConsoleColor.Cyan;
                      Log("PlayerLeft",$"----------------------{user}----------------------");
                      Log("PlayerLeft", $"Left at {DateTime.Now}");
                      Log("PlayerLeft", $"User ID [{UID}]");
                      Log("PlayerLeft", ($"----------------------{user}----------------------");

                      return true;
                  }
                  catch (Exception e)
                  {
                      Log("PlayerLeave", "Player Left Patch Fail", ConsoleColor.Red);
                      ExceptionHandler("PlayerLeave", e);
                      return true;
                  }
              }
          }*/


        private static void VRCPlayer_Awake(VRC_Player __instance)
        {
            MelonCoroutines.Start(RunMe());

            IEnumerator RunMe()
            {
                while (__instance?.gameObject?.GetComponent<Player>()?.field_Private_APIUser_0 == null) // Wait For APIUser To Exist, Tempermental If Not Done.
                    yield return new WaitForEndOfFrame();

                OnPlayerJoin(__instance.gameObject.GetComponent<Player>());

                yield break;
            }
        }

        internal static void OnPlayerJoin(Player __0)
        {

        }


    }

    
}

