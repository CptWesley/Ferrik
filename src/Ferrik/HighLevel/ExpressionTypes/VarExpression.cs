using System;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.ExpressionTypes
{
    /// <summary>
    /// Represents a variable expression.
    /// </summary>
    public class VarExpression : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VarExpression"/> class.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        public VarExpression(string name)
            => Name = name;

        /// <summary>
        /// Gets the value of the integer.
        /// </summary>
        public string Name { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Scope scope)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            if (scope is null)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            ushort index = scope.Get(Name);
            il.Emit(GetOpCode(index));
        }

        private static TypedOpCode GetOpCode(ushort index)
            => index switch
            {
                0 => TypedOpCodes.Ldloc0,
                1 => TypedOpCodes.Ldloc1,
                2 => TypedOpCodes.Ldloc2,
                3 => TypedOpCodes.Ldloc3,
                ushort x when x <= 255 => TypedOpCodes.LdlocS((byte)index),
                _ => TypedOpCodes.Ldloc(index),
            };
    }
}
