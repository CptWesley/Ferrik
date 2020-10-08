using System;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.Statements
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
            Value.Emit(il, scope);
            il.Emit(TypedOpCodes.Stloc(index));
        }
    }
}
