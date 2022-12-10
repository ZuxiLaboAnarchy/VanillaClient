using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;

namespace VanillaClient.Patches.Harmony
{
    internal class PlayerEvents : VanillaPatches
    {
        public override void Patch()
        {

            InitializeLocalPatchHandler(typeof(PlayerEvents));
            
            PatchMethod(AccessTools.Method(typeof(NetworkManager), nameof(NetworkManager.Method_Public_Void_Player_1), GetLocalPatch("PlayerJoin")));
         
          // Nebula.Patch(AccessTools.Method(typeof(NetworkManager), nameof(NetworkManager.Method_Public_Void_Player_1)), GetPatch(nameof(playev)));

            //Nebula.Patch(AccessTools.Method(typeof(NetworkManager), nameof(NetworkManager.Method_Public_Void_Player_0)), GetPatch(nameof(playevleave)));
        }


        private static MethodInfo _OnPhotonPlayerJoinMethod;

        internal static MethodInfo OnPhotonPlayerJoinMethod
        {
            get
            {
                if (_OnPhotonPlayerJoinMethod != null)
                {
                    return _OnPhotonPlayerJoinMethod;
                }
                return _OnPhotonPlayerJoinMethod = typeof(NetworkManager).GetMethods().Single(delegate (MethodInfo it)
                {
                    if (it.ReturnType == typeof(void) && it.GetParameters().Length == 1 && it.GetParameters()[0].ParameterType == typeof(Photon.Realtime.Player))
                    {
                        return XrefScanner.XrefScan(it).Any(jt =>
                        {
                            if (jt.Type == XrefType.Global)
                            {
                                Il2CppSystem.Object @object = jt.ReadAsObject();
                                if (@object != null)
                                {
                                    if (@object.ToString().Contains("Enter"))
                                    {
                                        //Logs.Log($"[XREF] Found [JOIN] Method! [{it.Name} with {@object.ToString()}]");
                                        _OnPhotonPlayerJoinMethod = it;
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
    }
}

