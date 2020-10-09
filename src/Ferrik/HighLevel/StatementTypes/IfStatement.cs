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
        /// <param name="else">The else body.</param>
        public IfStatement(Expression condition, Statement then, Statement @else)
            => (Condition, Then, Else) = (condition, then, @else);

        /// <summary>
        /// Initializes a new instance of the <see cref="IfStatement"/> class.
        /// </summary>
        /// <param name="condition">The condition of the if statement.</param>
        /// <param name="then">The then body.</param>
        public IfStatement(Expression condition, Statement then)
            : this(condition, then, null)
        {
        }

        /// <summary>
        /// Gets the condition.
        /// </summary>
        public Expression Condition { get; }

        /// <summary>
        /// Gets the body of the then-clause.
        /// </summary>
        public Statement Then { get; }

        /// <summary>
        /// Gets the body of the else-clause.
        /// </summary>
        public Statement Else { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Scope scope)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            if (Else is null)
            {
                Label lbl = il.DefineLabel();
                Condition.Emit(il, scope);
                il.Emit(TypedOpCodes.Brfalse(lbl));
                Then.Emit(il, scope);
                il.MarkLabel(lbl);
            }
            else
            {
                Label elseLbl = il.DefineLabel();
                Label endLbl = il.DefineLabel();
                Condition.Emit(il, scope);
                il.Emit(TypedOpCodes.Brfalse(elseLbl));
                Then.Emit(il, scope);
                il.Emit(TypedOpCodes.Br(endLbl));
                il.MarkLabel(elseLbl);
                Else.Emit(il, scope);
                il.MarkLabel(endLbl);
            }
        }
    }
}
