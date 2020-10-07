using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Ferrik.HighLevel
{
    /// <summary>
    /// Represents a variable declaration.
    /// </summary>
    public class Declare : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Declare"/> class.
        /// </summary>
        /// <param name="name">The name of the local variable.</param>
        /// <param name="type">The type of the local variable.</param>
        public Declare(string name, Type type)
            => (Name, Type) = (name, type);

        /// <summary>
        /// Gets the name of the declared local.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the declared local.
        /// </summary>
        public Type Type { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Dictionary<string, int> locals)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            if (locals is null)
            {
                throw new ArgumentNullException(nameof(locals));
            }

            LocalBuilder lb = il.DeclareLocal(Type);
            locals[Name] = lb.LocalIndex;
        }
    }
}
