using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.StatementTypes
{
    /// <summary>
    /// Represents a statement block.
    /// </summary>
    public class BlockStatement : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlockStatement"/> class.
        /// </summary>
        /// <param name="statements">The statements contained in the block.</param>
        public BlockStatement(IEnumerable<Statement> statements)
            => Statements = statements;

        /// <summary>
        /// Gets the statements contained in the block.
        /// </summary>
        public IEnumerable<Statement> Statements { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Scope scope)
        {
            if (scope is null)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            Scope newScope = scope.CreateChild();
            foreach (Statement statement in Statements)
            {
                statement.Emit(il, newScope);
            }
        }
    }
}
