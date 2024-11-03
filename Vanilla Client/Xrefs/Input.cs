using System.IO;
using System.Linq;
using System.Reflection;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using VRC.DataModel;

namespace Vanilla.Xrefs
{
    internal class Input
    {
        private static MethodInfo _NumbKeyboard { get; set; }
        private static MethodInfo _Toggle { get; set; }
        private static MethodInfo _Warning { get; set; }
        private static MethodInfo _InputPopout { get; set; }


#pragma warning disable CS0162 // Unreachable code detected


        //        private static StreamWriter WritingStream = new StreamWriter(FileHelper.GetCheatFolder() + "Scan.erp");


        internal static void GetMethods()
        {
            /*
            MethodInfo[] methods = typeof(UIManagerPublicBoObBoAc1BoAcGa1MeUnique).GetMethods();

            //  MethodInfo[] methods = typeof(VRCUiPopupManager).GetMethods();
            try
            {
                for (int i = 0; i < methods.Length; i++)
                {
                    var xrefedmethods = UnhollowerRuntimeLib.XrefScans.XrefScanner.XrefScan(methods[i]).ToArray();
                    for (int i2 = 0; i2 < xrefedmethods.Length; i2++)
                    {
                        if (xrefedmethods[i2].Type != UnhollowerRuntimeLib.XrefScans.XrefType.Global) continue;
                        switch (true)
                        {

                            case true when xrefedmethods[i2].ReadAsObject().ToString() == "Container/MMParent/Modal_MM_Keyboard":
                                _Warning = methods[i];
                                Log("XrefScanner", "Found Mehod for _Keyboard " + _Warning.Name);

                                break;


                            case true when xrefedmethods[i2].ReadAsObject().ToString().Contains("Modal_MM_Keyboard"):

                                _InputPopout = methods[i];

                                Log("XrefScanner", "Found Mehod for _Modal_MM_Keyboard => " + _InputPopout.Name, ConsoleColor.Yellow);
                                break;

                        }
                    }


                }
            }
            catch { }
            */
        }


        internal static void SetMethods1()
        {
            //   VRCUiPopup.Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_Int32_0
            //  UIElement1PublicOb_mGr_gMoCa_m_pStCaUnique

            //  VRCUiPopupInput

            //MainMenuStationController.


            var methods = typeof(VRCAnimationLayer).GetMethods();
            return;
            //  MethodInfo[] methods = typeof(VRCUiPopupManager).GetMethods();
            try
            {
                for (var i = 0; i < methods.Length; i++)
                {
                    var xrefedmethods = UnhollowerRuntimeLib.XrefScans.XrefScanner.XrefScan(methods[i]).ToArray();
                    for (var i2 = 0; i2 < xrefedmethods.Length; i2++)
                    {
                        if (xrefedmethods[i2].Type != UnhollowerRuntimeLib.XrefScans.XrefType.Global)
                        {
                            continue;
                        }

                        switch (true)
                        {
                            case true when xrefedmethods[i2].ReadAsObject().ToString() ==
                                           "Container/MMParent/Modal_MM_Keyboard":
                                _Warning = methods[i];
                                Log("XrefScanner", "Found Mehod for _Warning" + _Warning.Name);

                                break;


                            case true when xrefedmethods[i2].ReadAsObject().ToString() ==
                                           "MenuContent/Popups/AlertPopup":
                                _Warning = methods[i];
                                Log("XrefScanner", "Found Mehod for _Warning" + _Warning.Name);

                                break;
                            case true when xrefedmethods[i2].ReadAsObject().ToString() ==
                                           "MenuContent/Popups/InputKeypadPopup":
                                _NumbKeyboard = methods[i];
                                Log("XrefScanner", "Found Mehod for _Warning" + _NumbKeyboard.Name);
                                break;
                            case true when xrefedmethods[i2].ReadAsObject().ToString() ==
                                           "MenuContent/Popups/StandardPopupV2":
                                _Toggle = methods[i];
                                Log("XrefScanner", "Found Mehod for _Toggle" + _Toggle.Name);
                                break;
                            case true when xrefedmethods[i2].ReadAsObject().ToString() ==
                                           "MenuContent/Popups/InputPopup":
                                _InputPopout = methods[i];
                                Log("XrefScanner", "Found Mehod for _InputPopout" + _InputPopout.Name);
                                break;
                            case true when xrefedmethods[i2].ReadAsObject().ToString().Contains("Modal_MM_Keyboard"):

                                _InputPopout = methods[i];

                                Log("XrefScanner", "Found Mehod for _Modal_MM_Keyboard => " + _InputPopout.Name,
                                    ConsoleColor.Yellow);
                                break;
                        }
                    }
                }
            }
            catch
            {
            }
        }

#pragma warning restore CS0162 // Unreachable code detected


        internal static void PopOutWarrningMessage(string _message, string description = "", float _Time = 10)
        {
            _Warning.Invoke(VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0,
                new object[] { _message, description, _Time });
        }

        internal static void PopOutToggle(string _title, string _desciption, Action _ok = null, Action _cancel = null)
        {
            _Toggle.Invoke(VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0, new object[]
            {
                _title, _desciption, "Cancel", (Il2CppSystem.Action)new Action(() =>
                {
                    _cancel.Invoke();
                    VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Private_Void_0();
                }),
                "Accept", (Il2CppSystem.Action)new Action(() =>
                {
                    _ok.Invoke();
                    VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Private_Void_0();
                }),
                null
            });
        }

        internal static void PopOutNumbersKeyboard(string _title, Action<int> _intOut, Action _action)
        {
            _NumbKeyboard.Invoke(VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0, new object[]
            {
                _title, "", InputField.InputType.Standard, true, "Enter", DelegateSupport
                    .ConvertDelegate<Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text>>
                    (new Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text>
                    (delegate(string s, Il2CppSystem.Collections.Generic.List<KeyCode> k, Text t)
                    {
                        _intOut(int.Parse(s));
                        _action.Invoke();
                    })),
                null, "Enter A text", true, null, false, 0
            });
        }

        internal static void PopOutInput(string _title, Action<string> _stringOut, Action _action)
        {
            _InputPopout.Invoke(VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0, new object[]
            {
                _title, "", InputField.InputType.Standard, false, "Enter",
                DelegateSupport
                    .ConvertDelegate<Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text>>
                    (new Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text>
                    (delegate(string s, Il2CppSystem.Collections.Generic.List<KeyCode> k, Text t)
                    {
                        _stringOut(s);
                        _action.Invoke();
                    })),
                null, "Enter text....", true, null, false, 0
            });
        }

        public static void CloseKeyboard()
        {
            VRCUiManager.prop_VRCUiManager_0.Method_Public_Void_Boolean_Boolean_1(true, false);
        }

        public static Il2CppSystem.Action CloseKeyboardAction =>
            DelegateSupport.ConvertDelegate<Il2CppSystem.Action>(new Action(() => { CloseKeyboard(); }));
        /*
                public static void OpenKeyboard(string title, string text, Action<string, List<KeyCode>, Text> action)
                {
                    Il2CppSystem.Action<string, List<KeyCode>, Text> newAction = DelegateSupport.ConvertDelegate<Il2CppSystem.Action<string, List<KeyCode>, Text>>(action);
                    VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_Int32_0(title, null, TMPro.TMP_InputField.InputType.Standard, false, "Okay", newAction, CloseKeyboardAction, text);

                }
            }*/
    }
}