using System;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

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
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            if (statement is null)
            {
                throw new ArgumentNullException(nameof(statement));
            }

            statement.Emit(il);
        }

        /// <summary>
        /// Emits a strongly typed IL instructions.
        /// </summary>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="opCode">The typed instruction.</param>
        public static void Emit(this ILGenerator il, TypedOpCode opCode)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            if (opCode is null)
            {
                throw new ArgumentNullException(nameof(opCode));
            }

            Console.WriteLine(opCode);
            opCode.Emit(il);
        }

        /// <summary>
        /// Emits instructions to load a reference to an object on the stack.
        /// </summary>
        /// <typeparam name="T">Type of the object passed.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="obj">The object of which to emit a reference to.</param>
        /// <returns>The GCHandle to the object.</returns>
        public static GCHandle EmitLdRef<T>(this ILGenerator il, T obj)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            GCHandle handle = GCHandle.Alloc(obj);
            IntPtr ptr = GCHandle.ToIntPtr(handle);

            if (IntPtr.Size == 4)
            {
                il.Emit(OpCodes.Ldc_I4, ptr.ToInt32());
            }
            else
            {
                il.Emit(OpCodes.Ldc_I8, ptr.ToInt64());
            }

            il.Emit(OpCodes.Ldobj, typeof(T));

            return handle;
        }
    }
}
