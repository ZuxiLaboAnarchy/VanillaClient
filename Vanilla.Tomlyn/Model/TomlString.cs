// /*
//  *
//  * VanillaClient - TomlString.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn.Model
{
    /// <summary>
    /// Runtime representation of a TOML string
    /// </summary>
    public sealed class TomlString : TomlValue<string>
    {
        public TomlString(string value) : base(ObjectKind.String, value)
        {
        }
    }
}
