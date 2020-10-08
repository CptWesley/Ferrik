using System.Reflection.Emit;

namespace Ferrik.HighLevel.StatementTypes
{
    /// <summary>
    /// Represents a return statement.
    /// </summary>
    public class ReturnStatement : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnStatement"/> class.
        /// </summary>
        /// <param name="expression">The expression that needs to be returned.</param>
        public ReturnStatement(Expression expression)
            => Expression = expression;

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public Expression Expression { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Scope scope)
        {
            Expression.Emit(il, scope);
            il.Emit(TypedOpCodes.Ret);
        }
    }
}
