﻿// /*
//  *
//  * VanillaClient - ModuleModel.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

namespace Vanilla.Modules.Manager
{
    internal abstract class VanillaModule
    {
        protected virtual string ModuleName => "Undefined Module";

        internal virtual void UI()
        {
        }

        internal virtual void Start()
        {
        }

        internal virtual void Stop()
        {
        }

        internal virtual void Update()
        {
        }

        internal virtual void LateStart()
        {
        }

        internal virtual void LateUpdate()
        {
        }

        internal virtual void WaitForPlayer()
        {
        }

        internal virtual void WaitForAPIUser()
        {
        }

        internal virtual void OnGUI()
        {
        }

        internal virtual void PlayerEvent()
        {
        }

        internal virtual void Debug()
        {
        }

        internal virtual void WorldLoad(int level)
        {
        }

        internal virtual void WorldUnload(int level)
        {
        }

        internal virtual void PlayerJoin(VRC.Player player)
        {
        }

        internal virtual void PlayerLeave(VRC.Player player)
        {
        }

        internal virtual void AppFocus(bool state)
        {
        }

        internal virtual void OnUiManagerInit()
        {
        }

        internal virtual void OnQuickMenuLoaded()
        {
        }

        internal string GetModuleName()
        {
            return ModuleName;
        }
    }
}
