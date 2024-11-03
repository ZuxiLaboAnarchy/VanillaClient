// /*
//  *
//  * VanillaClient - ITokenProvider.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System.Collections.Generic;
using Vanilla.Tomlyn.Syntax;
using Vanilla.Tomlyn.Text;

namespace Vanilla.Tomlyn.Parsing
{
    internal interface ITokenProvider<out TSourceView> where TSourceView : ISourceView
    {
        /// <summary>
        /// Gets a boolean indicating whether this lexer has errors.
        /// </summary>
        bool HasErrors { get; }

        TSourceView Source { get; }

        LexerState State { get; set; }

        bool MoveNext();

        SyntaxTokenValue Token { get; }

        /// <summary>
        /// Gets error messages.
        /// </summary>
        IEnumerable<DiagnosticMessage> Errors { get; }
    }
}
