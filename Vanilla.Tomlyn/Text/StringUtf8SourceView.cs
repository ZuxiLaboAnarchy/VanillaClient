// /*
//  *
//  * VanillaClient - StringUtf8SourceView.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System.Text;

namespace Vanilla.Tomlyn.Text
{
    internal struct StringUtf8SourceView : ISourceView<StringCharacterUtf8Iterator>
    {
        private readonly byte[] _text;

        public StringUtf8SourceView(byte[] text, string sourcePath)
        {
            this._text = text;
            SourcePath = sourcePath;
        }

        public string SourcePath { get; }

        public string GetString(int offset, int length)
        {
            if (offset + length <= _text.Length)
            {
                return Encoding.UTF8.GetString(_text, offset, length);
            }
            return null;
        }

        public StringCharacterUtf8Iterator GetIterator()
        {
            return new StringCharacterUtf8Iterator(_text);
        }
    }
}
