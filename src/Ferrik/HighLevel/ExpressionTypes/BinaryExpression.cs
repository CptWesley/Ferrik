using System;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.ExpressionTypes
{
    /// <summary>
    /// Represents a binary expression.
    /// </summary>
    public class BinaryExpression : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryExpression"/> class.
        /// </summary>
        /// <param name="opCode">The IL instruction for performing the binary expression.</param>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public BinaryExpression(TypedOpCode opCode, Expression left, Expression right)
            => (OpCode, Left, Right) = (opCode, left, right);

        /// <summary>
        /// Gets the expression at the left-hand side of the operation.
        /// </summary>
        public Expression Left { get; }

        /// <summary>
        /// Gets the expression at the right-hand side of the operation.
        /// </summary>
        public Expression Right { get; }

        /// <summary>
        /// Gets the IL instruction.
        /// </summary>
        public TypedOpCode OpCode { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Scope scope)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            Left.Emit(il, scope);
            Right.Emit(il, scope);
            il.Emit(OpCode);
        }
    }

    /// <summary>
    /// Represents an addition expression.
    /// </summary>
    public class AddExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public AddExpression(Expression left, Expression right)
            : base(TypedOpCodes.Add, left, right)
        {
        }
    }

    /// <summary>
    /// Represents a multiplication expression.
    /// </summary>
    public class MulExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MulExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public MulExpression(Expression left, Expression right)
            : base(TypedOpCodes.Mul, left, right)
        {
        }
    }

    /// <summary>
    /// Represents a division expression.
    /// </summary>
    public class DivExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public DivExpression(Expression left, Expression right)
            : base(TypedOpCodes.Div, left, right)
        {
        }
    }

    /// <summary>
    /// Represents a modulo expression.
    /// </summary>
    public class ModExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public ModExpression(Expression left, Expression right)
            : base(TypedOpCodes.Rem, left, right)
        {
        }
    }
}
