using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Vanilla.Modules
{
    internal class CurserChanger : VanillaModule
    {
        internal override void OnUiManagerInit()
        {
            Cursor.SetCursor(AssetLoader.LoadTexture("cursor"), CursorMode.Auto);
        }
    }
}
