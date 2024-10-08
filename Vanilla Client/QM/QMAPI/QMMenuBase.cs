
using TMPro;
using UnityEngine;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
/*
namespace Vanilla.Buttons.QM
{
    internal class QMMenuBase
    {
        protected string btnQMLoc;
        protected GameObject MenuObject;
        protected TextMeshProUGUI MenuTitleText;
        protected UIPage MenuPage;
        protected string MenuName;

        internal string GetMenuName() => MenuName;

        internal UIPage GetMenuPage() => MenuPage;

        internal GameObject GetMenuObject() => MenuObject;

        internal void SetMenuTitle(string newTitle) => MenuObject.GetComponentInChildren<TextMeshProUGUI>(true).text = newTitle;

        internal void ClearChildren()
        {
            for (int i = 0; i < MenuObject.transform.childCount; i++)
            {
                if (MenuObject.transform.GetChild(i).name != "Header_H1" && MenuObject.transform.GetChild(i).name != "ScrollRect")
                {
                    UnityEngine.Object.Destroy(MenuObject.transform.GetChild(i).gameObject);
                }
            }
        }
    }
}*/