// /*
//  *
//  * VanillaClient - ThrowHelper.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System;

namespace Vanilla.Tomlyn.Helpers
{
    internal static class ThrowHelper
    {
        public static ArgumentOutOfRangeException GetIndexNegativeArgumentOutOfRangeException()
        {
            return new ArgumentOutOfRangeException("index", "Index must be positive");
        }
        public static ArgumentOutOfRangeException GetIndexArgumentOutOfRangeException(int maxValue)
        {
            return new ArgumentOutOfRangeException("index", $"Index must be less than {maxValue}");
        }
        public static InvalidOperationException GetExpectingNoParentException()
        {
            return new InvalidOperationException("The node is already attached to another parent");
        }
    }
}
