using System;
using System.Reflection.Emit;

namespace Ferrik.HighLevel.StatementTypes
{
    /// <summary>
    /// Represents a variable declaration.
    /// </summary>
    public class DeclareStatement : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareStatement"/> class.
        /// </summary>
        /// <param name="name">The name of the local variable.</param>
        /// <param name="type">The type of the local variable.</param>
        public DeclareStatement(string name, Type type)
            => (Name, Type) = (name, type);

        /// <summary>
        /// Gets the name of the declared local.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the declared local.
        /// </summary>
        public Type Type { get; }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Scope scope)
        {
            if (il is null)
            {
                throw new ArgumentNullException(nameof(il));
            }

            if (scope is null)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            LocalBuilder lb = il.DeclareLocal(Type);
            scope.Add(Name, lb.LocalIndex);
        }
    }
}
