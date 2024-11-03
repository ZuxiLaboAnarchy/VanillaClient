// /*
//  *
//  * VanillaClient - ValueSyntax.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn.Syntax
{
    /// <summary>
    /// Base class for all TOML values.
    /// </summary>
    public abstract class ValueSyntax : SyntaxNode
    {
        internal ValueSyntax(SyntaxKind kind) : base(kind)
        {
        }
    }
}
