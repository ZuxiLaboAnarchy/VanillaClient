using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Vanilla.Buttons.QM;

namespace Vanilla.Buttons.QM
{
    internal class QMSingleButton : QMButtonBase
    {
        internal QMSingleButton(string btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string btnToolTip, bool halfBtn = false)
        {
            btnQMLoc = btnMenu;
            if (halfBtn)
            {
                btnYLocation -= 0.21f;
            }
            Initialize(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip);
            if (halfBtn)
            {
                button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
                button.GetComponentInChildren<TMPro.TextMeshProUGUI>().rectTransform.anchoredPosition = new Vector2(0, 22);
            }
        }

        internal QMSingleButton(QMNestedButton btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string btnToolTip, bool halfBtn = false)
        {
            btnQMLoc = btnMenu.GetMenuName();
            if (halfBtn)
            {
                btnYLocation -= 0.21f;
            }
            Initialize(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip);
            if (halfBtn)
            {
                // 2.0175f
                button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
                button.GetComponentInChildren<TMPro.TextMeshProUGUI>().rectTransform.anchoredPosition = new Vector2(0, 22);
            }
        }

        internal QMSingleButton(QMTabMenu btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string btnToolTip, bool halfBtn = false)
        {
            btnQMLoc = btnMenu.GetMenuName();
            if (halfBtn)
            {
                btnYLocation -= 0.21f;
            }
            Initialize(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip);
            if (halfBtn)
            {
                button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
                button.GetComponentInChildren<TMPro.TextMeshProUGUI>().rectTransform.anchoredPosition = new Vector2(0, 22);
            }
        }

        private void Initialize(float btnXLocation, float btnYLocation, string btnText, Action btnAction, string btnToolTip)
        {
            button = UnityEngine.Object.Instantiate(APIUtils.GetQMButtonTemplate(), APIUtils.QuickMenuInstance.transform.Find("CanvasGroup/Container/Window/QMParent/" + btnQMLoc).transform, true);
            button.transform.Find("Badge_MMJump").gameObject.SetActive(false);
            button.name = $"{APIUtils.Identifier}-Single-Button-{APIUtils.RandomNumbers()}";
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().fontSize = 30;
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 176);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(-68, -250);
            button.transform.Find("Icon").GetComponentInChildren<Image>().gameObject.SetActive(false);
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().rectTransform.anchoredPosition += new Vector2(0, 50);

            initShift[0] = 0;
            initShift[1] = 0;

            SetLocation(btnXLocation, btnYLocation);
            SetButtonText(btnText);
            SetToolTip(btnToolTip);
            SetAction(btnAction);

            SetActive(true);
        }

        internal void SetBackgroundImage(Sprite newImg)
        {
            button.transform.Find("Background").GetComponent<Image>().sprite = newImg;
            button.transform.Find("Background").GetComponent<Image>().overrideSprite = newImg;
            RefreshButton();
        }

        internal void SetButtonText(string buttonText)
        {
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = buttonText;
        }

        internal void SetAction(Action buttonAction)
        {
            button.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            if (buttonAction != null)
                button.GetComponent<Button>().onClick.AddListener(UnhollowerRuntimeLib.DelegateSupport.ConvertDelegate<UnityAction>(buttonAction));
        }

        internal void SetInteractable(bool newState)
        {
            button.GetComponent<Button>().interactable = newState;
            RefreshButton();
        }

        internal void SetFontSize(float size)
        {
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().fontSize = size;
        }

        internal void ClickMe()
        {
            button.GetComponent<Button>().onClick.Invoke();
        }

        internal Image GetBackgroundImage()
        {
            return button.transform.Find("Background").GetComponent<Image>();
        }

        private void RefreshButton()
        {
            button.SetActive(false);
            button.SetActive(true);
        }
    }
}