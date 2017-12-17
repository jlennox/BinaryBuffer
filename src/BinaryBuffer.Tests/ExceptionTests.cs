using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryBuffer.Tests
{
    [TestClass]
    public class ExceptionTests
    {
        [TestMethod]
        public void ThrowsWhenBufferNull()
        {
            ThrowsWhenBufferNull(() => BinaryBuffer.ReadShort(null, 0));
            ThrowsWhenBufferNull(() => BinaryBuffer.ReadInt(null, 0));
            ThrowsWhenBufferNull(() => BinaryBuffer.ReadLong(null, 0));
            ThrowsWhenBufferNull(() => BinaryBuffer.ReadShort(null, 0));
            ThrowsWhenBufferNull(() => BinaryBuffer.ReadUShort(null, 0));
            ThrowsWhenBufferNull(() => BinaryBuffer.ReadUInt(null, 0));
            ThrowsWhenBufferNull(() => BinaryBuffer.ReadULong(null, 0));
            ThrowsWhenBufferNull(() => BinaryBuffer.ReadUShort(null, 0));
            ThrowsWhenBufferNull(() => BinaryBuffer.WriteShort(null, 0, 1));
            ThrowsWhenBufferNull(() => BinaryBuffer.WriteInt(null, 0, 1));
            ThrowsWhenBufferNull(() => BinaryBuffer.WriteLong(null, 0, 1));
            ThrowsWhenBufferNull(() => BinaryBuffer.WriteShort(null, 0, 1));
            ThrowsWhenBufferNull(() => BinaryBuffer.WriteUShort(null, 0, 1));
            ThrowsWhenBufferNull(() => BinaryBuffer.WriteUInt(null, 0, 1));
            ThrowsWhenBufferNull(() => BinaryBuffer.WriteULong(null, 0, 1));
            ThrowsWhenBufferNull(() => BinaryBuffer.WriteUShort(null, 0, 1));

            ThrowsWhenBufferNull(() => new BinaryBuffer(null, 0));
            ThrowsWhenBufferNull(() => new BinaryBuffer(null, 1));
        }

        private static void ThrowsWhenBufferNull(Action a)
        {
            try
            {
                a();
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentNullException e)
                when (e.ParamName == "buffer")
            {
            }
        }

        [TestMethod]
        public void ThrowsWithNegativeOffset()
        {
            var buf = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            ThrowsWithNegativeOffset(() => BinaryBuffer.ReadShort(buf, -1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.ReadInt(buf, -1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.ReadLong(buf, -1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.ReadShort(buf, -1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.ReadUShort(buf, -1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.ReadUInt(buf, -1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.ReadULong(buf, -1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.ReadUShort(buf, -1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.WriteShort(buf, -1, 1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.WriteInt(buf, -1, 1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.WriteLong(buf, -1, 1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.WriteShort(buf, -1, 1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.WriteUShort(buf, -1, 1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.WriteUInt(buf, -1, 1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.WriteULong(buf, -1, 1));
            ThrowsWithNegativeOffset(() => BinaryBuffer.WriteUShort(buf, -1, 1));

            ThrowsWithNegativeOffset(() => new BinaryBuffer(buf, -1));
        }

        private static void ThrowsWithNegativeOffset(Action a)
        {
            try
            {
                a();
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentOutOfRangeException e)
                when (e.ParamName == "offset")
            {
            }
        }

        [TestMethod]
        public void ThrowsWhenOutOfRange()
        {
            var buf = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            ThrowsWhenOutOfRange(() => BinaryBuffer.ReadShort(buf, 100));
            ThrowsWhenOutOfRange(() => BinaryBuffer.ReadInt(buf, 100));
            ThrowsWhenOutOfRange(() => BinaryBuffer.ReadLong(buf, 100));
            ThrowsWhenOutOfRange(() => BinaryBuffer.ReadShort(buf, 100));
            ThrowsWhenOutOfRange(() => BinaryBuffer.ReadUShort(buf, 100));
            ThrowsWhenOutOfRange(() => BinaryBuffer.ReadUInt(buf, 100));
            ThrowsWhenOutOfRange(() => BinaryBuffer.ReadULong(buf, 100));
            ThrowsWhenOutOfRange(() => BinaryBuffer.ReadUShort(buf, 100));
            ThrowsWhenOutOfRange(() => BinaryBuffer.WriteShort(buf, 100, 1));
            ThrowsWhenOutOfRange(() => BinaryBuffer.WriteInt(buf, 100, 1));
            ThrowsWhenOutOfRange(() => BinaryBuffer.WriteLong(buf, 100, 1));
            ThrowsWhenOutOfRange(() => BinaryBuffer.WriteShort(buf, 100, 1));
            ThrowsWhenOutOfRange(() => BinaryBuffer.WriteUShort(buf, 100, 1));
            ThrowsWhenOutOfRange(() => BinaryBuffer.WriteUInt(buf, 100, 1));
            ThrowsWhenOutOfRange(() => BinaryBuffer.WriteULong(buf, 100, 1));
            ThrowsWhenOutOfRange(() => BinaryBuffer.WriteUShort(buf, 100, 1));
            ThrowsWhenOutOfRange(() => new BinaryBuffer(buf, 100));
        }

        private static void ThrowsWhenOutOfRange(Action a)
        {
            try
            {
                a();
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentException e)
                when (e.ParamName == "buffer")
            {
            }
        }
    }
}
