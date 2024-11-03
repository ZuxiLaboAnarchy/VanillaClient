// /*
//  *
//  * VanillaClient - DebugManager.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using Vanilla.Modules.Manager;
using Vanilla.Wrappers;

namespace Vanilla.Modules
{
    internal class DebugManager : VanillaModule
    {
        protected override string ModuleName => "Debug Manager";

        internal override void Debug()
        {
            GeneralWrappers.CopyInstanceToClipboard();
        }
    }
}
