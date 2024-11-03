// /*
//  *
//  * VanillaClient - IStringView.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn.Text
{
    internal interface IStringView
    {
        string GetString(int offset, int length);
    }

    internal interface IStringView<out TCharIterator> : IStringView where TCharIterator : struct, CharacterIterator
    {
        TCharIterator GetIterator();
    }

}
