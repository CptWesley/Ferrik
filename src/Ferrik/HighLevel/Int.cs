using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Ferrik.LowLevel;

namespace Ferrik.HighLevel
{
    /// <summary>
    /// Represents an integer expression.
    /// </summary>
    public class Int : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Int"/> class.
        /// </summary>
        /// <param name="value">The value of the integer.</param>
        public Int(int value)
            => Value = value;

        /// <summary>
        /// Gets the value of the integer.
        /// </summary>
        public int Value { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Dictionary<string, int> locals)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            il.Emit(new LdcI4OpCode(Value));
        }
    }
}
