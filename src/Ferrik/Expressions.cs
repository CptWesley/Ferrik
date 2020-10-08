using System.Diagnostics.CodeAnalysis;
using Ferrik.HighLevel.ExpressionTypes;

namespace Ferrik
{
    /// <summary>
    /// Defines an easy to use API for dealing with expressions.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1724", Justification = "Simplicity.")]
    public static class Expressions
    {
        /// <summary>
        /// Creates a new integer expression.
        /// </summary>
        /// <param name="value">The value of the integer.</param>
        /// <returns>The new expression.</returns>
        public static Expression Int(int value)
            => new IntExpression(value);

        /// <summary>
        /// Creates a new variable expression.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <returns>The new expression.</returns>
        public static Expression Var(string name)
            => new VarExpression(name);

        /// <summary>
        /// Creates a new variable expression from an argument.
        /// </summary>
        /// <param name="index">The index of the argument.</param>
        /// <returns>The new expression.</returns>
        public static Expression Arg(int index)
            => new VarExpression("@" + index);

        /// <summary>
        /// Creates a new addition expression.
        /// </summary>
        /// <param name="left">The left-hand side of the operation.</param>
        /// <param name="right">The right-hand side of the operation.</param>
        /// <returns>The new expression.</returns>
        public static Expression Add(Expression left, Expression right)
            => new AddExpression(left, right);

        /// <summary>
        /// Creates a new subtraction expression.
        /// </summary>
        /// <param name="left">The left-hand side of the operation.</param>
        /// <param name="right">The right-hand side of the operation.</param>
        /// <returns>The new expression.</returns>
        public static Expression Subtract(Expression left, Expression right)
            => new SubtractExpression(left, right);

        /// <summary>
        /// Creates a new multiplication expression.
        /// </summary>
        /// <param name="left">The left-hand side of the operation.</param>
        /// <param name="right">The right-hand side of the operation.</param>
        /// <returns>The new expression.</returns>
        public static Expression Multiply(Expression left, Expression right)
            => new MultiplyExpression(left, right);

        /// <summary>
        /// Creates a new division expression.
        /// </summary>
        /// <param name="left">The left-hand side of the operation.</param>
        /// <param name="right">The right-hand side of the operation.</param>
        /// <returns>The new expression.</returns>
        public static Expression Divide(Expression left, Expression right)
            => new DivideExpression(left, right);

        /// <summary>
        /// Creates a new modulo expression.
        /// </summary>
        /// <param name="left">The left-hand side of the operation.</param>
        /// <param name="right">The right-hand side of the operation.</param>
        /// <returns>The new expression.</returns>
        public static Expression Modulo(Expression left, Expression right)
            => new ModuloExpression(left, right);

        /// <summary>
        /// Creates a new and expression.
        /// </summary>
        /// <param name="left">The left-hand side of the operation.</param>
        /// <param name="right">The right-hand side of the operation.</param>
        /// <returns>The new expression.</returns>
        public static Expression And(Expression left, Expression right)
            => new AndExpression(left, right);

        /// <summary>
        /// Creates a new or expression.
        /// </summary>
        /// <param name="left">The left-hand side of the operation.</param>
        /// <param name="right">The right-hand side of the operation.</param>
        /// <returns>The new expression.</returns>
        public static Expression Or(Expression left, Expression right)
            => new OrExpression(left, right);

        /// <summary>
        /// Creates a new lesser-than expression.
        /// </summary>
        /// <param name="left">The left-hand side of the operation.</param>
        /// <param name="right">The right-hand side of the operation.</param>
        /// <returns>The new expression.</returns>
        public static Expression LesserThan(Expression left, Expression right)
            => new LesserThanExpression(left, right);

        /// <summary>
        /// Creates a new greater-than expression.
        /// </summary>
        /// <param name="left">The left-hand side of the operation.</param>
        /// <param name="right">The right-hand side of the operation.</param>
        /// <returns>The new expression.</returns>
        public static Expression GreaterThan(Expression left, Expression right)
            => new GreaterThanExpression(left, right);

        /// <summary>
        /// Creates a new equals expression.
        /// </summary>
        /// <param name="left">The left-hand side of the operation.</param>
        /// <param name="right">The right-hand side of the operation.</param>
        /// <returns>The new expression.</returns>
        public static Expression Equals(Expression left, Expression right)
            => new EqualsExpression(left, right);

        /// <summary>
        /// Creates a new boolean constant expression.
        /// </summary>
        /// <param name="value">The value of the boolean constant.</param>
        /// <returns>The new expression.</returns>
        public static Expression Bool(bool value)
            => new BoolExpression(value);

        /// <summary>
        /// Creates a new true expression.
        /// </summary>
        /// <returns>The new expression.</returns>
        public static Expression True()
            => Bool(true);

        /// <summary>
        /// Creates a new false expression.
        /// </summary>
        /// <returns>The new expression.</returns>
        public static Expression False()
            => Bool(false);
    }
}
