using System.Reflection.Emit;

namespace Ferrik
{
    /// <summary>
    /// Base class for strongly typed IL instructors.
    /// </summary>
    public abstract class TypedOpCode
    {
        /// <summary>
        /// Emits itself on the given IL generator.
        /// </summary>
        /// <param name="il">The given IL generator.</param>
        public abstract void Emit(ILGenerator il);
    }
}
