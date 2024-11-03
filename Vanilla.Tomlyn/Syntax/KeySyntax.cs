// /*
//  *
//  * VanillaClient - KeySyntax.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System;

namespace Vanilla.Tomlyn.Syntax
{
    /// <summary>
    /// A key TOML syntax node.
    /// </summary>
    public sealed class KeySyntax : ValueSyntax
    {
        private BareKeyOrStringValueSyntax _key;

        /// <summary>
        /// Creates a new instance of a <see cref="KeySyntax"/>
        /// </summary>
        public KeySyntax() : base(SyntaxKind.Key)
        {
            DotKeys = new SyntaxList<DottedKeyItemSyntax>() { Parent = this };
        }

        /// <summary>
        /// Creates a new instance of a <see cref="KeySyntax"/>
        /// </summary>
        /// <param name="key">A simple name of this key</param>
        public KeySyntax(string key) : this()
        {
            Key = BareKeySyntax.IsBareKey(key) ? (BareKeyOrStringValueSyntax)new BareKeySyntax(key) : new StringValueSyntax(key);
        }

        /// <summary>
        /// Creates a new instance of a <see cref="KeySyntax"/>
        /// </summary>
        /// <param name="key">the base key</param>
        /// <param name="dotKey1">the key after the dot</param>
        public KeySyntax(string key, string dotKey1) : this()
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (dotKey1 == null) throw new ArgumentNullException(nameof(dotKey1));
            Key = BareKeySyntax.IsBareKey(key) ? (BareKeyOrStringValueSyntax)new BareKeySyntax(key) : new StringValueSyntax(key);
            DotKeys.Add(new DottedKeyItemSyntax(dotKey1));
        }

        /// <summary>
        /// The base of the key before the dot
        /// </summary>
        public BareKeyOrStringValueSyntax Key
        {
            get => _key;
            set => ParentToThis(ref _key, value); // TODO: add validation for type of key (basic key or string)
        }

        /// <summary>
        /// List of the dotted keys.
        /// </summary>
        public SyntaxList<DottedKeyItemSyntax> DotKeys { get; }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int ChildrenCount => 2;

        protected override SyntaxNode GetChildrenImpl(int index)
        {
            if (index == 0) return Key;
            return DotKeys;
        }
    }
}
