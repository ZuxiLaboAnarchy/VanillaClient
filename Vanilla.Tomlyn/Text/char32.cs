// /*
//  *
//  * VanillaClient - char32.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn.Text
{
    /// <summary>
    /// A UTF-32 character ala Stark.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal readonly struct char32
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="char32"/> UTF-32 character.
        /// </summary>
        /// <param name="code">The UTF-32 code character.</param>
        public char32(int code)
        {
            Code = code;
        }

        /// <summary>
        /// Gets the UTF-32 code.
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="char32"/> to <see cref="System.Int32"/>.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator int(char32 c)
        {
            return c.Code;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="char32"/>.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator char32(int c)
        {
            return new char32(c);
        }

        public override string ToString()
        {
            return char.ConvertFromUtf32(Code);
        }
    }
}
