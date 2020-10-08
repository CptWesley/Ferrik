using System;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.ExpressionTypes
{
    /// <summary>
    /// Represents an integer expression.
    /// </summary>
    public class IntExpression : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntExpression"/> class.
        /// </summary>
        /// <param name="value">The value of the integer.</param>
        public IntExpression(int value)
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
