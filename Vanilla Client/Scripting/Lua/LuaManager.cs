// /*
//  *
//  * VanillaClient - LuaManager.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using NLua;

namespace Vanilla.Scripting.Lua
{
    public class LuaManager
    {
        private static LuaManager instance = null;
        internal NLua.Lua lua;

        // Private constructor to enforce singleton pattern
        private LuaManager()
        {
            lua = new NLua.Lua();
        }

        // Get the singleton instance of LuaManager
        public static LuaManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LuaManager();
                }

                return instance;
            }
        }

        // Execute Lua code
        public void ExecuteLuaScript(string luaCode)
        {
            lua.DoString(luaCode);
        }

        // Call a Lua function
        public void CallLuaFunction(string functionName, params object[] args)
        {
            var luaFunction = lua[functionName] as LuaFunction;
            if (luaFunction != null)
            {
                luaFunction.Call(args);
            }
            else
            {
                Console.WriteLine($"Lua function {functionName} not found!");
            }
        }

        // Register a C# object to Lua
        public void RegisterObject(string objectName, object obj)
        {
            lua[objectName] = obj;
        }

        // Load Lua file (if you want to load scripts from external files)
        public void LoadLuaFile(string filePath)
        {
            lua.DoFile(filePath);
        }
    }
}
