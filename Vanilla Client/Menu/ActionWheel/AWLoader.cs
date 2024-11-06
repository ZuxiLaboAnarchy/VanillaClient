// /*
//  *
//  * VanillaClient - AWLoader.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using MelonLoader;
using System.Collections;
using Vanilla.Modules.Manager;
using static Vanilla.Menu.ActionWheel.API.ActionWheelAPI;

namespace Vanilla.Menu.ActionWheel
{
    internal class AWLoader : VanillaModule
    {
        private static ActionMenuPage mainAW = null;

        internal override void LateStart()
        {
            MelonCoroutines.Start(WaitForAM());
        }

        private static IEnumerator WaitForAM()
        {
            while (ActionMenuDriver.prop_ActionMenuDriver_0 == null) yield return null;
            InitializeActionMenu();
            yield break;
        }
        internal static void InitializeActionMenu()
        {
            mainAW = new ActionMenuPage(ActionMenuBaseMenu.MainMenu, "Vanilla Client",
                AssetLoader.LoadTexture("VanillaClientLogo"));
        }
    }
}
