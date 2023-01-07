using UnityEngine;
using Vanilla.Config;
using Vanilla.Wrappers;


namespace Vanilla.Modules
{
    internal class KeybindManager : VanillaModule
    {
        protected override string ModuleName => "KeyBindManager";

        internal override void Update()
        {
#if DEBUG
            if (UnityEngine.Input.GetKeyDown(KeyCode.L))
            {

               // LogHandler.LogToHud("Test");
                ModuleManager.DebugKey();




            }
#endif




            if ((Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F) || (Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.LeftControl))))
            {
                FlyManager.ToggleFly();
            }

            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                GeneralUtils.CloseGame();
            }

            if (Input.GetKeyDown(KeyCode.RightAlt))
            {
                GeneralUtils.Restart();
            }

            if ((Input.GetKey(KeyCode.LeftControl) && UnityEngine.Input.GetKeyDown(KeyCode.E) || (UnityEngine.Input.GetKey(KeyCode.E) && UnityEngine.Input.GetKeyDown(KeyCode.LeftControl))))
            {
                CameraModule.ChangeCameraState();
            }
            else if ((Input.GetKey(KeyCode.LeftControl) && UnityEngine.Input.GetKeyDown(KeyCode.P)) || (UnityEngine.Input.GetKey(KeyCode.P) && UnityEngine.Input.GetKeyDown(KeyCode.LeftControl)))
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
                if (Input.GetKey(KeyCode.LeftControl) && UnityEngine.Input.GetMouseButtonDown(1))
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
            else if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                CameraModule.ChangeCameraActualZoomState(zoom: true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                CameraModule.ChangeCameraActualZoomState(zoom: false);
            }
        }
    }
}

