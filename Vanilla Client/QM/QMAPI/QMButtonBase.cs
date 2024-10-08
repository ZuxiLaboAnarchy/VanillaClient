using System.Linq;
using UnityEngine;
/*
namespace Vanilla.Buttons.QM
{
    internal class QMButtonBase
    {
        protected GameObject button;
        protected string btnQMLoc;
        protected int[] initShift = { 0, 0 };

        internal GameObject GetGameObject() => button;

        internal void SetActive(bool state) => button.gameObject.SetActive(state);

        internal void SetLocation(float buttonXLoc, float buttonYLoc)
        {
            button.GetComponent<RectTransform>().anchoredPosition += Vector2.right * (232 * (buttonXLoc + initShift[0]));
            button.GetComponent<RectTransform>().anchoredPosition += Vector2.down * (210 * (buttonYLoc + initShift[1]));
        }
        // TODO: Fix ToolTips
        internal void SetToolTip(string buttonToolTip) { return;  } //=> //button.GetComponents<VRC.UI.Elements.Tooltips.UiTooltip>().ToList().ForEach(x => x.field_Public_String_0 = buttonToolTip);

        internal void DestroyMe()
        {
            try
            {
                UnityEngine.Object.Destroy(button);
            }
            catch { }
        }
    }
}*/