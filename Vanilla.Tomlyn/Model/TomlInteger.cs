// /*
//  *
//  * VanillaClient - TomlInteger.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn.Model
{
    /// <summary>
    /// Runtime representation of a TOML integer
    /// </summary>
    public sealed class TomlInteger : TomlValue<long>
    {
        public TomlInteger(long value) : base(ObjectKind.Integer, value)
        {
        }
    }
}
