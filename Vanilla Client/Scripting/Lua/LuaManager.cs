using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            LuaFunction luaFunction = lua[functionName] as LuaFunction;
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
