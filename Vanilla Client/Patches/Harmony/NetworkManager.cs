﻿// /*
//  *
//  * VanillaClient - NetworkManager.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using MelonLoader;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using Vanilla.Config;
using Vanilla.Modules.Manager;
using Vanilla.Patches.Manager;
using Vanilla.Wrappers;
using VRC;
using VRC.SDKBase;


namespace Vanilla.Patches.Harmony
{
    [Obfuscation(Feature = "-flow")]
    [Obfuscation(Feature = "-strenc")]
    [Obfuscation(Feature = "-virtualization")]
    [Obfuscation(Feature = "-rename")]
    internal class NetworkManagerPatch : VanillaPatches
    {
        protected override string patchName => "PlayerEventPatch";

        internal override void Patch()
        {
            //   var instance = new HarmonyLib.Harmony("StartDONTGETRIDOFTag");
            InitializeLocalPatchHandler(typeof(NetworkManagerPatch));


            //    PatchMethod(typeof(Player).GetMethod(nameof(VRC.Player.Awake)), GetLocalPatch("OnAvatarChanged"), null); // Post So It Exists.


            PatchMethod(typeof(NetworkManager).GetMethod(nameof(NetworkManager.Method_Public_Void_Player_1)),
                GetLocalPatch(Strings.PlayerJoin), null);
            PatchMethod(typeof(NetworkManager).GetMethod(nameof(NetworkManager.Method_Public_Void_Player_0)),
                GetLocalPatch(Strings.PlayerLeave), null);
            PatchMethod(typeof(NetworkManager).GetMethod(nameof(NetworkManager.OnJoinedRoom)), null,
                GetLocalPatch(Strings
                    .OnJoinedRoomPatch)); //fix for the peeps Method_Internal_Void_PDM_0 wont wor                                                                                     //PatchMethod(typeof(NetworkManager).GetMethod("OnJoinedRoom"), null, GetLocalPatch("OnJoinedRoomPatch")); //broken and replaced
            PatchMethod(typeof(NetworkManager).GetMethod(nameof(NetworkManager.OnLeftRoom)), null,
                GetLocalPatch(Strings.OnLeftRoomPatch)); //works

            /* MethodInfo[] methods = typeof(VRCPlayer).GetMethods().Where(mb => mb.Name.StartsWith("Method_Private_Void_GameObject_VRC_AvatarDescriptor_Boolean_")).ToArray();
             foreach (MethodInfo method in methods)
             {
                 PatchMethod(typeof(VRCPlayer).GetMethod(method.Name), GetLocalPatch(nameof(AntiCrashPatch)), null);
             }
            */

            //if (PlayerEvents.OnPlayerJoinedMethod != null)
            //  PatchMethod(PlayerEvents._OnPlayerJoinedMethod, null, GetLocalPatch("PlayerJoin"));

            //  if (PlayerEvents.OnPlayerLeftMethod != null)
            //    PatchMethod(PlayerEvents._OnPlayerLeftMethod, null, GetLocalPatch("PlayerLeave"));


            // instance.Patch(typeof(MonoBehaviourPublicAPOb_v_pObBo_UBoVRObUnique).GetMethod(nameof(MonoBehaviourPublicAPOb_v_pObBo_UBoVRObUnique.OnDestroy), AccessTools.all), new HarmonyMethod(typeof(PlayerEvents).GetMethod(nameof(Player_OnDestroyM), BindingFlags.NonPublic | BindingFlags.Static)));


            // PatchMethod(AccessTools.Method(typeof(MonoBehaviourPrivateAc1AcOb2AcInStHa2Unique), nameof(.player     .Method_Public_Void_Player_1), GetLocalPatch("PlayerJoin")));

            // PatchMethod(AccessTools.Method(typeof(MonoBehaviourPrivateAc1AcOb2AcInStHa2Unique)), nameof(MonoBehaviourPublicAPOb_v_pObBo_UBoVRObUnique.Awake), GetLocalPatch("OnAvatarChanged"));

            // Nebula.Patch(AccessTools.Method(typeof(NetworkManager), nameof(NetworkManager.Method_Public_Void_Player_1)), GetPatch(nameof(playev)));

            //Nebula.Patch(AccessTools.Method(typeof(NetworkManager), nameof(NetworkManager.Method_Public_Void_Player_0)), GetPatch(nameof(playevleave)));
        }

