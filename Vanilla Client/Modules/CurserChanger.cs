// /*
//  *
//  * VanillaClient - CurserChanger.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using UnityEngine;
using Vanilla.Modules.Manager;

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
