using UnityEngine;

namespace Vanilla.Utils
{
    internal class HUDHandler
    {
        internal static void InformHudText(string identifier, string text, bool logToConsole = false)
        {

             


            try
            {

            }
            catch (System.Exception e)
            {
                ExceptionHandler("HUD 367", e);
            }
            if (!logToConsole)
            {
                return;
            }
            int num = text.LastIndexOf("<color=");
            if (num != -1)
            {
                int num2 = text.IndexOf('>', num);
                string htmlString = text.Substring(num + 7, num2 - (num + 7));
                if (ColorUtility.TryParseHtmlString(htmlString, out var color))
                {
                    Log(identifier, text, ClosestConsoleColor((byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f)), "InformHudText");
                }
                else
                {
                    Log(identifier, text, System.ConsoleColor.Gray);
                }
            }
        }


    }
}
