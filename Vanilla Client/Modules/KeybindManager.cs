// /*
//  *
//  * VanillaClient - KeybindManager.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using UnityEngine;
using Vanilla.Modules.Manager;
using Vanilla.Wrappers;


namespace Vanilla.Modules
{
    internal class KeybindManager : VanillaModule
    {
        protected override string ModuleName => "KeyBindManager";

        public static void Test2(string s)
        {
            Log("Done", "Sucesss");
            Log("Output", s);
        }

        internal static void Test(Action<string> _stringOut, Action _action)
        {
            //  var UIObject = GameObject.Find("UIManager").GetComponent<UIManagerPublicBoObBoAc1BoAcGa1MeUnique>();

            //UIObject.Method_Public_Virtual_Final_New_Void_String_String_InputType_Boolean_String_Action_1_String_Action_String_Boolean_Int32_0("Title", "Something2", TMP_InputField.InputType.Standard, false, "String3", _stringOut, _action);
        }


        internal override void Update()
        {
#if DEBUG
            if (Input.GetKeyDown(KeyCode.L))
            {
                // InputHandler.RunAlertPopup(null, null);

                // LogHandler.LogToHud("Test");
                ModuleManager.DebugKey();

                //  GameObject.Find("").GetComponent<Button>().onClick.FindMethod_Impl.Name.ToString();
            }
#endif
            if (Input.GetKeyDown(KeyCode.O))
            {
                // InputHandler.RunAlertPopup(null, null);

                // LogHandler.LogToHud("Test");
                // ModuleManager.DebugKey();
                //   ModuleManager.WaitForPlayer();

                //  GameObject.Find("").GetComponent<Button>().onClick.FindMethod_Impl.Name.ToString();
            }


            if ((Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F)) ||
                (Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.LeftControl)))
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

            if ((Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E)) ||
                (Input.GetKey(KeyCode.E) && Input.GetKeyDown(KeyCode.LeftControl)))
            {
                CameraModule.ChangeCameraState();
            }
            else if ((Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.P)) ||
                     (Input.GetKey(KeyCode.P) && Input.GetKeyDown(KeyCode.LeftControl)))
            {
                CameraModule.UseFreezeCamera();
            }

            var axis = Input.GetAxis("Mouse ScrollWheel");
            if (axis != 0f)
            {
                CameraModule.ApplyThirdpersonSmoothZoom(axis > 0f);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(1))
                {
                    var ray = new Ray(GeneralWrappers.GetPlayerCamera().transform.position,
                        GeneralWrappers.GetPlayerCamera().transform.forward);
                    if (Physics.Raycast(ray, out var hitInfo))
                    {
                        //   PlayerWrapper.GetLocalPlayerInformation().vrcPlayer.transform.position = hitInfo.point;
                    }
                }

                if (Input.GetMouseButtonDown(2))
                {
                    CameraModule.ApplyCameraSmoothZoom(false, 60f);
                }
                else if (axis != 0f)
                {
                    CameraModule.ApplyCameraSmoothZoom(true, axis * 30f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                CameraModule.ChangeCameraActualZoomState(true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                CameraModule.ChangeCameraActualZoomState(false);
            }
        }
    }
}
