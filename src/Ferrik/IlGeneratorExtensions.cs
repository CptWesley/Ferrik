using System;
using System.Collections.Generic;
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
        /// Emits a strongly typed IL instruction.
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

            opCode.Emit(il);
        }

        /// <summary>
        /// Emits strongly typed IL instructions.
        /// </summary>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="opCodes">The typed instructions.</param>
        public static void Emit(this ILGenerator il, IEnumerable<TypedOpCode> opCodes)
        {
            if (opCodes is null)
            {
                throw new ArgumentNullException(nameof(opCodes));
            }

            foreach (TypedOpCode opCode in opCodes)
            {
                Emit(il, opCode);
            }
        }

        /// <summary>
        /// Emits strongly typed IL instructions.
        /// </summary>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="opCodes">The typed instructions.</param>
        public static void Emit(this ILGenerator il, params TypedOpCode[] opCodes)
            => Emit(il, opCodes as IEnumerable<TypedOpCode>);

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

        /// <summary>
        /// Emits the body of a delegate.
        /// </summary>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="delegate">The delegate to emit.</param>
        public static void Emit(this ILGenerator il, Delegate @delegate)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            byte[] bytes = @delegate.Method.GetMethodBody().GetILAsByteArray();
            Emit(il, TypedOpCodes.Parse(bytes));
        }

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
        /// <typeparam name="T13">The type of the thirtheenth parameter.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter.</typeparam>
        /// <typeparam name="T16">The type of the sixteenth parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this ILGenerator il, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
        /// <typeparam name="T13">The type of the thirtheenth parameter.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this ILGenerator il, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
        /// <typeparam name="T13">The type of the thirtheenth parameter.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this ILGenerator il, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
        /// <typeparam name="T13">The type of the thirtheenth parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this ILGenerator il, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this ILGenerator il, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this ILGenerator il, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this ILGenerator il, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this ILGenerator il, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8>(this ILGenerator il, Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7>(this ILGenerator il, Action<T1, T2, T3, T4, T5, T6, T7> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6>(this ILGenerator il, Action<T1, T2, T3, T4, T5, T6> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5>(this ILGenerator il, Action<T1, T2, T3, T4, T5> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3, T4>(this ILGenerator il, Action<T1, T2, T3, T4> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2, T3>(this ILGenerator il, Action<T1, T2, T3> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1, T2>(this ILGenerator il, Action<T1, T2> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit<T1>(this ILGenerator il, Action<T1> action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of an action.
        /// </summary>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="action">The action to emit.</param>
        public static void Emit(this ILGenerator il, Action action)
            => Emit(il, action as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
        /// <typeparam name="T13">The type of the thirtheenth parameter.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter.</typeparam>
        /// <typeparam name="T16">The type of the sixteenth parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
        /// <typeparam name="T13">The type of the thirtheenth parameter.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
        /// <typeparam name="T13">The type of the thirtheenth parameter.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
        /// <typeparam name="T13">The type of the thirtheenth parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, T7, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, T6, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, T5, T6, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, T5, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, T5, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, T4, TResult>(this ILGenerator il, Func<T1, T2, T3, T4, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, T3, TResult>(this ILGenerator il, Func<T1, T2, T3, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, T2, TResult>(this ILGenerator il, Func<T1, T2, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<T1, TResult>(this ILGenerator il, Func<T1, TResult> func)
            => Emit(il, func as Delegate);

        /// <summary>
        /// Emits the body of a function.
        /// </summary>
        /// <typeparam name="TResult">The result type of the function.</typeparam>
        /// <param name="il">The IL generator used for emitting.</param>
        /// <param name="func">The function to emit.</param>
        public static void Emit<TResult>(this ILGenerator il, Func<TResult> func)
            => Emit(il, func as Delegate);
    }
}
