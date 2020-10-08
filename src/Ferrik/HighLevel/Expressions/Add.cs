using System;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.Expressions
{
    /// <summary>
    /// Represents an integer expression.
    /// </summary>
    public class Add : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public Add(Expression left, Expression right)
            => (Left, Right) = (left, right);

        /// <summary>
        /// Gets the expression at the left-hand side of the operation.
        /// </summary>
        public Expression Left { get; }

        /// <summary>
        /// Gets the expression at the right-hand side of the operation.
        /// </summary>
        public Expression Right { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Scope scope)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            Left.Emit(il, scope);
            Right.Emit(il, scope);
            il.Emit(TypedOpCodes.Add);
        }
    }
}
