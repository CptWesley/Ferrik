using System.Collections.Generic;
using System.Reflection.Emit;
using Ferrik.LowLevel;

namespace Ferrik.HighLevel
{
    /// <summary>
    /// Represents a return statement.
    /// </summary>
    public class Return : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Return"/> class.
        /// </summary>
        /// <param name="expression">The expression that needs to be returned.</param>
        public Return(Expression expression)
            => Expression = expression;

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public Expression Expression { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Dictionary<string, int> locals)
        {
            Expression.Emit(il, locals);
            il.Emit(new RetOpCode());
        }
    }
}
