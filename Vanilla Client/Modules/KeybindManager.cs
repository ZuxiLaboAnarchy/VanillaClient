using System.Diagnostics;
using System.IO;
using UnityEngine;
using Vanilla.Config;
using Vanilla.Wrappers;
using static BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests.SkeinEngine;

namespace Vanilla.Modules
{
    internal class KeybindManager : VanillaModule
    {

        internal override void Update()
        {

            if (UnityEngine.Input.GetKeyDown(KeyCode.L))
            {
               // Buttons.Loader.LoadButtons();

                // UniversalUI.SetUIActive("VanillaClient", IsGUIActive());
            }


            if (UnityEngine.Input.GetKeyDown(KeyCode.L))
            {
                LogHandler.RePop();
                MainConfig.Load();
            }


            

            if (UnityEngine.Input.GetKeyDown(KeyCode.RightControl))
            {
                GeneralUtils.CloseGame();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.RightAlt))
            {
                GeneralUtils.Restart();
            }

            if ((UnityEngine.Input.GetKey(KeyCode.LeftControl) && UnityEngine.Input.GetKeyDown(KeyCode.E) || (UnityEngine.Input.GetKey(KeyCode.E) && UnityEngine.Input.GetKeyDown(KeyCode.LeftControl))))
            {
                CameraModule.ChangeCameraState();
            }
            else if ((UnityEngine.Input.GetKey(KeyCode.LeftControl) && UnityEngine.Input.GetKeyDown(KeyCode.P)) || (UnityEngine.Input.GetKey(KeyCode.P) && UnityEngine.Input.GetKeyDown(KeyCode.LeftControl)))
            {
                CameraModule.UseFreezeCamera();
            }
            float axis = UnityEngine.Input.GetAxis("Mouse ScrollWheel");
            if (axis != 0f)
            {
                CameraModule.ApplyThirdpersonSmoothZoom(axis > 0f);
            }
            if (UnityEngine.Input.GetKey(KeyCode.LeftControl))
            {
                if (UnityEngine.Input.GetKey(KeyCode.LeftControl) && UnityEngine.Input.GetMouseButtonDown(1))
                {
                    Ray ray = new Ray(GeneralWrappers.GetPlayerCamera().transform.position, GeneralWrappers.GetPlayerCamera().transform.forward);
                    if (Physics.Raycast(ray, out var hitInfo))
                    {
                     //   PlayerWrapper.GetLocalPlayerInformation().vrcPlayer.transform.position = hitInfo.point;
                    }
                }
                if (UnityEngine.Input.GetMouseButtonDown(2))
                {
                    CameraModule.ApplyCameraSmoothZoom(incremental: false, 60f);
                }
                else if (axis != 0f)
                {
                    CameraModule.ApplyCameraSmoothZoom(incremental: true, axis * 30f);
                }
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.LeftAlt))
            {
                CameraModule.ChangeCameraActualZoomState(zoom: true);
            }
            else if (UnityEngine.Input.GetKeyUp(KeyCode.LeftAlt))
            {
                CameraModule.ChangeCameraActualZoomState(zoom: false);
            }
        }
    }
}
    
