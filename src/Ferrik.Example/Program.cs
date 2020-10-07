using System;
using System.Reflection;
using System.Reflection.Emit;
using Ferrik.HighLevel;

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
        {
            Statement dec1 = new Declare("a", typeof(int));
            Statement dec2 = new Declare("b", typeof(int));
            Statement dec3 = new Declare("c", typeof(int));
            Statement ass1 = new Assign("a", new Int(40));
            Statement ass2 = new Assign("b", new Int(2));
            Statement ass3 = new Assign("c", new Add(new Var("a"), new Var("b")));
            Statement ret = new Return(new Var("c"));
            Statement block = new Block(new[] {
                dec1,
                dec2,
                dec3,
                ass1,
                ass2,
                ass3,
                ret
            });

            return block;
        }
    }
}
