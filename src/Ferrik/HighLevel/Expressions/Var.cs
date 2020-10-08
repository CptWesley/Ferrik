using System;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.Expressions
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
            il.Emit(TypedOpCodes.Ldloc(index));
        }
    }
}
