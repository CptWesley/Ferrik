using System;
using System.Collections.Generic;

namespace Ferrik
{
    /// <summary>
    /// Represents a variable scope.
    /// </summary>
    public class Scope
    {
        private readonly Dictionary<string, ushort> dictionary = new Dictionary<string, ushort>();
        private readonly object lck = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="Scope"/> class.
        /// </summary>
        /// <param name="parent">The parent scope.</param>
        public Scope(Scope parent)
            => Parent = parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scope"/> class.
        /// </summary>
        public Scope()
            : this(null)
        {
        }

        /// <summary>
        /// Gets the parent scope.
        /// </summary>
        public Scope Parent { get; }

        /// <summary>
        /// Adds a variable to the scope.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="index">The index of the variable.</param>
        public void Add(string name, ushort index)
        {
            lock (lck)
            {
                if (dictionary.ContainsKey(name))
                {
                    throw new ArgumentException($"Duplicate definition of variable name '{name}'.", nameof(name));
                }

                dictionary[name] = index;
            }
        }

        /// <summary>
        /// Adds a variable to the scope.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="index">The index of the variable.</param>
        public void Add(string name, int index)
        {
            ushort actualIndex = (ushort)index;

            if (actualIndex != index)
            {
                throw new ArgumentException($"Index is out of ushort bounds.", nameof(index));
            }

            Add(name, actualIndex);
        }

        /// <summary>
        /// Gets the variable from the scope.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <returns>The index of the variable.</returns>
        public ushort Get(string name)
        {
            if (dictionary.TryGetValue(name, out ushort result))
            {
                return result;
            }

            if (Parent != null)
            {
                return Parent.Get(name);
            }

            throw new ArgumentException($"Variable '{name}' not defined.");
        }

        /// <summary>
        /// Creates a new child scope.
        /// </summary>
        /// <returns>The new child scope.</returns>
        public Scope CreateChild()
            => new Scope(this);
    }
}