        //   private static bool AntiCrashPatch(VRCPlayer __instance, GameObject __0, VRC_AvatarDescriptor __1, Boolean __2)
        // {
        //       return Anticrash.ProcessAvatar(__0, __instance);
        // }
        private static void OnJoinedRoomPatch()
        {
            RuntimeConfig.isConnectedToInstance = true;
        }

        private static void OnLeftRoomPatch()
        {
            RuntimeConfig.isConnectedToInstance = false;
        }


        private static bool PlayerJoin(Player __0)
        {
            try
            {
                Networking.RPC(RPC.Destination.Others, PlayerWrapper.GetLocalPlayer().gameObject,
                    "Zuxi_Networked_Join_VanillaClient", new Il2CppSystem.Object[0]);
                ModuleManager.PlayerJoin(__0);
                // TODO: move this to somewhere else
            }
            catch (Exception e)
            {
                ExceptionHandler("PJP", e);
            }

            return true;
        }

        private static bool PlayerLeave(Player __0)
        {
            try
            {
                ModuleManager.PlayerLeave(__0);
            }
            catch (Exception e)
            {
                ExceptionHandler("PJP", e);
            }

            return true;
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
                while (__instance?.gameObject?.GetComponent<Player>()?.field_Private_APIUser_0 ==
                       null) // Wait For APIUser To Exist, Tempermental If Not Done.
                {
                    yield return new WaitForEndOfFrame();
                }

                //OnPlayerJoin(__instance.gameObject.GetComponent<Player>());

                yield break;
            }
        }

        //  internal static void OnPlayerJoin(Player __0)
        // {
        //  __0.field_Private_ApiAvatar_0
        //}
        private static MethodInfo _OnPlayerJoinedMethod;
        private static MethodInfo _OnPlayerLeftMethod;

        internal static MethodInfo OnPlayerJoinedMethod
        {
            get
            {
                if (_OnPlayerJoinedMethod != null)
                {
                    return _OnPlayerJoinedMethod;
                }

                return _OnPlayerJoinedMethod = typeof(NetworkManager).GetMethods().Single(delegate (MethodInfo it)
                {
                    if (it.ReturnType == typeof(void) && it.GetParameters().Length == 1 &&
                        it.GetParameters()[0].ParameterType == typeof(Player))
                    {
                        return XrefScanner.XrefScan(it).Any(jt =>
                        {
                            if (jt.Type == XrefType.Global
                               )
                            {
                                var @object = jt.ReadAsObject();
                                if (@object != null)
                                {
                                    if (@object.ToString().Contains("OnPlayerJoined"))
                                    {
                                        _OnPlayerJoinedMethod = it;
                                        return true;
                                    }
                                }

                                return false;
                            }

                            return false;
                        });
                    }

                    return false;
                });
            }
        }

        internal static MethodInfo OnPlayerLeftMethod
        {
            get
            {
                if (_OnPlayerLeftMethod != null)
                {
                    return _OnPlayerLeftMethod;
                }

                return _OnPlayerLeftMethod = typeof(NetworkManager).GetMethods().Single(delegate (MethodInfo it)
                {
                    if (it.ReturnType == typeof(void) && it.GetParameters().Length == 1 &&
                        it.GetParameters()[0].ParameterType == typeof(Player))
                    {
                        return XrefScanner.XrefScan(it).Any(jt =>
                        {
                            if (jt.Type == XrefType.Global
                               )
                            {
                                var @object = jt.ReadAsObject();
                                if (@object != null)
                                {
                                    if (@object.ToString().Contains("OnPlayerLeft"))
                                    {
                                        _OnPlayerLeftMethod = it;
                                        return true;
                                    }
                                }

                                return false;
                            }

                            return false;
                        });
                    }

                    return false;
                });
            }
        }
    }
}
