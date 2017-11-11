using System;
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
            unchecked
            {
                Assert.AreEqual((short)0x1100, BinaryBuffer.ReadShort(_buf, 0));
                Assert.AreEqual((short)0x0011, BinaryBuffer.ReadShortNetwork(_buf, 0));
                Assert.AreEqual((short)0x2211, BinaryBuffer.ReadShort(_buf, 1));
                Assert.AreEqual((short)0x1122, BinaryBuffer.ReadShortNetwork(_buf, 1));

                Assert.AreEqual((short)0x8877, BinaryBuffer.ReadShort(_buf, 7));
                Assert.AreEqual((short)0x7788, BinaryBuffer.ReadShortNetwork(_buf, 7));
                Assert.AreEqual((short)0x9988, BinaryBuffer.ReadShort(_buf, 8));
                Assert.AreEqual((short)0x8899, BinaryBuffer.ReadShortNetwork(_buf, 8));

                Assert.AreEqual((ushort)0x1100, BinaryBuffer.ReadUShort(_buf, 0));
                Assert.AreEqual((ushort)0x0011, BinaryBuffer.ReadUShortNetwork(_buf, 0));
                Assert.AreEqual((ushort)0x2211, BinaryBuffer.ReadUShort(_buf, 1));
                Assert.AreEqual((ushort)0x1122, BinaryBuffer.ReadUShortNetwork(_buf, 1));

                Assert.AreEqual((ushort)0x8877, BinaryBuffer.ReadUShort(_buf, 7));
                Assert.AreEqual((ushort)0x7788, BinaryBuffer.ReadUShortNetwork(_buf, 7));
                Assert.AreEqual((ushort)0x9988, BinaryBuffer.ReadUShort(_buf, 8));
                Assert.AreEqual((ushort)0x8899, BinaryBuffer.ReadUShortNetwork(_buf, 8));
            }
        }

        [TestMethod]
        public void ReadInt()
        {
            unchecked
            {
                Assert.AreEqual((int)0x33221100, BinaryBuffer.ReadInt(_buf, 0));
                Assert.AreEqual((int)0x00112233, BinaryBuffer.ReadIntNetwork(_buf, 0));
                Assert.AreEqual((int)0x44332211, BinaryBuffer.ReadInt(_buf, 1));
                Assert.AreEqual((int)0x11223344, BinaryBuffer.ReadIntNetwork(_buf, 1));

                Assert.AreEqual((int)0x88776655, BinaryBuffer.ReadInt(_buf, 5));
                Assert.AreEqual((int)0x55667788, BinaryBuffer.ReadIntNetwork(_buf, 5));
                Assert.AreEqual((int)0x99887766, BinaryBuffer.ReadInt(_buf, 6));
                Assert.AreEqual((int)0x66778899, BinaryBuffer.ReadIntNetwork(_buf, 6));

                Assert.AreEqual((uint)0x33221100, BinaryBuffer.ReadUInt(_buf, 0));
                Assert.AreEqual((uint)0x00112233, BinaryBuffer.ReadUIntNetwork(_buf, 0));
                Assert.AreEqual((uint)0x44332211, BinaryBuffer.ReadUInt(_buf, 1));
                Assert.AreEqual((uint)0x11223344, BinaryBuffer.ReadUIntNetwork(_buf, 1));

                Assert.AreEqual((uint)0x88776655, BinaryBuffer.ReadUInt(_buf, 5));
                Assert.AreEqual((uint)0x55667788, BinaryBuffer.ReadUIntNetwork(_buf, 5));
                Assert.AreEqual((uint)0x99887766, BinaryBuffer.ReadUInt(_buf, 6));
                Assert.AreEqual((uint)0x66778899, BinaryBuffer.ReadUIntNetwork(_buf, 6));
            }
        }

        [TestMethod]
        public void ReadLong()
        {
            unchecked
            {
                Assert.AreEqual((long)0x7766554433221100, BinaryBuffer.ReadLong(_buf, 0));
                Assert.AreEqual((long)0x0011223344556677, BinaryBuffer.ReadLongNetwork(_buf, 0));
                Assert.AreEqual((long)0x8877665544332211, BinaryBuffer.ReadLong(_buf, 1));
                Assert.AreEqual((long)0x1122334455667788, BinaryBuffer.ReadLongNetwork(_buf, 1));

                Assert.AreEqual((ulong)0x7766554433221100, BinaryBuffer.ReadULong(_buf, 0));
                Assert.AreEqual((ulong)0x0011223344556677, BinaryBuffer.ReadULongNetwork(_buf, 0));
                Assert.AreEqual((ulong)0x8877665544332211, BinaryBuffer.ReadULong(_buf, 1));
                Assert.AreEqual((ulong)0x1122334455667788, BinaryBuffer.ReadULongNetwork(_buf, 1));
            }
        }
    }
}
