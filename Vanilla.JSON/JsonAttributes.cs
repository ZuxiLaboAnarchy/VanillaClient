﻿// /*
//  *
//  * VanillaClient - JsonAttributes.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System;
namespace Vanilla.JSON
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class JsonPropertyAttribute : Attribute
    {
        public string Name { get; private set; }

        public JsonPropertyAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class MatchSnakeCaseAttribute : Attribute
    {
    }
}
