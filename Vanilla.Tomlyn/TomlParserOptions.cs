// /*
//  *
//  * VanillaClient - TomlParserOptions.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn
{
    /// <summary>
    /// Options for parsing a TOML string.
    /// </summary>
    public enum TomlParserOptions
    {
        /// <summary>
        /// Parse and validate.
        /// </summary>
        ParseAndValidate = 0,

        /// <summary>
        /// Parse only the document.
        /// </summary>
        ParseOnly = 1,
    }
}
