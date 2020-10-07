using System.Collections.Generic;

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
        public override IEnumerable<TypedOpCode> ToOpCodes()
        {
            foreach (Statement statement in Statements)
            {
                foreach (TypedOpCode opCode in statement.ToOpCodes())
                {
                    yield return opCode;
                }
            }
        }
    }
}
