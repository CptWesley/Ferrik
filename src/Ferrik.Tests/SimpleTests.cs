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
            Statement body = Statements.Return(Expressions.Add(Expressions.Arg(1), Expressions.Arg(2)));
            il.Emit(body);
            object dynamicObject = tb.CreateInstance();
            AssertThat(dynamicObject.Invoke(mb, 3, 4)).IsEqualTo(7);
        }

        /// <summary>
        /// Checks that a simple if works correctly.
        /// </summary>
        [Fact]
        public static void If()
        {
            TypeBuilder tb = DynamicTypes.Define();
            string methodName = "DynamicMethod";
            MethodBuilder mb = tb.DefineMethod(methodName, MethodAttributes.Public, typeof(bool), new[] { typeof(int) });
            ILGenerator il = mb.GetILGenerator();
            Statement body = Statements.Builder()
                .If(Expressions.LesserThan(Expressions.Arg(1), Expressions.Int(42)), Statements.Return(Expressions.False()))
                .Return(Expressions.True());
            il.Emit(body);
            object dynamicObject = tb.CreateInstance();
            AssertThat(dynamicObject.Invoke(mb, 40)).IsEqualTo(false);
            AssertThat(dynamicObject.Invoke(mb, 80)).IsEqualTo(true);
        }

        /// <summary>
        /// Checks that a simple if works correctly.
        /// </summary>
        [Fact]
        public static void IfElse()
        {
            TypeBuilder tb = DynamicTypes.Define();
            string methodName = "DynamicMethod";
            MethodBuilder mb = tb.DefineMethod(methodName, MethodAttributes.Public, typeof(int), new[] { typeof(bool) });
            ILGenerator il = mb.GetILGenerator();
            Statement body = Statements.Builder()
                .Declare("x", typeof(int))
                .Assign("x", Expressions.Int(69))
                .If(Expressions.Arg(1), Statements.Assign("x", Expressions.Subtract(Expressions.Var("x"), Expressions.Int(27))), Statements.Assign("x", Expressions.Add(Expressions.Var("x"), Expressions.Int(1268))))
                .Return(Expressions.Var("x"));
            il.Emit(body);
            object dynamicObject = tb.CreateInstance();
            AssertThat(dynamicObject.Invoke(mb, true)).IsEqualTo(42);
            AssertThat(dynamicObject.Invoke(mb, false)).IsEqualTo(1337);
        }

        /// <summary>
        /// Checks that a simple while works correctly.
        /// </summary>
        [Fact]
        public static void While()
        {
            TypeBuilder tb = DynamicTypes.Define();
            string methodName = "DynamicMethod";
            MethodBuilder mb = tb.DefineMethod(methodName, MethodAttributes.Public, typeof(int), new[] { typeof(int) });
            ILGenerator il = mb.GetILGenerator();
            Statement body = Statements.Builder()
                .Declare("x", typeof(int))
                .Assign("x", Expressions.Int(0))
                .While(Expressions.GreaterThan(Expressions.Arg(1), Expressions.Int(0)), Statements.Builder()
                    .Assign("x", Expressions.Add(Expressions.Var("x"), Expressions.Int(2)))
                    .Assign(1, Expressions.Subtract(Expressions.Arg(1), Expressions.Int(1))))
                .Return(Expressions.Var("x"));
            il.Emit(body);
            object dynamicObject = tb.CreateInstance();
            AssertThat(dynamicObject.Invoke(mb, 40)).IsEqualTo(80);
            AssertThat(dynamicObject.Invoke(mb, 2)).IsEqualTo(4);
        }
    }
}
