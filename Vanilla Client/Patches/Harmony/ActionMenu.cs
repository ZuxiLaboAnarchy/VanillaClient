// /*
//  *
//  * VanillaClient - ActionMenu.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using System.Linq;
using System.Reflection;
using UnhollowerRuntimeLib.XrefScans;
using Vanilla.Menu.ActionWheel.API;
using Vanilla.Patches.Manager;

namespace Vanilla.Patches.Harmony
{
    internal class ActionMenuPatch : VanillaPatches
    {
        protected override string patchName => "ActionMenuPatch";

        internal override void Patch()
        {
            InitializeLocalPatchHandler(typeof(ActionMenuPatch));
            PatchMethod(
                typeof(ActionMenu).GetMethods().FirstOrDefault((MethodInfo it) =>
                    XrefScanner.XrefScan(it).Any((XrefInstance jt) =>
                        jt.Type == XrefType.Global && jt.ReadAsObject()?.ToString() == "Emojis")), null,
                GetLocalPatch("OpenMainPage"));
        }

        private static void OpenMainPage(ActionMenu __instance)
        {
            ActionWheelAPI.OpenMainPage(__instance);
        }
    }
}
