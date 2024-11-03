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