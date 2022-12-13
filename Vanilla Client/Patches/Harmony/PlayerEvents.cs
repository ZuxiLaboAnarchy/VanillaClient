using MelonLoader;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using Vanilla.Config;
using Vanilla.ServerAPI;
using VRC;
using static BestHTTP.JSON.Json;
using static MelonLoader.MelonLogger;

namespace Vanilla.Patches.Harmony
{
    internal class PlayerEvents : VanillaPatches
    {
        protected override string patchName => "PlayerEventPatch";
        internal override void Patch()
        {
            //   var instance = new HarmonyLib.Harmony("StartDONTGETRIDOFTag");
            InitializeLocalPatchHandler(typeof(PlayerEvents));



        //    PatchMethod(typeof(Player).GetMethod(nameof(VRC.Player.Awake)), GetLocalPatch("OnAvatarChanged"), null); // Post So It Exists.

            PatchMethod(typeof(NetworkManager).GetMethod(nameof(NetworkManager.Method_Public_Void_Player_1)), GetLocalPatch("PlayerLeave"), null);

            PatchMethod(typeof(NetworkManager).GetMethod(nameof(NetworkManager.Method_Public_Void_Player_0)), GetLocalPatch("PlayerJoin"), null);


            // instance.Patch(typeof(MonoBehaviourPublicAPOb_v_pObBo_UBoVRObUnique).GetMethod(nameof(MonoBehaviourPublicAPOb_v_pObBo_UBoVRObUnique.OnDestroy), AccessTools.all), new HarmonyMethod(typeof(PlayerEvents).GetMethod(nameof(Player_OnDestroyM), BindingFlags.NonPublic | BindingFlags.Static)));



            // PatchMethod(AccessTools.Method(typeof(MonoBehaviourPrivateAc1AcOb2AcInStHa2Unique), nameof(.player     .Method_Public_Void_Player_1), GetLocalPatch("PlayerJoin")));

            // PatchMethod(AccessTools.Method(typeof(MonoBehaviourPrivateAc1AcOb2AcInStHa2Unique)), nameof(MonoBehaviourPublicAPOb_v_pObBo_UBoVRObUnique.Awake), GetLocalPatch("OnAvatarChanged"));

            // Nebula.Patch(AccessTools.Method(typeof(NetworkManager), nameof(NetworkManager.Method_Public_Void_Player_1)), GetPatch(nameof(playev)));

            //Nebula.Patch(AccessTools.Method(typeof(NetworkManager), nameof(NetworkManager.Method_Public_Void_Player_0)), GetPatch(nameof(playevleave)));
        }


        private static bool PlayerJoin(Player __0)
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

                { Log("Player Join", $"{user} Joined at {DateTime.Now} UserID {UID}"); }
                //   HudNotify.Msg($"{user} Joined", 4f);



                return true;
            }
            catch (Exception e)
            {
                MelonLogger.Error("Player Join Patch Fail");
                MelonLogger.Error(e);
                return true;
            }
        }



        private static bool PlayerLeave(Player __0)
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
                Log("PlayerLeft", $"----------------------{user}----------------------");
                Log("PlayerLeft", $"Left at {DateTime.Now}");
                Log("PlayerLeft", $"User ID [{UID}]");
                Log("PlayerLeft", $"----------------------{user}----------------------");

                return true;
            }
            catch (Exception e)
            {
                Log("PlayerLeave", "Player Left Patch Fail", ConsoleColor.Red);
                ExceptionHandler("PlayerLeave", e);
                return true;
            }
        }




        /*
        private static bool OnAvatarChanged(VRCPlayer __instance)
        {




            // VRCPlayer.MulticastDelegateNPublicSealedVoUnique.   Method_Public_add_Void_OnAvatarIsReady_0
            if (__instance == null) return true;
            //__instance.Method_Public_add_Void_MulticastDelegateNPublicSealedVoUnique_0(new Action(() =>
            // __instance.Method_Public_add_Void_Action_0(new Action(() =>
            //  {
            //
            __instance.Method_Public_add_Void_Action_0(new Action(() =>
            {
                if (__instance._player != null && __instance._player.field_Private_APIUser_0 != null && __instance.field_Private_ApiAvatar_0 != null)
                {
                    Dev("Avatar", "AvatarSwitched");
                    try
                    {
                        var a = __instance.field_Private_ApiAvatar_0;

                        var senda = new AvatarLog()
                        {
                            AvatarName = a.name,

                            Author = a.authorName,

                            Authorid = a.authorId,

                            Avatarid = a.id,

                            Description = a.description,

                            Asseturl = a.assetUrl,

                            Image = a.imageUrl,

                            Platform = a.platform,

                            Status = a.releaseStatus,


                          
                            code = "9",

                        };
                        WSBase.sendmsg($"{JsonConvert.SerializeObject(senda)}");
                    }
                    catch { }

                }
            }));
            return true;
        }


       */


        private static void VRCPlayer_Awake(VRCPlayer __instance)
        {
            MelonCoroutines.Start(RunMe());

            IEnumerator RunMe()
            {
                while (__instance?.gameObject?.GetComponent<Player>()?.field_Private_APIUser_0 == null) // Wait For APIUser To Exist, Tempermental If Not Done.
                    yield return new WaitForEndOfFrame();

                //OnPlayerJoin(__instance.gameObject.GetComponent<Player>());

                yield break;
            }
        }

        //  internal static void OnPlayerJoin(Player __0)
        // {
        //  __0.field_Private_ApiAvatar_0
        //}


    }


}

