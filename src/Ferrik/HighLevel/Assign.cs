using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Ferrik.LowLevel;

namespace Ferrik.HighLevel
{
    /// <summary>
    /// Represents an assignment statement.
    /// </summary>
    public class Assign : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Assign"/> class.
        /// </summary>
        /// <param name="name">The name of the local variable.</param>
        /// <param name="value">The new value of the variable.</param>
        public Assign(string name, Expression value)
            => (Name, Value) = (name, value);

        /// <summary>
        /// Gets the name of the declared local.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the declared local.
        /// </summary>
        public Expression Value { get; }

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

            if (!locals.TryGetValue(Name, out int temp))
            {
                throw new Exception($"Variable '{Name}' not bound.");
            }

            ushort index = (ushort)temp;

            if (index != temp)
            {
                throw new Exception($"Index of local variable out of bounds.");
            }

            Value.Emit(il, locals);
            il.Emit(new StlocOpCode(index));
        }
    }
}
