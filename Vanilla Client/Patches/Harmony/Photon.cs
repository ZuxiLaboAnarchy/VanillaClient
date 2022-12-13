
using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using ExitGames.Client.Photon;
using HarmonyLib;
using Il2CppSystem.Net;
using MelonLoader;
using Photon.Realtime;
using UnityEngine;
using Vanilla.Helpers;
using VRC.Core;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Vanilla.Patches.Harmony
{
    internal class PhotonPatch : VanillaPatches
    {
        protected override string patchName => "PhotonPatch";

        internal override void Patch()
        {
            try
            {
                InitializeLocalPatchHandler(typeof(PhotonPatch));

                PatchMethod(typeof(LoadBalancingClient).GetMethod("OnEvent"), GetLocalPatch("OnEvent"), null);

               

                //PatchMethod(typeof().GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), GetLocalPatch("PhotonRaiseEventPatch"), null);



            }
            catch (Exception e)
            {
                Utils.LogHandler.ExceptionHandler(patchName, e);
            }
        }



        private static bool OnEvent(EventData __0)
        {
            var eventCode = __0.Code;
            switch (eventCode)
            {
                case 42:
                    return WSHelper.AvatarLogHandler();
                case 223:
                    return WSHelper.AvatarLogHandler();
                default:
                    return true;
            }
        }

      

    }
}
