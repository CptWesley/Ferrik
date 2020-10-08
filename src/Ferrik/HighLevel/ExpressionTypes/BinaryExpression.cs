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
    /// Represents a subtraction expression.
    /// </summary>
    public class SubtractExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public SubtractExpression(Expression left, Expression right)
            : base(TypedOpCodes.Sub, left, right)
        {
        }
    }

    /// <summary>
    /// Represents a multiplication expression.
    /// </summary>
    public class MultiplyExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplyExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public MultiplyExpression(Expression left, Expression right)
            : base(TypedOpCodes.Mul, left, right)
        {
        }
    }

    /// <summary>
    /// Represents a division expression.
    /// </summary>
    public class DivideExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivideExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public DivideExpression(Expression left, Expression right)
            : base(TypedOpCodes.Div, left, right)
        {
        }
    }

    /// <summary>
    /// Represents a modulo expression.
    /// </summary>
    public class ModuloExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuloExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public ModuloExpression(Expression left, Expression right)
            : base(TypedOpCodes.Rem, left, right)
        {
        }
    }

    /// <summary>
    /// Represents a lesser-than expression.
    /// </summary>
    public class LesserThanExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LesserThanExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public LesserThanExpression(Expression left, Expression right)
            : base(TypedOpCodes.Clt, left, right)
        {
        }
    }

    /// <summary>
    /// Represents a greater-than expression.
    /// </summary>
    public class GreaterThanExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThanExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public GreaterThanExpression(Expression left, Expression right)
            : base(TypedOpCodes.Cgt, left, right)
        {
        }
    }

    /// <summary>
    /// Represents an equals expression.
    /// </summary>
    public class EqualsExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualsExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public EqualsExpression(Expression left, Expression right)
            : base(TypedOpCodes.Ceq, left, right)
        {
        }
    }

    /// <summary>
    /// Represents an and expression.
    /// </summary>
    public class AndExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public AndExpression(Expression left, Expression right)
            : base(TypedOpCodes.And, left, right)
        {
        }
    }

    /// <summary>
    /// Represents an or expression.
    /// </summary>
    public class OrExpression : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrExpression"/> class.
        /// </summary>
        /// <param name="left">The expression at the left-hand side of the operation.</param>
        /// <param name="right">The expression at the right-hand side of the operation.</param>
        public OrExpression(Expression left, Expression right)
            : base(TypedOpCodes.Or, left, right)
        {
        }
    }
}
