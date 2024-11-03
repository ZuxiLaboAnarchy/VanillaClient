// /*
//  *
//  * VanillaClient - TomlFloat.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System;
using System.Globalization;

namespace Vanilla.Tomlyn.Model
{
    /// <summary>
    /// Runtime representation of a TOML float
    /// </summary>
    public sealed class TomlFloat : TomlValue<double>
    {
        public TomlFloat(double value) : base(ObjectKind.Float, value)
        {
        }

        public override string ToString()
        {
            if (double.IsNaN(Value))
            {
                return BitConverter.DoubleToInt64Bits(Value) > 0 ? "+nan" : "nan";
            }
            if (double.IsPositiveInfinity(Value))
            {
                return "+inf";
            }
            if (double.IsNegativeInfinity(Value))
            {
                return "-inf";
            }
            return AppendDecimalPoint(Value.ToString("g16", CultureInfo.InvariantCulture));
        }

        private static string AppendDecimalPoint(string text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];
                // Do not append a decimal point if floating point type value
                // - is in exponential form, or
                // - already has a decimal point
                if (c == 'e' || c == 'E' || c == '.')
                {
                    return text;
                }
            }
            return text + ".0";
        }
    }
}
