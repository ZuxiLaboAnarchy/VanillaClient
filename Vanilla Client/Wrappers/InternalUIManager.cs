using TMPro;
using UnityEngine;
using VRC.DataModel;

namespace Vanilla.Utils
{
    internal class InternalUIManager
    {
        static MonoBehaviour1PublicTe_p_dTeKe_kBo_mStBuUnique KeyBoardMonoBehav = null;

        internal static void RunKeyBoardPopup(string Title, string TextBox, string OKButton, Action<string> RealTimeString, Action<string> EndString, Action OnClose, bool multiLine2 = true, int CharLimit = 0)
        {
            try
            {
                KeyboardData KeyBoardData = new KeyboardData();

                KeyBoardMonoBehav = GameObject.Find("VanillaObject").GetComponent<MonoBehaviour1PublicTe_p_dTeKe_kBo_mStBuUnique>();

                KeyboardData KeyBoardData2 = KeyBoardData.Method_Public_KeyboardData_String_String_String_String_String_PDM_0(Title, TextBox, "3", OKButton);

                KeyboardData keyboardData3 = KeyBoardData2.Method_Public_KeyboardData_Action_1_String_Action_1_String_Action_Boolean_PDM_1(RealTimeString, EndString, OnClose, true);

                KeyboardData keyboardData4 = keyboardData3.Method_Public_KeyboardData_EnumPublicSealedvaStNuSe4vUnique_Boolean_PDM_0(EnumPublicSealedvaStNuSe4vUnique.Standard, true);

                KeyboardData keyboardData5 = keyboardData4.Method_Public_KeyboardData_InputType_ContentType_Int32_Boolean_Boolean_InterfacePublicAbstractBoStVoAc1VoAcSt1BoUnique_PDM_0(TMP_InputField.InputType.Standard, TMP_InputField.ContentType.Standard, CharLimit, multiLine2, false, null);

                keyboardData5._isWorldKeyboard = true;

                KeyBoardMonoBehav._keyboardData = keyboardData5;

                KeyBoardMonoBehav.Method_Private_Void_1();
            }
            catch (Exception) { }
        }

        internal static void RunAlertPopup(string Header, string Body, string button1, string button2, Il2CppSystem.Action button1Action, Il2CppSystem.Action button2Action)
        {
            UIManagerPublicBoObBoAc1BoAcGa1MeUnique UIManager = GameObject.Find("UIManager").GetComponent<UIManagerPublicBoObBoAc1BoAcGa1MeUnique>();

            UIManager.Method_Public_Virtual_Final_New_Void_String_String_Boolean_String_Action_String_Action_1(Header, Body, true, button1, button1Action, button2, button2Action);
        }

        internal static void KAction1(string a)
        {

        }

        internal static void KAction2(string a)
        {



            LogHandler.Log("TestKeyboard Action 2 ", a);
        }

        internal static Il2CppSystem.Action KAction3()
        {
            LogHandler.Log("Test Action 3 ", "I Was Invoked");
            return null;
        }

        internal static Il2CppSystem.Action KAction4()
        {
            LogHandler.Log("Test Action 4 ", "I Was Invoked");
            return null;
        }
    }
}