using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Ferrik.Example
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            AssemblyName an = new AssemblyName("DynamicAssembly");
            AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.RunAndCollect);
            ModuleBuilder mob = ab.DefineDynamicModule(an.Name);

            string typeName = "DynamicType";
            TypeBuilder tb = mob.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class);
            string methodName = "DynamicMethod";
            MethodBuilder mb = tb.DefineMethod(methodName, MethodAttributes.Public, typeof(int), null);
            ILGenerator il = mb.GetILGenerator();
            il.Emit(CreateBody());
            Type type = tb.CreateType();
            MethodInfo mi = type.GetMethod(methodName);
            object dynamicObject = Activator.CreateInstance(type);
            object result = mi.Invoke(dynamicObject, new object[0]);
            Console.WriteLine($"Result: {result}");
        }

        private static Statement CreateBody()
            => Statements.Builder()
            .Declare("a", typeof(int))
            .Declare("b", typeof(int))
            .Declare("c", typeof(int))
            .Assign("a", Expressions.Int(40))
            .Assign("b", Expressions.Int(2))
            .Assign("c", Expressions.Add(Expressions.Var("a"), Expressions.Var("b")))
            .Return(Expressions.Var("c"));
    }
}
