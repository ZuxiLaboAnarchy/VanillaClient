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

            if ((Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.O) || (Input.GetKey(KeyCode.O) && Input.GetKeyDown(KeyCode.LeftControl))))
            {
                CameraModule.ChangeCameraState();
            }
            else if ((Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.P)) || (Input.GetKey(KeyCode.P) && Input.GetKeyDown(KeyCode.LeftControl)))
            {
                CameraModule.UseFreezeCamera();
            }
            float axis = Input.GetAxis("Mouse ScrollWheel");
            if (axis != 0f)
            {
                CameraModule.ApplyThirdpersonSmoothZoom(axis > 0f);
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(1))
                {
                    Ray ray = new Ray(GeneralWrappers.GetPlayerCamera().transform.position, GeneralWrappers.GetPlayerCamera().transform.forward);
                    if (Physics.Raycast(ray, out var hitInfo))
                    {
                     //   PlayerWrapper.GetLocalPlayerInformation().vrcPlayer.transform.position = hitInfo.point;
                    }
                }
                if (Input.GetMouseButtonDown(2))
                {
                    CameraModule.ApplyCameraSmoothZoom(incremental: false, 60f);
                }
                else if (axis != 0f)
                {
                    CameraModule.ApplyCameraSmoothZoom(incremental: true, axis * 30f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                CameraModule.ChangeCameraActualZoomState(zoom: true);
            }
            else if (Input.GetKeyUp(KeyCode.U))
            {
                CameraModule.ChangeCameraActualZoomState(zoom: false);
            }
        }
    }
}
    
