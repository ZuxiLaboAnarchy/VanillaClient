using UnityEngine;
using Vanilla.Config;
using VRC;
using VRC.Core;

namespace Vanilla.Modules
{
    internal class ESPModule : VanillaModule
    {
        internal override void PlayerJoin(Player __0)
        {
            if (!MainConfig.ESP || __0.field_Private_APIUser_0.id == APIUser.CurrentUser.id)
                return;


            var Renderer = __0.transform.Find("SelectRegion").GetComponent<Renderer>();
            HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(Renderer, true);
        }

    }
}
