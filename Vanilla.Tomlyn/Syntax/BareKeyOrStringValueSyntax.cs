// /*
//  *
//  * VanillaClient - BareKeyOrStringValueSyntax.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn.Syntax
{
    /// <summary>
    /// Base class for a <see cref="BareKeySyntax"/> or a <see cref="StringValueSyntax"/>
    /// </summary>
    public abstract class BareKeyOrStringValueSyntax : ValueSyntax
    {
        internal BareKeyOrStringValueSyntax(SyntaxKind kind) : base(kind)
        {
        }
    }
}
