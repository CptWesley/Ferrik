using System;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.Expressions
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
        public override void Emit(ILGenerator il, Scope scope)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            il.Emit(TypedOpCodes.LdcI4(Value));
        }
    }
}
