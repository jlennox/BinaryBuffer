using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryBuffer.Tests
{
    public enum ReadMethod
    {
        Byte,
        Short,
        ShortUnsafe,
        ShortNetwork,
        ShortNetworkUnsafe,
        UShort,
        UShortUnsafe,
        UShortNetwork,
        UShortNetworkUnsafe,
        Int,
        IntUnsafe,
        IntNetwork,
        IntNetworkUnsafe,
        UInt,
        UIntUnsafe,
        UIntNetwork,
        UIntNetworkUnsafe,
        Long,
        LongUnsafe,
        LongNetwork,
        LongNetworkUnsafe,
        ULong,
        ULongUnsafe,
        ULongNetwork,
        ULongNetworkUnsafe
    }

    public class BufferTest
    {
        private readonly byte[] _buffer;

        private static readonly Dictionary<ReadMethod, MethodInfo>
            _staticReadMethods =
            new Dictionary<ReadMethod, MethodInfo>();

        private static readonly Dictionary<ReadMethod, MethodInfo>
            _instanceReadMethods =
            new Dictionary<ReadMethod, MethodInfo>();

        private static readonly Dictionary<ReadMethod, MethodInfo>
            _instancePeekMethods =
            new Dictionary<ReadMethod, MethodInfo>();

        private static Dictionary<T, MethodInfo> GetMethods<T>(
            BindingFlags flags, string prefix)
        {
            return typeof(BinaryBuffer)
                .GetMethods(flags | BindingFlags.Public)
                .Where(t => t.Name.StartsWith(prefix))
                // Do not get the byte* basic overloads. They're at the end
                // of the call stack and testing them is both redundant and
                // would need to be done separately.
                .Where(t => t.GetParameters()
                   .All(q => q.ParameterType != typeof(byte*)))
                .ToDictionary(
                    t => (T)Enum.Parse(
                        typeof(T),
                        t.Name.Substring(prefix.Length)),
                    t => t);
        }

        static BufferTest()
        {
            _staticReadMethods = GetMethods<ReadMethod>(
                BindingFlags.Static, "Read");

            _instanceReadMethods = GetMethods<ReadMethod>(
                BindingFlags.Instance, "Read");

            _instanceReadMethods = GetMethods<ReadMethod>(
                BindingFlags.Instance, "Peek");
        }

        public BufferTest(byte[] buffer)
        {
            _buffer = buffer;
        }

        public void AssertRead<T>(T expected, ReadMethod method, int offset)
            where T : struct
        {
            var methodInfo = _staticReadMethods[method];
            var result = (T)methodInfo.Invoke(null, new object[] { _buffer, offset });
            Assert.AreEqual(expected, result);
        }
    }
}
