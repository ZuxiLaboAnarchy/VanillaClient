// /*
//  *
//  * VanillaClient - InlineTableItemSyntax.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System;

namespace Vanilla.Tomlyn.Syntax
{
    /// <summary>
    /// A key-value pair item of an inline table.
    /// </summary>
    public sealed class InlineTableItemSyntax : SyntaxNode
    {
        private KeyValueSyntax _keyValue;
        private SyntaxToken _comma;

        /// <summary>
        /// Creates an instance of <see cref="InlineTableItemSyntax"/>
        /// </summary>
        public InlineTableItemSyntax() : base(SyntaxKind.InlineTable)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="InlineTableItemSyntax"/>
        /// </summary>
        /// <param name="keyValue">The key=value</param>
        public InlineTableItemSyntax(KeyValueSyntax keyValue) : this()
        {
            KeyValue = keyValue ?? throw new ArgumentNullException(nameof(keyValue));
        }

        /// <summary>
        /// Gets or sets the <see cref="KeyValueSyntax"/>.
        /// </summary>
        public KeyValueSyntax KeyValue
        {
            get => _keyValue;
            set => ParentToThis(ref _keyValue, value);
        }

        /// <summary>
        /// Gets or sets the comma, mandatory to separate entries in an inline table.
        /// </summary>
        public SyntaxToken Comma
        {
            get => _comma;
            set => ParentToThis(ref _comma, value, TokenKind.Comma);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int ChildrenCount => 2;

        protected override SyntaxNode GetChildrenImpl(int index)
        {
            return index == 0 ? (SyntaxNode)KeyValue : Comma;
        }
    }
}
