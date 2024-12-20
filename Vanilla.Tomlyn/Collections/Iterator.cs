// /*
//  *
//  * VanillaClient - Iterator.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn.Collections
{
    /// <summary>
    /// Iterator ala Stark.
    /// </summary>
    /// <typeparam name="TElement">The type of an element of the iteration.</typeparam>
    /// <typeparam name="TState">The type of the state of the iteration.</typeparam>
    // ReSharper disable once TypeParameterCanBeVariant
    // ReSharper disable once InconsistentNaming
    internal interface Iterator<TElement, TState> where TElement : struct // Note that in Stark, we would not have a struct constraint on TElement, as TElement? would be possible
    {
        /// <summary>
        /// Gets the start state for the iteration.
        /// </summary>
        TState Start { get; }

        /// <summary>
        /// Tries to get the next element in the iteration.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>none if no element, or an element</returns>
        TElement? TryGetNext(ref TState state);
    }
}
