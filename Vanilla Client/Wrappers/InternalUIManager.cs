// /*
//  *
//  * VanillaClient - InternalUIManager.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;

namespace Vanilla.Wrappers
{
    internal class InternalUIManager
    {
        // static MonoBehaviour1PublicTe_p_dTeKe_kBo_mStBuUnique KeyBoardMonoBehav = null;

        internal static void RunKeyBoardPopup(string Title, string TextBox, string OKButton,
            Action<string> RealTimeString, Action<string> EndString, Action OnClose, bool multiLine2 = true,
            int CharLimit = 0)
        {
            CreateKeyBoardPopup(Title, EndString, OnClose);
        }
        // TODO: Get Old Popup
        /* internal static void RunAlertPopup(string Header, string Body, string button1, string button2, Il2CppSystem.Action button1Action, Il2CppSystem.Action button2Action)
         {
             UIManagerPublicBoObBoAc1BoAcGa1MeUnique UIManager = GameObject.Find("UIManager").GetComponent<UIManagerPublicBoObBoAc1BoAcGa1MeUnique>();

             UIManager.Method_Public_Virtual_Final_New_Void_String_String_Boolean_String_Action_String_Action_1(Header, Body, true, button1, button1Action, button2, button2Action);
         }*/

        public static string CreateKeyBoardPopup(string name, Action<string> setOutput, Action action)
        {
            var returned = "";
            VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0
                .Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_PDM_0(
                    name, "", InputField.InputType.Standard, false, "Enter",
                    DelegateSupport
                        .ConvertDelegate<Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>,
                            Text>>
                        (new Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text>
                        (delegate (string s, Il2CppSystem.Collections.Generic.List<KeyCode> k, Text t)
                        {
                            setOutput(s);
                            if (action != null)
                            {
                                action.Invoke();
                            }

                            ;
                        })), null, "", true, null);
            return returned;
        }

        internal static void KAction1(string a)
        {
        }

        internal static void KAction2(string a)
        {
            Log("TestKeyboard Action 2 ", a);
        }

        internal static Il2CppSystem.Action KAction3()
        {
            Log("Test Action 3 ", "I Was Invoked");
            return null;
        }

        internal static Il2CppSystem.Action KAction4()
        {
            Log("Test Action 4 ", "I Was Invoked");
            return null;
        }
    }
}
