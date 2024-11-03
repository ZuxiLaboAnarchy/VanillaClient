// /*
//  *
//  * VanillaClient - CharReaderException.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System;

namespace Vanilla.Tomlyn.Text
{
    internal sealed class CharReaderException : Exception
    {
        public CharReaderException(string message) : base(message)
        {
        }
    }
}
