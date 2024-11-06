// /*
//  *
//  * VanillaClient - ConsoleExt.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

namespace Vanilla.Utils
{
    using System;

    public static class ConsoleExt
    {
        public static string ToHex(this ConsoleColor color)
        {
            return color switch
            {
                ConsoleColor.Black => "#2E2E2E", // Soft Black
                ConsoleColor.DarkBlue => "#6A8DFF", // Pastel Blue
                ConsoleColor.DarkGreen => "#77DD77", // Pastel Green
                ConsoleColor.DarkCyan => "#66CCCC", // Pastel Teal
                ConsoleColor.DarkRed => "#FF6961", // Pastel Red
                ConsoleColor.DarkMagenta => "#FFB7C5", // Pastel Pink
                ConsoleColor.DarkYellow => "#FDFD96", // Pastel Yellow
                ConsoleColor.Gray => "#D3D3D3", // Light Gray
                ConsoleColor.DarkGray => "#A9A9A9", // Soft Dark Gray
                ConsoleColor.Blue => "#AEC6CF", // Light Pastel Blue
                ConsoleColor.Green => "#B4E7B4", // Light Pastel Green
                ConsoleColor.Cyan => "#AEEEEE", // Pastel Cyan
                ConsoleColor.Red => "#FFB6B6", // Light Pastel Red
                ConsoleColor.Magenta => "#FFB6C1", // Light Pastel Pink
                ConsoleColor.Yellow => "#FFFFCC", // Light Pastel Yellow
                ConsoleColor.White => "#FFFFFF", // White (unchanged)
                _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
            };
        }

        internal static ConsoleColor ClosestConsoleColor(this UnityEngine.Color _color)
        {
            var result = ConsoleColor.White;
            double rValue = (int)_color.r * 255f;
            double gValue = (int)_color.g * 255f;
            double bValue = (int)_color.b * 255f;
            var num4 = double.MaxValue;
            foreach (ConsoleColor value in Enum.GetValues(typeof(ConsoleColor)))
            {
                var name = Enum.GetName(typeof(ConsoleColor), value);
                var color = System.Drawing.Color.FromName(name == "DarkYellow" ? "Orange" : name);
                var num5 = Math.Pow((double)(int)color.R - rValue, 2.0) + Math.Pow((double)(int)color.G - gValue, 2.0) +
                           Math.Pow((double)(int)color.B - bValue, 2.0);
                if (num5 == 0.0)
                {
                    return value;
                }

                if (num5 < num4)
                {
                    num4 = num5;
                    result = value;
                }
            }

            return result;
        }
    }
}
