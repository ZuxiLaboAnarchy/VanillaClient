using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Modules;
using NLua;
using VRC.UI.Core;

namespace Vanilla.Scripting.Lua
{
    internal class LuaBase : VanillaModule
    {
        /*

        NLua.Lua lua = new NLua.Lua();
        internal override void Start()
        {
            // Create a new player object
           // LuaManager.Instance.RegisterObject("LogHandler.", player);
            LuaManager.Instance.lua.RegisterFunction("print", this, typeof(LuaBindings).GetMethod("LuaPrint"));
            // Pass the player object to Lua
         //   lua["player"] = player;

                // Lua script using the player object


        }

        internal override void Update()
        {
            LuaManager.Instance.CallLuaFunction("Update");
        }

        internal override void Debug()
        {

            LuaManager.Instance.ExecuteLuaScript(
            @"
                  function Update()
                    print('Lua Update:', dt)


                ");
        }*/
    }
}