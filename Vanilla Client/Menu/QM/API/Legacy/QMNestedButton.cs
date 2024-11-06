// /*
//  *
//  * VanillaClient - QMNestedButton.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

namespace Vanilla.Menu.QM.API.Legacy
{
    /*
    internal class QMNestedButton : QMMenuBase
    {
        protected bool IsMenuRoot;
        protected GameObject BackButton;
        protected QMSingleButton MainButton;

        internal QMNestedButton(QMTabMenu location, float posX, float posY, string btnText, string toolTipText, string menuTitle, bool halfButton = false)
        {
            btnQMLoc = location.GetMenuName();
            Initialize(false, btnText, posX, posY, toolTipText, menuTitle, halfButton);
        }

        public QMNestedButton(string location, float posX, float posY, string btnText, string toolTipText, string menuTitle, bool halfButton = false)
        {
            btnQMLoc = location;
            Initialize(location.StartsWith("Menu_"), btnText, posX, posY, toolTipText, menuTitle, halfButton);
        }


        internal QMNestedButton(QMNestedButton location, float posX, float posY, string btnText, string toolTipText, string menuTitle, bool halfButton = false)
        {
            btnQMLoc = location.GetMenuName();
            Initialize(false, btnText, posX, posY, toolTipText, menuTitle, halfButton);
        }

        private void Initialize(bool isRoot, string btnText, float btnPosX, float btnPosY, string btnToolTipText, string menuTitle, bool halfButton)
        {
            MenuName = $"{APIUtils.Identifier}-Menu-{APIUtils.RandomNumbers()}";
            MenuObject = UnityEngine.Object.Instantiate(APIUtils.GetQMMenuTemplate(), APIUtils.GetQMMenuTemplate().transform.parent);
            MenuObject.name = MenuName;
            MenuObject.SetActive(false);
            UnityEngine.Object.DestroyImmediate(MenuObject.GetComponent<LaunchPadQMMenu>());
            MenuPage = MenuObject.AddComponent<UIPage>();
            MenuPage.field_Public_String_0 = MenuName;
            MenuPage.field_Protected_MenuStateController_0 = APIUtils.MenuStateControllerInstance;
            MenuPage.field_Private_List_1_UIPage_0 = new();
            MenuPage.field_Private_List_1_UIPage_0.Add(MenuPage);
            APIUtils.MenuStateControllerInstance.field_Private_Dictionary_2_String_UIPage_0.Add(MenuName, MenuPage);

            IsMenuRoot = isRoot;

            if (IsMenuRoot)
            {
                var list = APIUtils.MenuStateControllerInstance.field_Public_ArrayOf_UIPage_0.ToList();
                list.Add(MenuPage);
                APIUtils.MenuStateControllerInstance.field_Public_ArrayOf_UIPage_0 = list.ToArray();
            }

            MenuObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup").DestroyChildren();
            MenuTitleText = MenuObject.GetComponentInChildren<TextMeshProUGUI>(true);
            SetMenuTitle(menuTitle);
            BackButton = MenuObject.transform.GetChild(0).Find("LeftItemContainer/Button_Back").gameObject;
            BackButton.SetActive(true);
            BackButton.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
            BackButton.GetComponentInChildren<Button>().onClick.AddListener(new System.Action(() =>
            {
                if (isRoot)
                {
                    if (btnQMLoc.StartsWith("Menu_"))
                    {
                        APIUtils.MenuStateControllerInstance.Method_Public_Void_String_Boolean_Boolean_0("QuickMenu" + btnQMLoc.Remove(0, 5));
                        return;
                    }
                    APIUtils.MenuStateControllerInstance.Method_Public_Void_String_Boolean_Boolean_0(btnQMLoc);
                    return;
                }
                MenuPage.Method_Protected_Virtual_New_Void_0();
            }));
            MenuObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Expand").gameObject.SetActive(false);
            MainButton = new QMSingleButton(btnQMLoc, btnPosX, btnPosY, btnText, OpenMe, btnToolTipText, halfButton);

            ClearChildren();
            MenuObject.transform.Find("ScrollRect").GetComponent<ScrollRect>().enabled = false;
        }

        internal void OpenMe()
        {
            MenuObject.SetActive(true);
           //TODO: Fix Menu State Controller
            // APIUtils.MenuStateControllerInstance.Method_Public_Void_String_UIContext_Boolean_TransitionType_0(MenuPage.field_Public_String_0, null, false, TransitionType.Left);
        }

        internal void CloseMe()
        {
            MenuPage.Method_Public_Virtual_New_Void_0();
        }

        internal QMSingleButton GetMainButton() => MainButton;

        internal GameObject GetBackButton() => BackButton;
    }*/
}
