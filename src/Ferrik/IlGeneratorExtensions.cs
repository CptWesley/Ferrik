using System;
using System.Reflection.Emit;

namespace Ferrik
{
    /// <summary>
    /// Extension class for the <see cref="ILGenerator"/> class.
    /// </summary>
    public static class IlGeneratorExtensions
    {
        /// <summary>
        /// Emits a statement.
        /// </summary>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="statement">The statement.</param>
        public static void Emit(this ILGenerator il, Statement statement)
        {
            if (statement is null)
            {
                throw new ArgumentNullException(nameof(statement));
            }

            foreach (TypedOpCode opCode in statement.ToOpCodes())
            {
                il.Emit(opCode);
            }
        }

        /// <summary>
        /// Emits a strongly typed IL instructions.
        /// </summary>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="opCode">The typed instruction.</param>
        public static void Emit(this ILGenerator il, TypedOpCode opCode)
        {
            if (opCode is null)
            {
                throw new ArgumentNullException(nameof(opCode));
            }

            opCode.Emit(il);
        }
    }
}
