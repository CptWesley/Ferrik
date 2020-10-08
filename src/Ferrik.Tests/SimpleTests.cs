using System.Reflection;
using System.Reflection.Emit;
using Xunit;
using static AssertNet.Assertions;

namespace Ferrik.Tests
{
    /// <summary>
    /// Test class containing simple test cases.
    /// </summary>
    public static class SimpleTests
    {
        /// <summary>
        /// Checks that the example works correctly.
        /// </summary>
        [Fact]
        public static void Example()
        {
            TypeBuilder tb = DynamicTypes.Define();
            string methodName = "DynamicMethod";
            MethodBuilder mb = tb.DefineMethod(methodName, MethodAttributes.Public, typeof(int), null);
            ILGenerator il = mb.GetILGenerator();
            Statement body = Statements.Builder()
                .Declare("a", typeof(int))
                .Declare("b", typeof(int))
                .Declare("c", typeof(int))
                .Assign("a", Expressions.Int(40))
                .Assign("b", Expressions.Int(2))
                .Assign("c", Expressions.Add(Expressions.Var("a"), Expressions.Var("b")))
                .Return(Expressions.Var("c"));
            il.Emit(body);
            object dynamicObject = tb.CreateInstance();
            AssertThat(dynamicObject.Invoke(mb)).IsEqualTo(42);
        }

        /// <summary>
        /// Checks that a simple addition function works correctly.
        /// </summary>
        [Fact]
        public static void Add()
        {
            TypeBuilder tb = DynamicTypes.Define();
            string methodName = "DynamicMethod";
            MethodBuilder mb = tb.DefineMethod(methodName, MethodAttributes.Public, typeof(int), new[] { typeof(int), typeof(int) });
            ILGenerator il = mb.GetILGenerator();
            Statement body = Statements.Return(Expressions.Add(Expressions.Var("@1"), Expressions.Var("@2")));
            il.Emit(body);
            object dynamicObject = tb.CreateInstance();
            AssertThat(dynamicObject.Invoke(mb, 3, 4)).IsEqualTo(7);
        }
    }
}
