using System;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.StatementTypes
{
    /// <summary>
    /// Represents an if statement.
    /// </summary>
    public class IfStatement : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IfStatement"/> class.
        /// </summary>
        /// <param name="condition">The condition of the if statement.</param>
        /// <param name="then">The then body.</param>
        public IfStatement(Expression condition, Statement then)
            => (Condition, Then) = (condition, then);

        /// <summary>
        /// Gets the condition.
        /// </summary>
        public Expression Condition { get; }

        /// <summary>
        /// Gets the body.
        /// </summary>
        public Statement Then { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Scope scope)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            Label lbl = il.DefineLabel();
            Condition.Emit(il, scope);
            il.Emit(TypedOpCodes.Brfalse(lbl));
            Then.Emit(il, scope);
            il.MarkLabel(lbl);
        }
    }
}
