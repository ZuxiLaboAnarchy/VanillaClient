// /*
//  *
//  * VanillaClient - ULogHander.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

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
            catch (Exception e)
            {
                ExceptionHandler("HUD 367", e);
            }

            if (!logToConsole)
            {
                return;
            }

            var num = text.LastIndexOf("<color=");
            if (num != -1)
            {
                var num2 = text.IndexOf('>', num);
                var htmlString = text.Substring(num + 7, num2 - (num + 7));
                if (ColorUtility.TryParseHtmlString(htmlString, out var color))
                {
                    Log(identifier, text, color.ClosestConsoleColor(), "InformHudText");
                }
                else
                {
                    Log(identifier, text, ConsoleColor.Gray);
                }
            }
        }
    }
}
