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

            il.Emit(GetOpCode());
        }

        private TypedOpCode GetOpCode()
            => Value switch
            {
                0 => TypedOpCodes.LdcI4_0,
                1 => TypedOpCodes.LdcI4_1,
                2 => TypedOpCodes.LdcI4_2,
                3 => TypedOpCodes.LdcI4_3,
                4 => TypedOpCodes.LdcI4_4,
                5 => TypedOpCodes.LdcI4_5,
                6 => TypedOpCodes.LdcI4_6,
                7 => TypedOpCodes.LdcI4_7,
                8 => TypedOpCodes.LdcI4_8,
                int x when x >= -128 && x <= 127 => TypedOpCodes.LdcI4S((sbyte)Value),
                _ => TypedOpCodes.LdcI4(Value),
            };
    }
}
