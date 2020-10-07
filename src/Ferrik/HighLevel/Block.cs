using System.Collections.Generic;
using System.Reflection.Emit;

namespace Ferrik.HighLevel
{
    /// <summary>
    /// Represents a statement block.
    /// </summary>
    public class Block : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// </summary>
        /// <param name="statements">The statements contained in the block.</param>
        public Block(IEnumerable<Statement> statements)
            => Statements = statements;

        /// <summary>
        /// Gets the statements contained in the block.
        /// </summary>
        public IEnumerable<Statement> Statements { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Dictionary<string, int> locals)
        {
            Dictionary<string, int> newLocals = new Dictionary<string, int>(locals);
            foreach (Statement statement in Statements)
            {
                statement.Emit(il, newLocals);
            }
        }
    }
}
