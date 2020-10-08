using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Ferrik.Tests
{
    /// <summary>
    /// Provides helper functions for invoking things.
    /// </summary>
    public static class InvocationHelper
    {
        /// <summary>
        /// Invokes a method on a target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="mi">The method info.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The result of the invocation.</returns>
        public static object Invoke(this object target, MethodInfo mi, params object[] args)
        {
            Type t = target?.GetType();
            MethodInfo actualMethod = t?.GetMethod(mi?.Name, mi?.GetParameters()?.Select(x => x.ParameterType)?.ToArray());
            return actualMethod?.Invoke(target, args);
        }

        /// <summary>
        /// Creates a new instance from the given type builder.
        /// </summary>
        /// <param name="tb">The type builder.</param>
        /// <param name="args">The arguments for the constructor.</param>
        /// <returns>A new instance.</returns>
        public static object CreateInstance(this TypeBuilder tb, params object[] args)
            => Activator.CreateInstance(tb?.CreateType(), args);
    }
}
