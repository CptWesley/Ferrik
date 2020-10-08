using System;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.ExpressionTypes
{
    /// <summary>
    /// Represents an integer expression.
    /// </summary>
    public class BoolExpression : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoolExpression"/> class.
        /// </summary>
        /// <param name="value">The value of the integer.</param>
        public BoolExpression(bool value)
            => Value = value;

        /// <summary>
        /// Gets a value indicating whether the value is true or false.
        /// </summary>
        public bool Value { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Scope scope)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            il.Emit(Value ? TypedOpCodes.LdcI4_1 : TypedOpCodes.LdcI4_0);
        }
    }
}
