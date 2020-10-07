using System.Collections.Generic;
using System.Reflection.Emit;

namespace Ferrik
{
    /// <summary>
    /// Represents something that can be emited.
    /// </summary>
    public abstract class Emitable
    {
        /// <summary>
        /// Emits itself to the given IL generator.
        /// </summary>
        /// <param name="il">The given IL generator.</param>
        public void Emit(ILGenerator il)
            => Emit(il, new Dictionary<string, int>());

        /// <summary>
        /// Emits itself to the given IL generator.
        /// </summary>
        /// <param name="il">The given IL generator.</param>
        /// <param name="locals">The local variables in scope.</param>
        public abstract void Emit(ILGenerator il, Dictionary<string, int> locals);
    }
}
