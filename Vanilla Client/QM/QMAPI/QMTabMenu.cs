﻿using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vanilla.Helpers;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Menus;
using VRC.UI.Elements.Tooltips;

namespace Vanilla.Buttons.QM
{
    public class QMTabMenu : QMMenuBase
    {
        protected GameObject MainButton;
        protected GameObject BadgeObject;
        protected TextMeshProUGUI BadgeText;
        protected MenuTab MenuTabComp;

        public QMTabMenu(string ToolTipText, string MenuTitle, Sprite ButtonImage = null)
        {
            Initialize(ToolTipText, MenuTitle, ButtonImage);
        }

        private void Initialize(string ToolTipText, string MenuTitle, Sprite ButtonImage)
        {
            MenuName = $"{Helpers.Buttons.Identifier}-TabMenu";
            MenuObject = UnityEngine.Object.Instantiate(Helpers.Buttons.GetQMMenuTemplate(), Helpers.Buttons.GetQMMenuTemplate().transform.parent);
            MenuObject.name = MenuName;
            MenuObject.SetActive(false);
            UnityEngine.Object.DestroyImmediate(MenuObject.GetComponent<LaunchPadMenuQM>());
            MenuPage = MenuObject.AddComponent<UIPage>();
            MenuPage.field_Public_String_0 = MenuName;
            MenuPage.field_Protected_MenuStateController_0 = Helpers.Buttons.MenuStateControllerInstance;
            MenuPage.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
            MenuPage.field_Private_List_1_UIPage_0.Add(MenuPage);
            Helpers.Buttons.MenuStateControllerInstance.field_Private_Dictionary_2_String_UIPage_0.Add(MenuName, MenuPage);

            var tmpList = Helpers.Buttons.MenuStateControllerInstance.field_Public_ArrayOf_UIPage_0.ToList();
            tmpList.Add(MenuPage);
            Helpers.Buttons.MenuStateControllerInstance.field_Public_ArrayOf_UIPage_0 = tmpList.ToArray();

            MenuObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup").DestroyChildren();
            MenuTitleText = MenuObject.GetComponentInChildren<TextMeshProUGUI>(true);
            SetMenuTitle(MenuTitle);
            MenuObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Expand").gameObject.SetActive(false);
            ClearChildren();
            MenuObject.transform.Find("ScrollRect").GetComponent<ScrollRect>().enabled = false;

            MainButton = UnityEngine.Object.Instantiate(Helpers.Buttons.GetQMTabButtonTemplate(), Helpers.Buttons.GetQMTabButtonTemplate().transform.parent);
            MainButton.name = MenuName;
            MenuTabComp = MainButton.GetComponent<MenuTab>();
            MenuTabComp.field_Private_MenuStateController_0 = Helpers.Buttons.MenuStateControllerInstance;
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

        public void SetImage(Sprite newImg)
        {
            MainButton.transform.Find("Icon").GetComponent<Image>().sprite = newImg;
            MainButton.transform.Find("Icon").GetComponent<Image>().overrideSprite = newImg;
            MainButton.transform.Find("Icon").GetComponent<Image>().color = Color.white;
            MainButton.transform.Find("Icon").GetComponent<Image>().m_Color = Color.white;
        }

        public void SetToolTip(string newText)
        {
            MainButton.GetComponent<UiTooltip>().field_Public_String_0 = newText;
        }

        public void SetIndex(int newPosition)
        {
            MainButton.transform.SetSiblingIndex(newPosition);
        }

        public void SetActive(bool newState)
        {
            MainButton.SetActive(newState);
        }

        public void SetBadge(bool showing = true, string text = "")
        {
            if (BadgeObject == null || BadgeText == null)
                return;

            BadgeObject.SetActive(showing);
            BadgeText.text = text;
        }

        public GameObject GetMainButton() => MainButton;
    }
}
