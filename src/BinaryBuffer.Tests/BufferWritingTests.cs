using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryBuffer.Tests
{
    [TestClass]
    public class BufferWritingTests
    {
        [DataTestMethod]
        [DataRow((ushort)0x2233, DisplayName = "Unsigned")]
        [DataRow((ushort)0xF456, DisplayName = "Signed")]
        public void WriteShort(ushort value)
        {
            unchecked
            {
                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteShort(buf, 0, (short)value);
                    Assert.AreEqual((short)value, BinaryBuffer.ReadShort(buf, 0));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteShort(buf, 1, (short)value);
                    Assert.AreEqual((short)value, BinaryBuffer.ReadShort(buf, 1));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteShortNetwork(buf, 0, (short)value);
                    Assert.AreEqual((short)value, BinaryBuffer.ReadShortNetwork(buf, 0));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteShortNetwork(buf, 1, (short)value);
                    Assert.AreEqual((short)value, BinaryBuffer.ReadShortNetwork(buf, 1));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteUShort(buf, 0, (ushort)value);
                    Assert.AreEqual((ushort)value, BinaryBuffer.ReadUShort(buf, 0));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteUShort(buf, 1, (ushort)value);
                    Assert.AreEqual((ushort)value, BinaryBuffer.ReadUShort(buf, 1));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteUShortNetwork(buf, 0, (ushort)value);
                    Assert.AreEqual((ushort)value, BinaryBuffer.ReadUShortNetwork(buf, 0));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteUShortNetwork(buf, 1, (ushort)value);
                    Assert.AreEqual((ushort)value, BinaryBuffer.ReadUShortNetwork(buf, 1));
                }
            }
        }

        [DataTestMethod]
        [DataRow((uint)0x22334455, DisplayName = "Unsigned")]
        [DataRow((uint)0xF4565432, DisplayName = "Signed")]
        public void WriteInt(uint value)
        {
            unchecked
            {
                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteInt(buf, 0, (int)value);
                    Assert.AreEqual((int)value, BinaryBuffer.ReadInt(buf, 0));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteInt(buf, 1, (int)value);
                    Assert.AreEqual((int)value, BinaryBuffer.ReadInt(buf, 1));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteIntNetwork(buf, 0, (int)value);
                    Assert.AreEqual((int)value, BinaryBuffer.ReadIntNetwork(buf, 0));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteIntNetwork(buf, 1, (int)value);
                    Assert.AreEqual((int)value, BinaryBuffer.ReadIntNetwork(buf, 1));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteUInt(buf, 0, (uint)value);
                    Assert.AreEqual((uint)value, BinaryBuffer.ReadUInt(buf, 0));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteUInt(buf, 1, (uint)value);
                    Assert.AreEqual((uint)value, BinaryBuffer.ReadUInt(buf, 1));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteUIntNetwork(buf, 0, (uint)value);
                    Assert.AreEqual((uint)value, BinaryBuffer.ReadUIntNetwork(buf, 0));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteUIntNetwork(buf, 1, (uint)value);
                    Assert.AreEqual((uint)value, BinaryBuffer.ReadUIntNetwork(buf, 1));
                }
            }
        }

        [DataTestMethod]
        [DataRow((ulong)0x2233445544332211, DisplayName = "Unsigned")]
        [DataRow((ulong)0xF456543210123456, DisplayName = "Signed")]
        public void WriteLong(ulong value)
        {
            unchecked
            {
                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteLong(buf, 0, (long)value);
                    Assert.AreEqual((long)value, BinaryBuffer.ReadLong(buf, 0));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteLong(buf, 1, (long)value);
                    Assert.AreEqual((long)value, BinaryBuffer.ReadLong(buf, 1));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteLongNetwork(buf, 0, (long)value);
                    Assert.AreEqual((long)value, BinaryBuffer.ReadLongNetwork(buf, 0));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteLongNetwork(buf, 1, (long)value);
                    Assert.AreEqual((long)value, BinaryBuffer.ReadLongNetwork(buf, 1));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteULong(buf, 0, (ulong)value);
                    Assert.AreEqual((ulong)value, BinaryBuffer.ReadULong(buf, 0));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteULong(buf, 1, (ulong)value);
                    Assert.AreEqual((ulong)value, BinaryBuffer.ReadULong(buf, 1));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteULongNetwork(buf, 0, (ulong)value);
                    Assert.AreEqual((ulong)value, BinaryBuffer.ReadULongNetwork(buf, 0));
                }

                {
                    var buf = new byte[10];
                    BinaryBuffer.WriteULongNetwork(buf, 1, (ulong)value);
                    Assert.AreEqual((ulong)value, BinaryBuffer.ReadULongNetwork(buf, 1));
                }
            }
        }
    }
}
