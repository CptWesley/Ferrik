using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Ferrik
{
    /// <summary>
    /// Static helper class for defining dynamic types.
    /// </summary>
    public static class DynamicTypes
    {
        /// <summary>
        /// Defines a new dynamic type.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="typeAttributes">The attributes of the type.</param>
        /// <param name="parent">The parent class.</param>
        /// <param name="interfaces">The interfaces.</param>
        /// <returns>The typebuilder belonging to the type.</returns>
        public static TypeBuilder Define(string name, TypeAttributes typeAttributes, Type parent, Type[] interfaces)
        {
            AssemblyName an = new AssemblyName(Guid.NewGuid().ToString());
            AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.RunAndCollect);
            ModuleBuilder mb = ab.DefineDynamicModule(an.Name);
            return mb.DefineType(name, typeAttributes, parent, interfaces);
        }

        /// <summary>
        /// Defines a new dynamic type.
        /// </summary>
        /// <param name="typeAttributes">The attributes of the type.</param>
        /// <param name="parent">The parent class.</param>
        /// <param name="interfaces">The interfaces.</param>
        /// <returns>The typebuilder belonging to the type.</returns>
        public static TypeBuilder Define(TypeAttributes typeAttributes, Type parent, Type[] interfaces)
            => Define(Guid.NewGuid().ToString(), typeAttributes, parent, interfaces);

        /// <summary>
        /// Defines a new dynamic type.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="typeAttributes">The attributes of the type.</param>
        /// <param name="parent">The parent class.</param>
        /// <returns>The typebuilder belonging to the type.</returns>
        public static TypeBuilder Define(string name, TypeAttributes typeAttributes, Type parent)
            => Define(name, typeAttributes, parent, null);

        /// <summary>
        /// Defines a new dynamic type.
        /// </summary>
        /// <param name="typeAttributes">The attributes of the type.</param>
        /// <param name="parent">The parent class.</param>
        /// <returns>The typebuilder belonging to the type.</returns>
        public static TypeBuilder Define(TypeAttributes typeAttributes, Type parent)
            => Define(Guid.NewGuid().ToString(), typeAttributes, parent, null);

        /// <summary>
        /// Defines a new dynamic type.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="typeAttributes">The attributes of the type.</param>
        /// <returns>The typebuilder belonging to the type.</returns>
        public static TypeBuilder Define(string name, TypeAttributes typeAttributes)
            => Define(name, typeAttributes, null, null);

        /// <summary>
        /// Defines a new dynamic type.
        /// </summary>
        /// <param name="typeAttributes">The attributes of the type.</param>
        /// <returns>The typebuilder belonging to the type.</returns>
        public static TypeBuilder Define(TypeAttributes typeAttributes)
            => Define(Guid.NewGuid().ToString(), typeAttributes, null, null);

        /// <summary>
        /// Defines a new dynamic type.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <returns>The typebuilder belonging to the type.</returns>
        public static TypeBuilder Define(string name)
            => Define(name, 0, null, null);

        /// <summary>
        /// Defines a new dynamic type.
        /// </summary>
        /// <returns>The typebuilder belonging to the type.</returns>
        public static TypeBuilder Define()
            => Define(Guid.NewGuid().ToString(), 0, null, null);

        /// <summary>
        /// Defines a new dynamic type.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="parent">The parent class.</param>
        /// <param name="interfaces">The interfaces.</param>
        /// <returns>The typebuilder belonging to the type.</returns>
        public static TypeBuilder Define(string name, Type parent, Type[] interfaces)
            => Define(name, 0, parent, interfaces);

        /// <summary>
        /// Defines a new dynamic type.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="parent">The parent class.</param>
        /// <returns>The typebuilder belonging to the type.</returns>
        public static TypeBuilder Define(string name, Type parent)
            => Define(name, 0, parent, null);

        /// <summary>
        /// Defines a new dynamic type.
        /// </summary>
        /// <param name="parent">The parent class.</param>
        /// <param name="interfaces">The interfaces.</param>
        /// <returns>The typebuilder belonging to the type.</returns>
        public static TypeBuilder Define(Type parent, Type[] interfaces)
            => Define(Guid.NewGuid().ToString(), 0, parent, interfaces);

        /// <summary>
        /// Defines a new dynamic type.
        /// </summary>
        /// <param name="parent">The parent class.</param>
        /// <returns>The typebuilder belonging to the type.</returns>
        public static TypeBuilder Define(Type parent)
            => Define(Guid.NewGuid().ToString(), 0, parent, null);
    }
}
