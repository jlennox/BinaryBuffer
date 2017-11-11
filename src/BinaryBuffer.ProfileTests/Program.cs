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

        private static void Main(string[] args)
        {
            Console.WriteLine("Read tests:");
            Time(ReadInt);
            Time(ReadIntUnsafe);
            Time(ReadIntUnsafePtr);
            Time(ReadIntUnsafeVar);
            Time(BitConverter.ToInt32);
            Time(ToInt32);
            Console.WriteLine("Write tests:");
            Time(WriteIntUnsafe);
            Time(WriteIntUnsafeShifted);

            Console.ReadLine();
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
            return ((int)buffer[0] << 0) |
                ((int)buffer[1] << 8) |
                ((int)buffer[2] << 16) |
                ((int)buffer[3] << 24);
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

        static void Time(Func<byte[], int, int> a)
        {
            var buff = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 };

            for (var i = 0; i < _times / 2; i++)
            {
                a(buff, 2);
            }

            var watch = Stopwatch.StartNew();
            for (var i = 0; i < _times; i++)
            {
                a(buff, 2);
            }

            var length = watch.Elapsed;

            Console.WriteLine("{0}:\t {1}", a.Method.Name, length);
        }

        static void Time(Action<byte[], int, int> a)
        {
            var buff = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 };

            for (var i = 0; i < _times / 2; i++)
            {
                a(buff, 2, 0x22334455);
            }

            var watch = Stopwatch.StartNew();
            for (var i = 0; i < _times; i++)
            {
                a(buff, 2, 0x22334455);
            }

            var length = watch.Elapsed;

            Console.WriteLine("{0}:\t {1}", a.Method.Name, length);
        }
    }
}
