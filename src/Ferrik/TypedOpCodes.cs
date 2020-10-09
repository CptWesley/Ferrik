using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Ferrik
{
    /// <summary>
    /// Holds functions for creating opcodes.
    /// </summary>
    [SuppressMessage("Microsoft.Maintainability", "CA1506", Justification = "Other partial part is generated code.")]
    public static partial class TypedOpCodes
    {
        /// <summary>
        /// Parses a collection of bytes to typed op codes.
        /// </summary>
        /// <param name="instructions">IL instructions as bytes.</param>
        /// <returns>A series of typed op codes.</returns>
        public static IEnumerable<TypedOpCode> Parse(IEnumerable<byte> instructions)
        {
            if (!instructions.Any())
            {
                return Array.Empty<TypedOpCode>();
            }

            return instructions.First() switch
            {
                0x58 => Add.PrependTo(instructions, 1),
                0xD6 => AddOvf.PrependTo(instructions, 1),
                0xD7 => AddOvfUn.PrependTo(instructions, 1),
                0x5F => And.PrependTo(instructions, 1),
                0x3B => Beq(instructions.Skip(1).ToInt()).PrependTo(instructions, 5),
                0x2E => BeqS(instructions.Skip(1).ToSByte()).PrependTo(instructions, 2),
                0x3C => Bge(instructions.Skip(1).ToInt()).PrependTo(instructions, 5),
                0x2F => BgeS(instructions.Skip(1).ToSByte()).PrependTo(instructions, 2),
                0x41 => BgeUn(instructions.Skip(1).ToInt()).PrependTo(instructions, 5),
                0x34 => BgeUnS(instructions.Skip(1).ToSByte()).PrependTo(instructions, 2),
                0x3D => Bgt(instructions.Skip(1).ToInt()).PrependTo(instructions, 5),
                0x30 => BgtS(instructions.Skip(1).ToSByte()).PrependTo(instructions, 2),
                0x42 => BgtUn(instructions.Skip(1).ToInt()).PrependTo(instructions, 5),
                0x35 => BgtUnS(instructions.Skip(1).ToSByte()).PrependTo(instructions, 2),
                0x3E => Blt(instructions.Skip(1).ToInt()).PrependTo(instructions, 5),
                0x31 => BltS(instructions.Skip(1).ToSByte()).PrependTo(instructions, 2),
                0x43 => BltUn(instructions.Skip(1).ToInt()).PrependTo(instructions, 5),
                0x36 => BltUnS(instructions.Skip(1).ToSByte()).PrependTo(instructions, 2),
                0x40 => BneUn(instructions.Skip(1).ToInt()).PrependTo(instructions, 5),
                0x33 => BneUnS(instructions.Skip(1).ToSByte()).PrependTo(instructions, 2),

                // 0x8c => ???
                0x38 => Br(instructions.Skip(1).ToInt()).PrependTo(instructions, 5),
                0x2B => BrS(instructions.Skip(1).ToSByte()).PrependTo(instructions, 2),
                0x01 => Break.PrependTo(instructions, 1),
                0x39 => Brfalse(instructions.Skip(1).ToInt()).PrependTo(instructions, 5),
                0x2C => BrfalseS(instructions.Skip(1).ToSByte()).PrependTo(instructions, 2),
                0x3A => Brtrue(instructions.Skip(1).ToInt()).PrependTo(instructions, 5),
                0x2D => BrtrueS(instructions.Skip(1).ToSByte()).PrependTo(instructions, 2),

                // 0x28 => ???
                // 0x29 => ???
                // 0x6F => ???
                // 0x74 => ???
                0xD3 => ConvI.PrependTo(instructions, 1),
                0x67 => ConvI1.PrependTo(instructions, 1),
                0x68 => ConvI2.PrependTo(instructions, 1),
                0x69 => ConvI4.PrependTo(instructions, 1),
                0x6A => ConvI8.PrependTo(instructions, 1),
                0xD4 => ConvOvfI.PrependTo(instructions, 1),
                0x8A => ConvOvfIUn.PrependTo(instructions, 1),
                0xB3 => ConvOvfI1.PrependTo(instructions, 1),
                0x82 => ConvOvfI1Un.PrependTo(instructions, 1),
                0xB5 => ConvOvfI2.PrependTo(instructions, 1),
                0x83 => ConvOvfI2Un.PrependTo(instructions, 1),
                0xB7 => ConvOvfI4.PrependTo(instructions, 1),
                0x84 => ConvOvfI4Un.PrependTo(instructions, 1),
                0xB9 => ConvOvfI8.PrependTo(instructions, 1),
                0x85 => ConvOvfI8Un.PrependTo(instructions, 1),
                0xD5 => ConvOvfU.PrependTo(instructions, 1),
                0x8B => ConvOvfUUn.PrependTo(instructions, 1),
                0xB4 => ConvOvfU1.PrependTo(instructions, 1),
                0x86 => ConvOvfU1Un.PrependTo(instructions, 1),
                0xB6 => ConvOvfU2.PrependTo(instructions, 1),
                0x87 => ConvOvfU2Un.PrependTo(instructions, 1),
                0xB8 => ConvOvfU4.PrependTo(instructions, 1),
                0x88 => ConvOvfU4Un.PrependTo(instructions, 1),
                0xBA => ConvOvfU8.PrependTo(instructions, 1),
                0x89 => ConvOvfU8Un.PrependTo(instructions, 1),
                0x76 => ConvRUn.PrependTo(instructions, 1),
                0x6B => ConvR4.PrependTo(instructions, 1),
                0x6C => ConvR8.PrependTo(instructions, 1),
                0xE0 => ConvU.PrependTo(instructions, 1),
                0xD2 => ConvU1.PrependTo(instructions, 1),
                0xD1 => ConvU2.PrependTo(instructions, 1),
                0x6D => ConvU4.PrependTo(instructions, 1),
                0x6E => ConvU8.PrependTo(instructions, 1),

                // 0x70 => ???
                0x5B => Div.PrependTo(instructions, 1),
                0x5C => DivUn.PrependTo(instructions, 1),
                0x25 => Dup.PrependTo(instructions, 1),
                0xDC => Endfinally.PrependTo(instructions, 2),

                // 0x75 => ???
                // 0x27 => ???
                0x02 => Ldarg0.PrependTo(instructions, 1),
                0x03 => Ldarg1.PrependTo(instructions, 1),
                0x04 => Ldarg2.PrependTo(instructions, 1),
                0x05 => Ldarg3.PrependTo(instructions, 1),
                0x0E => LdargS(instructions.Skip(1).ToByte()).PrependTo(instructions, 2),
                0x0F => LdargaS(instructions.Skip(1).ToByte()).PrependTo(instructions, 2),
                0x20 => LdcI4(instructions.Skip(1).ToInt()).PrependTo(instructions, 5),
                0x16 => LdcI4_0.PrependTo(instructions, 1),
                0x17 => LdcI4_1.PrependTo(instructions, 1),
                0x18 => LdcI4_2.PrependTo(instructions, 1),
                0x19 => LdcI4_3.PrependTo(instructions, 1),
                0x1A => LdcI4_4.PrependTo(instructions, 1),
                0x1B => LdcI4_5.PrependTo(instructions, 1),
                0x1C => LdcI4_6.PrependTo(instructions, 1),
                0x1D => LdcI4_7.PrependTo(instructions, 1),
                0x1E => LdcI4_8.PrependTo(instructions, 1),
                0x15 => LdcI4M1.PrependTo(instructions, 1),
                0x1F => LdcI4S(instructions.Skip(1).ToSByte()).PrependTo(instructions, 2),
                0x21 => LdcI8(instructions.Skip(1).ToLong()).PrependTo(instructions, 9),
                0x22 => LdcR4(instructions.Skip(1).ToFloat()).PrependTo(instructions, 5),
                0x23 => LdcR8(instructions.Skip(1).ToDouble()).PrependTo(instructions, 9),

                // 0xA3 => ???
                0x2A => Ret.PrependTo(instructions, 1),

                0xFE => instructions.Skip(1).First() switch
                {
                    0x00 => Arglist.PrependTo(instructions, 2),
                    0x01 => Ceq.PrependTo(instructions, 2),
                    0x02 => Cgt.PrependTo(instructions, 2),
                    0x03 => CgtUn.PrependTo(instructions, 2),
                    0x04 => Clt.PrependTo(instructions, 2),
                    0x05 => CltUn.PrependTo(instructions, 2),

                    // 0x16 => ???
                    0x17 => Cpblk.PrependTo(instructions, 2),
                    0x11 => Endfilter.PrependTo(instructions, 2),
                    0x18 => Initblk.PrependTo(instructions, 2),

                    // 0x15 => ???
                    0x09 => Ldarg(instructions.Skip(1).ToUShort()).PrependTo(instructions, 4),
                    0x0A => Ldarga(instructions.Skip(1).ToUShort()).PrependTo(instructions, 4),
                    byte opcode => throw new ArgumentException($"Unknown opcode: 0xFE {opcode.ToString("X", CultureInfo.InvariantCulture)}"),
                },
                byte opcode => throw new ArgumentException($"Unknown opcode: {opcode.ToString("X", CultureInfo.InvariantCulture)}"),
            };
        }

        private static byte[] GetByteArray(this IEnumerable<byte> bytes, int count)
        {
            byte[] arr = bytes.Take(count).ToArray();
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(arr);
            }

            return arr;
        }

        private static int ToInt(this IEnumerable<byte> bytes)
            => BitConverter.ToInt32(bytes.GetByteArray(4), 0);

        private static long ToLong(this IEnumerable<byte> bytes)
            => BitConverter.ToInt64(bytes.GetByteArray(8), 0);

        private static float ToFloat(this IEnumerable<byte> bytes)
            => BitConverter.ToSingle(bytes.GetByteArray(4), 0);

        private static double ToDouble(this IEnumerable<byte> bytes)
            => BitConverter.ToDouble(bytes.GetByteArray(8), 0);

        private static ushort ToUShort(this IEnumerable<byte> bytes)
            => BitConverter.ToUInt16(bytes.GetByteArray(2), 0);

        private static byte ToByte(this IEnumerable<byte> bytes)
            => bytes.First();

        private static sbyte ToSByte(this IEnumerable<byte> bytes)
            => (sbyte)bytes.ToByte();

        private static IEnumerable<TypedOpCode> PrependTo(this TypedOpCode opCode, IEnumerable<byte> tail, int skips)
            => new[] { opCode }.Concat(Parse(tail.Skip(skips)));
    }
}
