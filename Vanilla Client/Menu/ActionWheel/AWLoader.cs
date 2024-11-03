// /*
//  *
//  * VanillaClient - AWLoader.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using Vanilla.Modules.Manager;
using static Vanilla.Menu.ActionWheel.API.ActionWheelAPI;

namespace Vanilla.Menu.ActionWheel
{
    internal class AWLoader : VanillaModule
    {
        private static ActionMenuPage mainAW = null;

        internal override void LateStart()
        {
        }


        internal override void OnQuickMenuLoaded()
        {
            mainAW = new ActionMenuPage(ActionMenuBaseMenu.MainMenu, "Vanilla Client",
                AssetLoader.LoadTexture("VanillaClientLogo"));
        }
    }
}
