using System;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.StatementTypes
{
    /// <summary>
    /// Represents an assignment statement.
    /// </summary>
    public class AssignStatement : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignStatement"/> class.
        /// </summary>
        /// <param name="name">The name of the local variable.</param>
        /// <param name="value">The new value of the variable.</param>
        public AssignStatement(string name, Expression value)
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

            if (Name != null && Name.Length > 1 && Name[0] == '@' && ushort.TryParse(Name.Substring(1), out ushort paramIndex))
            {
                Value.Emit(il, scope);
                il.Emit(GetParamOpCode(paramIndex));
            }
            else
            {
                ushort index = scope.Get(Name);
                Value.Emit(il, scope);
                il.Emit(GetOpCode(index));
            }
        }

        private static TypedOpCode GetParamOpCode(ushort index)
            => index switch
            {
                ushort x when x <= 255 => TypedOpCodes.StargS((byte)index),
                _ => TypedOpCodes.Starg(index),
            };

        private static TypedOpCode GetOpCode(ushort index)
            => index switch
            {
                0 => TypedOpCodes.Stloc0,
                1 => TypedOpCodes.Stloc1,
                2 => TypedOpCodes.Stloc2,
                3 => TypedOpCodes.Stloc3,
                ushort x when x <= 255 => TypedOpCodes.StlocS((byte)index),
                _ => TypedOpCodes.Stloc(index),
            };
    }
}
