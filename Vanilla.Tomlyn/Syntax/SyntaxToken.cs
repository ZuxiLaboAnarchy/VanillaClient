// /*
//  *
//  * VanillaClient - SyntaxToken.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn.Syntax
{
    /// <summary>
    /// A token node.
    /// </summary>
    public class SyntaxToken : SyntaxNode
    {
        /// <summary>
        /// Creates a new instance of <see cref="SyntaxToken"/>
        /// </summary>
        public SyntaxToken() : base(SyntaxKind.Token)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="SyntaxToken"/>
        /// </summary>
        /// <param name="tokenKind">The type of token</param>
        /// <param name="text">The associated textual representation</param>
        public SyntaxToken(TokenKind tokenKind, string text) : this()
        {
            TokenKind = tokenKind;
            Text = text;
        }

        /// <summary>
        /// Gets or sets the kind of token.
        /// </summary>
        public TokenKind TokenKind { get; set; }

        /// <summary>
        /// Gets or sets the associated text
        /// </summary>
        public string Text { get; set; }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int ChildrenCount => 0;

        protected override SyntaxNode GetChildrenImpl(int index)
        {
            return null;
        }
    }
}
