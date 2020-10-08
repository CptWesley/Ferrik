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
        /// Creates a new addition expression.
        /// </summary>
        /// <param name="left">The left-hand side of the operation.</param>
        /// <param name="right">The right-hand side of the operation.</param>
        /// <returns>The new expression.</returns>
        public static Expression Add(Expression left, Expression right)
            => new AddExpression(left, right);

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
