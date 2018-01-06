using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryBuffer.Tests
{
    [TestClass]
    public class SeekTests
    {
        private static void AssertSeek(
            ref BinaryBuffer bb, byte expected,
            int offset, SeekOrigin origin)
        {
            bb.Seek(offset, origin);
            Assert.AreEqual(expected, bb.Buffer[bb.Offset]);
        }

        private static void AssertOffset(
            ref BinaryBuffer bb, byte expected,
            int offset)
        {
            bb.Offset = offset;
            Assert.AreEqual(expected, bb.Buffer[bb.Offset]);
        }

        private static void AssertBounds(
            ref BinaryBuffer bb,
            int offset, SeekOrigin origin)
        {
            try
            {
                bb.Seek(offset, origin);
                Assert.Fail("Did not throw.");
            }
            catch (ArgumentOutOfRangeException e)
                when (e.ParamName == "offset")
            {
            }
        }

        private static void AssertBounds(
            ref BinaryBuffer bb, int offset)
        {
            try
            {
                bb.Offset = offset;
                Assert.Fail("Did not throw.");
            }
            catch (ArgumentOutOfRangeException e)
                when (e.ParamName == "offset")
            {
            }
        }

        [TestMethod]
        public void TestSeek()
        {
            var bb = new BinaryBuffer(new byte[] { 0, 1, 2, 3 }, 0);

            AssertSeek(ref bb, 0, 0, SeekOrigin.Begin);
            AssertSeek(ref bb, 1, 1, SeekOrigin.Begin);
            AssertSeek(ref bb, 0, -1, SeekOrigin.Current);
            AssertBounds(ref bb, -1, SeekOrigin.Current);
            AssertSeek(ref bb, 3, 0, SeekOrigin.End);
            AssertSeek(ref bb, 2, -1, SeekOrigin.End);
            AssertBounds(ref bb, 1, SeekOrigin.End);
            AssertSeek(ref bb, 3, 0, SeekOrigin.End);
            AssertBounds(ref bb, 1, SeekOrigin.Current);
        }

        [TestMethod]
        public void TestOffset()
        {
            var bb = new BinaryBuffer(new byte[] { 0, 1, 2, 3 }, 0);

            AssertBounds(ref bb, -1);
            AssertOffset(ref bb, 0, 0);
            AssertOffset(ref bb, 1, 1);
            AssertOffset(ref bb, 2, 2);
            AssertOffset(ref bb, 3, 3);
            AssertBounds(ref bb, 4);
        }
    }
}
