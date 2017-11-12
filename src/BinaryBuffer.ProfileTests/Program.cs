using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryBuffer.ProfileTests
{
    // The code for BinaryBuffer is guided by the outcome of this profiling.
    unsafe class Program
    {
        private const int _times = 5000000;

        private static int _offset = 2;

        private static void Main(string[] args)
        {
            // Run ten times to improve consistency.
            for (var i = 0; i < 10; ++i)
            {
                // Some methods special case aligned access, in theory for
                // added speed.
                foreach (var offset in new[] { 2, 0 })
                {
                    _offset = offset;
                    var title = offset != 0
                        ? "Unaligned access:"
                        : "Aligned access:";

                    Console.WriteLine(title);
                    Console.WriteLine(new string('=', title.Length));
                    Console.WriteLine("Read tests:");
                    Console.WriteLine("-----------");
                    Time(ReadInt);
                    Time(ReadIntUnsafe);
                    Time(ReadMemory);
                    Time(ReadIntWithBitCheckUnsafe);
                    Time(ReadIntWithModCheckUnsafe);
                    Time(ReadIntUnsafePtr);
                    Time(ReadIntUnsafeVar);
                    Time(BitConverter.ToInt32);
                    Time(ToInt32);
                    Console.WriteLine("Write tests:");
                    Console.WriteLine("-----------");
                    Time(WriteIntUnsafe);
                    Time(WriteIntUnsafeShifted);
                    Console.WriteLine("Read network:");
                    Console.WriteLine("-----------");
                    Time(ReadNetworkIntUnsafe, false);
                    Time(ReadNetworkMemory, false);
                    Console.WriteLine();
                }
            }

            Console.ReadLine();
        }

        public static int ReadNetworkIntUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ((int)bufferPtr[0] << 24) |
                    ((int)bufferPtr[1] << 16) |
                    ((int)bufferPtr[2] << 8) |
                    ((int)bufferPtr[3] << 0);
            }
        }

        public static int ReadNetworkMemory(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                var result = *(int*)bufferPtr;
                return ((result/*  & 0x000000FF */) << 24) |
                    ((result & 0x0000FF00) << 8) |
                    ((result & 0x00FF0000) >> 8) |
                    ((int)(result/* & 0xFF000000*/) >> 24);
            }
        }

        public static void WriteIntUnsafe(
            byte[] buffer, int offset, int i)
        {
            var iPtr = (byte*)&i;
            fixed (byte* bufferPtr = &buffer[offset])
            {
                bufferPtr[0] = iPtr[0];
                bufferPtr[1] = iPtr[1];
                bufferPtr[2] = iPtr[2];
                bufferPtr[3] = iPtr[3];
            }
        }

        public static void WriteIntUnsafeShifted(
            byte[] buffer, int offset, int i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                bufferPtr[0] = (byte)((i >> 0) & 0xFF);
                bufferPtr[1] = (byte)((i >> 8) & 0xFF);
                bufferPtr[2] = (byte)((i >> 16) & 0xFF);
                bufferPtr[3] = (byte)((i >> 24) & 0xFF);
            }
        }

        public static int ReadInt(
            byte[] buffer, int offset)
        {
            return ((int)buffer[offset + 0] << 0) |
                ((int)buffer[offset + 1] << 8) |
                ((int)buffer[offset + 2] << 16) |
                ((int)buffer[offset + 3] << 24);
        }

        public static int ReadIntUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ((int)bufferPtr[0] << 0) |
                    ((int)bufferPtr[1] << 8) |
                    ((int)bufferPtr[2] << 16) |
                    ((int)bufferPtr[3] << 24);
            }
        }

        public static int ReadMemory(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return *(int*)bufferPtr;
            }
        }

        public static int ReadIntWithBitCheckUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                if ((offset & 0b11) == 0 && BitConverter.IsLittleEndian)
                {
                    return *(int*)bufferPtr;
                }

                return ((int)bufferPtr[0] << 0) |
                    ((int)bufferPtr[1] << 8) |
                    ((int)bufferPtr[2] << 16) |
                    ((int)bufferPtr[3] << 24);
            }
        }

        public static int ReadIntWithModCheckUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                if ((offset % 4) == 0 && BitConverter.IsLittleEndian)
                {
                    return *(int*)bufferPtr;
                }

                return ((int)bufferPtr[0] << 0) |
                    ((int)bufferPtr[1] << 8) |
                    ((int)bufferPtr[2] << 16) |
                    ((int)bufferPtr[3] << 24);
            }
        }

        public static int ReadIntUnsafePtr(
            byte[] buffer, int offset)
        {
            int result;
            var resultPtr = (byte*)&result;

            fixed (byte* bufferPtr = &buffer[offset])
            {
                resultPtr[0] = bufferPtr[0];
                resultPtr[1] = bufferPtr[1];
                resultPtr[2] = bufferPtr[2];
                resultPtr[3] = bufferPtr[3];
            }

            return result;
        }

        public static int ReadIntUnsafeVar(
            byte[] buffer, int offset)
        {
            int result;

            fixed (byte* bufferPtr = &buffer[offset])
            {
                result = bufferPtr[0] << 0;
                result |= bufferPtr[1] << 8;
                result |= bufferPtr[2] << 16;
                result |= bufferPtr[3] << 24;
            }

            return result;
        }

        // BitCovnerter.ToInt32 without the arguments check.
        public static int ToInt32(byte[] value, int startIndex)
        {
            fixed (byte* numPtr = &value[startIndex])
            {
                if (startIndex % 4 == 0)
                    return *(int*)numPtr;
                if (BitConverter.IsLittleEndian)
                    return (int)*numPtr | (int)numPtr[1] << 8 | (int)numPtr[2] << 16 | (int)numPtr[3] << 24;
                return (int)*numPtr << 24 | (int)numPtr[1] << 16 | (int)numPtr[2] << 8 | (int)numPtr[3];
            }
        }

        static void Time(Func<byte[], int, int> a, bool check = true)
        {
            var buff = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 };

            if (check)
            {
                var expected = BitConverter.ToInt32(buff, _offset);
                var actual = a(buff, _offset);

                if (expected != actual)
                {
                    throw new Exception($"Expected: {expected}, got: {actual}. Unable to test {a.Method.Name}");
                }
            }

            for (var i = 0; i < _times / 2; i++)
            {
                a(buff, _offset);
            }

            GC.TryStartNoGCRegion(0xFFFF);
            var watch = Stopwatch.StartNew();
            for (var i = 0; i < _times; i++)
            {
                a(buff, _offset);
            }
            var length = watch.Elapsed;
            GC.EndNoGCRegion();

            Console.WriteLine("{0,-25} {1}", a.Method.Name, length);
        }

        static void Time(Action<byte[], int, int> a)
        {
            var buff = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 };

            for (var i = 0; i < _times / 2; i++)
            {
                a(buff, _offset, 0x22334455);
            }

            GC.TryStartNoGCRegion(0xFFFF);
            var watch = Stopwatch.StartNew();
            for (var i = 0; i < _times; i++)
            {
                a(buff, _offset, 0x22334455);
            }
            var length = watch.Elapsed;
            GC.EndNoGCRegion();

            Console.WriteLine("{0,-25} {1}", a.Method.Name, length);
        }
    }
}
