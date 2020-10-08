﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ferrik.OpCodeClassGenerator
{
    [Generator]
    public class ClassGenerator : ISourceGenerator
    {
        private static readonly string Lines = @"
add
add.ovf
add.ovf.un
and
arglist
beq int
beq.s sbyte
bge int
bge.s sbyte
bge.un int
bge.un.s sbyte
bgt int
bgt.s sbyte
bgt.un int
bgt.un.s sbyte
ble int
ble.s sbyte
ble.un int
ble.un.s sbyte
blt int
blt.s sbyte
blt.un int
blt.un.s sbyte
bne.un int
bne.un.s sbyte
box System.Type
br int
br.s sbyte
break
brfalse int
brfalse.s sbyte
brtrue int
brtrue.s sbyte
call System.Reflection.MethodInfo
callvirt System.Reflection.MethodInfo
castclass System.Type
ceq
cgt
cgt.un
ckfinite
clt
clt.un
conv.i
conv.i1
conv.i2
conv.i4
conv.i8
conv.ovf.i
conv.ovf.i.un
conv.ovf.i1
conv.ovf.i1.un
conv.ovf.i2
conv.ovf.i2.un
conv.ovf.i4
conv.ovf.i4.un
conv.ovf.i8
conv.ovf.i8.un
conv.ovf.u1
conv.ovf.u1.un
conv.ovf.u2
conv.ovf.u2.un
conv.ovf.u4
conv.ovf.u4.un
conv.ovf.u8
conv.ovf.u8.un
conv.r.un
conv.r4
conv.r8
conv.u
conv.u1
conv.u2
conv.u4
conv.u8
cpblk
cpobj System.Type
div
div.un
dup
endfilter
endfinally
initblk
initobj System.Type
isinst System.Type
jmp System.Reflection.MethodInfo
ldarg ushort
ldarg.0
ldarg.1
ldarg.2
ldarg.3
ldarg.s byte
ldarga ushort
ldarga.s byte
ldc.i4 int
ldc.i4.0
ldc.i4.1
ldc.i4.2
ldc.i4.3
ldc.i4.4
ldc.i4.5
ldc.i4.6
ldc.i4.7
ldc.i4.8
ldc.i4.m1
ldc.i4.s sbyte
ldc.i8 long
ldc.r4 float
ldc.r8 double
ldelem System.Type
ldelem.i
ldelem.i1
ldelem.i2
ldelem.i4
ldelem.i8
ldelem.r4
ldelem.r8
ldelem.ref
ldelem.u1
ldelem.u2
ldelem.u4
ldelema System.Type
ldfld System.Reflection.FieldInfo
ldflda System.Reflection.FieldInfo
ldftn System.Reflection.MethodInfo
ldind.i
ldind.i1
ldind.i2
ldind.i4
ldind.i8
ldind.r4
ldind.r8
ldind.ref
ldind.u1
ldind.u2
ldind.u4
ldlen
ldloc ushort
ldloc.0
ldloc.1
ldloc.2
ldloc.3
ldloc.s byte
ldloca ushort
ldloca.s byte
ldnull
ldobj System.Type
ldsfld System.Reflection.FieldInfo
ldsflda System.Reflection.FieldInfo
ldstr string
ldvirtftn System.Reflection.MethodInfo
leave int
leave.s sbyte
localloc
mkrefany System.Type
mul
mul.ovf
mul.ovf.un
neg
newarr System.Type
newobj System.Reflection.ConstructorInfo
nop
not
or
pop
refanytype
refanyval System.Type
rem
rem.un
ret
rethrow
shl
shr
shr.un
sizeof System.Type
starg ushort
starg.s byte
stelem System.Type
stelem.i
stelem.i1
stelem.i2
stelem.i4
stelem.i8
stelem.r4
stelem.r8
stelem.ref
stfld System.Reflection.FieldInfo
stind.i
stind.i1
stind.i2
stind.i4
stind.i8
stind.r4
stind.r8
stind.ref
stloc ushort
stloc.0
stloc.1
stloc.2
stloc.3
stloc.s byte
stobj System.Type
stsfld System.Reflection.FieldInfo
sub
sub.ovf
sub.ovf.un
throw
unbox System.Type
unbox.any System.Type
xor
";

        public void Execute(GeneratorExecutionContext context)
        {
            IEnumerable<string> lines = Lines.Split('\n').Select(x => x.Replace("\r", "")).Where(x => !string.IsNullOrEmpty(x));
            string code = $"//<autogenerated>{Environment.NewLine}{string.Join(Environment.NewLine, lines.Select(ToClass))}{ToStaticClass(lines)}";
            context.AddSource("opcodes", SourceText.From(code, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // Do Nothing.
        }

        private static string ToStaticClass(IEnumerable<string> lines)
            => @$"
namespace Ferrik
{{
    /// <summary>
    /// Holds functions for creating opcodes.
    /// </summary>
    public static class TypedOpCodes
    {{
        {string.Join(Environment.NewLine, lines.Select(ToStaticField))}
        {string.Join(Environment.NewLine, lines.Select(ToStaticMethod))}
    }}
}}
";

        private static string ToStaticField(string line)
        {
            string[] parts = line.Split(' ');
            string instructionName = parts[0];
            string fieldName = ToClassName(instructionName);
            string[] args = parts.Skip(1).ToArray();

            if (args.Length > 0)
            {
                return string.Empty;
            }

            return @$"
        /// <summary>
        /// An object representing the {instructionName} instruction.
        /// </summary>
        public static readonly TypedOpCode {fieldName} = new Ferrik.LowLevel.{fieldName}OpCode();
";
        }

        private static string ToStaticMethod(string line)
        {
            string[] parts = line.Split(' ');
            string instructionName = parts[0];
            string methodName = ToClassName(instructionName);
            string[] args = parts.Skip(1).ToArray();

            if (args.Length == 0)
            {
                return string.Empty;
            }

            return @$"
        /// <summary>
        /// An object representing the {instructionName} instruction.
        /// </summary>
        public static TypedOpCode {methodName}({ToArguments(args)})
            => new Ferrik.LowLevel.{methodName}OpCode({string.Join(", ", args.Select((x, i) => $"arg{i + 1}"))});
";
        }

        private static string ToClass(string line)
        {
            string[] parts = line.Split(' ');
            string instructionName = parts[0];
            string className = ToClassName(instructionName) + "OpCode";
            string opCode = ToOpCodeName(instructionName);
            string[] args = parts.Skip(1).ToArray();

            return @$"
namespace Ferrik.LowLevel
{{
    /// <summary>
    /// Represents the {instructionName} instruction.
    /// </summary>
    public class {className} : TypedOpCode
    {{

        /// <summary>
        /// Initializes a new instance of the <see cref=""{className}""/> class.
        /// </summary>
        public {className}({ToArguments(args)})
        {{
{ToSetters(args.Length)}
        }}
        {ToProperties(args)}
        /// <inheritdoc/>
        public override void Emit(System.Reflection.Emit.ILGenerator il)
        {{
            if (il is null)
            {{
                throw new System.ArgumentNullException(nameof(il));
            }}
            
            il.Emit(System.Reflection.Emit.OpCodes.{opCode}{ToArguments(args.Length)});
        }}
        
        /// <inheritdoc/>
        public override string ToString()
            => ""{instructionName}""{ToToStringArguments(args.Length)};
    }}
}}
";
        }

        private static string ToArguments(string[] types)
            => string.Join(", ", types.Select((t, i) => $"{t} arg{i + 1}"));

        private static string ToSetters(int count)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                sb.AppendLine($"            Arg{i + 1} = arg{i + 1};");
            }

            return sb.ToString();
        }

        private static string ToProperties(string[] types)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < types.Length; i++)
            {
                sb.AppendLine($"/// <summary>Gets the argument at position {i + 1}.</summary>");
                sb.AppendLine($"public {types[i]} Arg{i + 1} {{ get; }}");
            }

            return sb.ToString();
        }

        private static string ToArguments(int count)
        {
            if (count == 0)
            {
                return string.Empty;
            }

            string[] args = new string[count];
            for (int i = 0; i < count; i++)
            {
                args[i] = $"Arg{i + 1}";
            }

            return ", " + string.Join(", ", args);
        }

        private static string ToToStringArguments(int count)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                sb.Append(" + \" \" + ");
                sb.Append($"Arg{i + 1}");
            }

            return sb.ToString();
        }

        private static string ToClassName(string instructionName)
        {
            string[] parts = instructionName.Split('.');
            StringBuilder sb = new StringBuilder();

            string previous = "X";
            for (int i = 0; i < parts.Length; i++)
            {
                string part = CapitalizeFirstLetter(parts[i]);
                if (LastCharIsNum(previous) && FirstCharIsNum(part))
                {
                    sb.Append("_");
                }

                sb.Append(part);
                previous = part;
            }

            return sb.ToString();
        }

        private static bool FirstCharIsNum(string x)
            => IsNum(x[0]);

        private static bool LastCharIsNum(string x)
            => IsNum(x[x.Length - 1]);

        private static bool IsNum(char c)
            => c >= '0' && c <= '9';

        private static string ToOpCodeName(string instructionName)
        {
            string[] parts = instructionName.Split('.');
            return string.Join("_", parts.Select(CapitalizeFirstLetter));
        }

        private static string CapitalizeFirstLetter(string str)
            => str.First().ToString().ToUpperInvariant() + str.Substring(1).ToLowerInvariant();
    }
}
