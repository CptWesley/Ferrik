using System;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.StatementTypes
{
    /// <summary>
    /// Represents an if statement.
    /// </summary>
    public class WhileStatement : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WhileStatement"/> class.
        /// </summary>
        /// <param name="condition">The condition of the if statement.</param>
        /// <param name="body">The body.</param>
        public WhileStatement(Expression condition, Statement body)
            => (Condition, Body) = (condition, body);

        /// <summary>
        /// Gets the condition.
        /// </summary>
        public Expression Condition { get; }

        /// <summary>
        /// Gets the body of the then-clause.
        /// </summary>
        public Statement Body { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Scope scope)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            Label startLbl = il.DefineLabel();
            Label endLbl = il.DefineLabel();
            il.MarkLabel(startLbl);
            Condition.Emit(il, scope);
            il.Emit(TypedOpCodes.Brfalse(endLbl));
            Body.Emit(il, scope);
            il.Emit(TypedOpCodes.Br(startLbl));
            il.MarkLabel(endLbl);
        }
    }
}
