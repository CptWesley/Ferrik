using System.Collections.Generic;

namespace Ferrik
{
    /// <summary>
    /// Base class for all statements.
    /// </summary>
    public abstract class Statement
    {
        /// <summary>
        /// Transforms the given statement to a series of IL instructions.
        /// </summary>
        /// <returns>A series of IL instructions representing the statement.</returns>
        public abstract IEnumerable<TypedOpCode> ToOpCodes();
    }
}
