﻿// /*
//  *
//  * VanillaClient - QMTabMenu.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

namespace Vanilla.Menu.QM.API.Legacy
{
    /*
    internal class QMTabMenu : QMMenuBase
    {
        protected GameObject MainButton;
        protected GameObject BadgeObject;
        protected TextMeshProUGUI BadgeText;
        protected MenuTab MenuTabComp;

        internal QMTabMenu(string ToolTipText, string MenuTitle, Sprite ButtonImage = null)
        {
            Initialize(ToolTipText, MenuTitle, ButtonImage);
        }

        private void Initialize(string ToolTipText, string MenuTitle, Sprite ButtonImage)
        {
            MenuName = $"{APIUtils.Identifier}-TabMenu-{APIUtils.RandomNumbers()}";
            MenuObject = UnityEngine.Object.Instantiate(APIUtils.GetQMMenuTemplate(), APIUtils.GetQMMenuTemplate().transform.parent);
            MenuObject.name = MenuName;
            MenuObject.SetActive(false);
            UnityEngine.Object.DestroyImmediate(MenuObject.GetComponent<LaunchPadQMMenu>());
            MenuPage = MenuObject.AddComponent<UIPage>();
            MenuPage.field_Public_String_0 = MenuName;
            MenuPage.field_Private_Boolean_1 = true;
            MenuPage.field_Protected_MenuStateController_0 = APIUtils.MenuStateControllerInstance;
            MenuPage.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
            MenuPage.field_Private_List_1_UIPage_0.Add(MenuPage);
            APIUtils.MenuStateControllerInstance.field_Private_Dictionary_2_String_UIPage_0.Add(MenuName, MenuPage);

            var tmpList = APIUtils.MenuStateControllerInstance.field_Public_ArrayOf_UIPage_0.ToList();
            tmpList.Add(MenuPage);
            APIUtils.MenuStateControllerInstance.field_Public_ArrayOf_UIPage_0 = tmpList.ToArray();

            MenuObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup").DestroyChildren();
            MenuTitleText = MenuObject.GetComponentInChildren<TextMeshProUGUI>(true);
            SetMenuTitle(MenuTitle);
            MenuObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Expand").gameObject.SetActive(false);
            ClearChildren();
            MenuObject.transform.Find("ScrollRect").GetComponent<ScrollRect>().enabled = false;

            MainButton = UnityEngine.Object.Instantiate(APIUtils.GetQMTabButtonTemplate(), APIUtils.GetQMTabButtonTemplate().transform.parent);
            MainButton.name = MenuName;
            MenuTabComp = MainButton.GetComponent<MenuTab>();
            MenuTabComp.field_Private_MenuStateController_0 = APIUtils.MenuStateControllerInstance;
            MenuTabComp.field_Public_String_0 = MenuName;
            MenuTabComp.GetComponent<StyleElement>().field_Private_Selectable_0 = MenuTabComp.GetComponent<Button>();
            BadgeObject = MainButton.transform.GetChild(0).gameObject;
            BadgeText = BadgeObject.GetComponentInChildren<TextMeshProUGUI>();
            MainButton.GetComponent<Button>().onClick.AddListener(new System.Action(() =>
            {
                MenuObject.SetActive(true);
                MenuTabComp.GetComponent<StyleElement>().field_Private_Selectable_0 = MenuTabComp.GetComponent<Button>();
            }));

            SetToolTip(ToolTipText);
            if (ButtonImage != null)
            {
                SetImage(ButtonImage);
            }
        }

        internal void SetImage(Sprite newImg)
        {
            UnityEngine.Object.Destroy(MainButton.transform.Find("Icon").GetComponent<StyleElement>());

            MainButton.transform.Find("Icon").GetComponent<Image>().color = Color.white;
            MainButton.transform.Find("Icon").GetComponent<Image>().m_Color = Color.white;
            MainButton.transform.Find("Icon").GetComponent<Image>().sprite = newImg;
            MainButton.transform.Find("Icon").GetComponent<Image>().overrideSprite = newImg;
            MainButton.transform.Find("Icon").GetComponent<Image>().color = Color.white;
            MainButton.transform.Find("Icon").GetComponent<Image>().m_Color = Color.white;
        }

        internal void SetToolTip(string newText)
        {//TODO: Fix ToolTips
            return;
         //   MainButton.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = newText;
        }

        internal void SetIndex(int newPosition)
        {
            MainButton.transform.SetSiblingIndex(newPosition);
        }

        internal void SetActive(bool newState)
        {
            MainButton.SetActive(newState);
        }

        internal void SetBadge(bool showing = true, string text = "")
        {
            if (BadgeObject == null || BadgeText == null)
                return;

            BadgeObject.SetActive(showing);
            BadgeText.text = text;
        }

        internal GameObject GetMainButton() => MainButton;
    }*/
}
