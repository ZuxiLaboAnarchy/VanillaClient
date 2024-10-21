namespace Vanilla.Scripting.Lua
{
    internal class LuaBindings
    {
        public static void LuaPrint(string message)
        {
            LogHandler.Log("LuaApi", message, ConsoleColor.DarkBlue);
        }
    }
}
