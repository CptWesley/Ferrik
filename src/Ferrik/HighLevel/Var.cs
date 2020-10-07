using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Ferrik.LowLevel;

namespace Ferrik.HighLevel
{
    /// <summary>
    /// Represents a variable expression.
    /// </summary>
    public class Var : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Var"/> class.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        public Var(string name)
            => Name = name;

        /// <summary>
        /// Gets the value of the integer.
        /// </summary>
        public string Name { get; }

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

            il.Emit(new LdlocOpCode(index));
        }
    }
}
