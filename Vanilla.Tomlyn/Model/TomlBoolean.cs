// /*
//  *
//  * VanillaClient - TomlBoolean.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn.Model
{
    /// <summary>
    /// Runtime representation of a TOML bool
    /// </summary>
    public sealed class TomlBoolean : TomlValue<bool>
    {
        public TomlBoolean(bool value) : base(ObjectKind.Boolean, value)
        {
        }
    }
}
