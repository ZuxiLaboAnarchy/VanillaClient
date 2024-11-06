// /*
//  *
//  * VanillaClient - LuaBindings.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

namespace Vanilla.Scripting.Lua
{
    internal class LuaBindings
    {
        public static void LuaPrint(string message)
        {
            Log("LuaApi", message, ConsoleColor.DarkBlue);
        }
    }
}
