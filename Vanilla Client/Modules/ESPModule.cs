using UnityEngine;
using Vanilla.Config;
using VRC;
using VRC.Core;

namespace Vanilla.Modules
{
    internal class ESPModule : VanillaModule
    {
        protected override string ModuleName => "ESPModule";

        private HighlightsFXStandalone _highlightFX = null;
        public float colorCycleSpeed = 1.0f;

        internal override void WaitForPlayer()
        {
            _highlightFX = GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>();
        }

        internal override void PlayerJoin(Player __0)
        {
            if (!GetInstance().ESP || __0.field_Private_APIUser_0.id == APIUser.CurrentUser.id)
            {
                return;
            }

            var Renderer = __0.transform.Find("SelectRegion").GetComponent<Renderer>();
            HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(Renderer, true);
        }

        internal override void Update()
        {
            if (_highlightFX is not null)
            {
                _highlightFX.highlightColor = GetRainbowColor(Time.time * colorCycleSpeed);
            }
        }

        private Color GetRainbowColor(float t)
        {
            t = t % 1f; // Loop the value between 0 and 1
            return new Color(
                Mathf.Sin(t * (float)Math.PI * 2f) * 0.5f + 0.5f, // Red
                Mathf.Sin((t + 0.333f) * (float)Math.PI * 2f) * 0.5f + 0.5f, // Green
                Mathf.Sin((t + 0.666f) * (float)Math.PI * 2f) * 0.5f + 0.5f // Blue
            );
        }
    }
}