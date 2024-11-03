// /*
//  *
//  * VanillaClient - ISourceView.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn.Text
{
    internal interface ISourceView : IStringView
    {
        string SourcePath { get; }
    }

    internal interface ISourceView<out TCharIterator> : ISourceView, IStringView<TCharIterator> where TCharIterator : struct, CharacterIterator
    {
    }
}
