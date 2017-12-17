using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryBuffer.Tests
{
    [TestClass]
    public class BufferReadingTests
    {
        private static readonly byte[] _buf = new byte[] {
            0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99
        };

        [TestMethod]
        public void ReadShort()
        {
            var test = new BufferTest(_buf);

            unchecked
            {
                test.AssertRead((short)0x1100, ReadMethod.Short, 0);
                test.AssertRead((short)0x0011, ReadMethod.ShortNetwork, 0);
                test.AssertRead((short)0x2211, ReadMethod.Short, 1);
                test.AssertRead((short)0x1122, ReadMethod.ShortNetwork, 1);

                test.AssertRead((short)0x8877, ReadMethod.Short, 7);
                test.AssertRead((short)0x7788, ReadMethod.ShortNetwork, 7);
                test.AssertRead((short)0x9988, ReadMethod.Short, 8);
                test.AssertRead((short)0x8899, ReadMethod.ShortNetwork, 8);

                test.AssertRead((ushort)0x1100, ReadMethod.UShort, 0);
                test.AssertRead((ushort)0x0011, ReadMethod.UShortNetwork, 0);
                test.AssertRead((ushort)0x2211, ReadMethod.UShort, 1);
                test.AssertRead((ushort)0x1122, ReadMethod.UShortNetwork, 1);

                test.AssertRead((ushort)0x8877, ReadMethod.UShort, 7);
                test.AssertRead((ushort)0x7788, ReadMethod.UShortNetwork, 7);
                test.AssertRead((ushort)0x9988, ReadMethod.UShort, 8);
                test.AssertRead((ushort)0x8899, ReadMethod.UShortNetwork, 8);
            }
        }

        [TestMethod]
        public void ReadInt()
        {
            var test = new BufferTest(_buf);

            unchecked
            {
                test.AssertRead((int)0x33221100, ReadMethod.Int, 0);
                test.AssertRead((int)0x00112233, ReadMethod.IntNetwork, 0);
                test.AssertRead((int)0x44332211, ReadMethod.Int, 1);
                test.AssertRead((int)0x11223344, ReadMethod.IntNetwork, 1);

                test.AssertRead((int)0x88776655, ReadMethod.Int, 5);
                test.AssertRead((int)0x55667788, ReadMethod.IntNetwork, 5);
                test.AssertRead((int)0x99887766, ReadMethod.Int, 6);
                test.AssertRead((int)0x66778899, ReadMethod.IntNetwork, 6);

                test.AssertRead((uint)0x33221100, ReadMethod.UInt, 0);
                test.AssertRead((uint)0x00112233, ReadMethod.UIntNetwork, 0);
                test.AssertRead((uint)0x44332211, ReadMethod.UInt, 1);
                test.AssertRead((uint)0x11223344, ReadMethod.UIntNetwork, 1);

                test.AssertRead((uint)0x88776655, ReadMethod.UInt, 5);
                test.AssertRead((uint)0x55667788, ReadMethod.UIntNetwork, 5);
                test.AssertRead((uint)0x99887766, ReadMethod.UInt, 6);
                test.AssertRead((uint)0x66778899, ReadMethod.UIntNetwork, 6);
            }
        }

        [TestMethod]
        public void ReadLong()
        {
            var test = new BufferTest(_buf);

            unchecked
            {
                test.AssertRead((long)0x7766554433221100, ReadMethod.Long, 0);
                test.AssertRead((long)0x0011223344556677, ReadMethod.LongNetwork, 0);
                test.AssertRead((long)0x8877665544332211, ReadMethod.Long, 1);
                test.AssertRead((long)0x1122334455667788, ReadMethod.LongNetwork, 1);

                test.AssertRead((ulong)0x7766554433221100, ReadMethod.ULong, 0);
                test.AssertRead((ulong)0x0011223344556677, ReadMethod.ULongNetwork, 0);
                test.AssertRead((ulong)0x8877665544332211, ReadMethod.ULong, 1);
                test.AssertRead((ulong)0x1122334455667788, ReadMethod.ULongNetwork, 1);
            }
        }
    }
}
