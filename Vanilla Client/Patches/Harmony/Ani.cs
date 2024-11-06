// /*
//  *
//  * VanillaClient - Ani.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using Vanilla.Patches.Manager;

namespace Vanilla.Patches.Harmony
{
    internal class Ani : VanillaPatches
    {
        internal override void Patch()
        {
            ///   PatchMethod(typeof(Analytics).GetMethod(nameof(Analytics.Update)), GetPatch(nameof(ReturnFalse)), null);
        }
    }
}
