using System;
using System.Runtime.CompilerServices;

#pragma warning disable CS3002 // Return type is not CLS-compliant
namespace BinaryBuffer
{
    public unsafe struct BinaryBuffer
    {
        private readonly byte[] _buffer;
        private int _offset;

        public BinaryBuffer(byte[] buffer, int offset)
            : this()
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (offset >= buffer.Length) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            _buffer = buffer;
            _offset = offset;
        }

        public byte PeekByte()
        {
            return _buffer[_offset];
        }

        public byte ReadByte()
        {
            var result = _buffer[_offset];
            ++_offset;
            return result;
        }

        public short PeekShort()
        {
            fixed (byte* bufferPtr = &_buffer[_offset])
            {
            return (short)(((short)bufferPtr[0] << 0) |
                ((short)bufferPtr[1] << 8));
            }
        }

        public short ReadShort()
        {
            var result = PeekShort();
            _offset += 2;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadShort(
            byte[] buffer, int offset)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 2) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadShortUnsafe(bufferPtr, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadShortUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadShortUnsafe(bufferPtr, offset);
            }
        }

        public static short ReadShortUnsafe(
            byte* bufferPtr, int offset)
        {
            return (short)(((short)bufferPtr[0] << 0) |
                ((short)bufferPtr[1] << 8));
        }
        public void WriteShort(short i)
        {
            WriteShort(_buffer, _offset, i);
            _offset += 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteShort(
            byte[] buffer, int offset, short i)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 2) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteShortUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteShortUnsafe(
            byte[] buffer, int offset, short i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteShortUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteShortUnsafe(
            byte* bufferPtr, int offset, short i)
        {
            bufferPtr[0] = (byte)((i >> 0) & 0xFF);
            bufferPtr[1] = (byte)((i >> 8) & 0xFF);
        }

        public short PeekShortNetwork()
        {
            fixed (byte* bufferPtr = &_buffer[_offset])
            {
            return (short)(((short)bufferPtr[0] << 8) |
                ((short)bufferPtr[1] << 0));
            }
        }

        public short ReadShortNetwork()
        {
            var result = PeekShortNetwork();
            _offset += 2;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadShortNetwork(
            byte[] buffer, int offset)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 2) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadShortNetworkUnsafe(bufferPtr, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadShortNetworkUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadShortNetworkUnsafe(bufferPtr, offset);
            }
        }

        public static short ReadShortNetworkUnsafe(
            byte* bufferPtr, int offset)
        {
            return (short)(((short)bufferPtr[0] << 8) |
                ((short)bufferPtr[1] << 0));
        }
        public void WriteShortNetwork(short i)
        {
            WriteShortNetwork(_buffer, _offset, i);
            _offset += 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteShortNetwork(
            byte[] buffer, int offset, short i)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 2) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteShortNetworkUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteShortNetworkUnsafe(
            byte[] buffer, int offset, short i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteShortNetworkUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteShortNetworkUnsafe(
            byte* bufferPtr, int offset, short i)
        {
            bufferPtr[0] = (byte)((i >> 8) & 0xFF);
            bufferPtr[1] = (byte)((i >> 0) & 0xFF);
        }

        public ushort PeekUShort()
        {
            fixed (byte* bufferPtr = &_buffer[_offset])
            {
            return (ushort)(((ushort)bufferPtr[0] << 0) |
                ((ushort)bufferPtr[1] << 8));
            }
        }

        public ushort ReadUShort()
        {
            var result = PeekUShort();
            _offset += 2;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReadUShort(
            byte[] buffer, int offset)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 2) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadUShortUnsafe(bufferPtr, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReadUShortUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadUShortUnsafe(bufferPtr, offset);
            }
        }

        public static ushort ReadUShortUnsafe(
            byte* bufferPtr, int offset)
        {
            return (ushort)(((ushort)bufferPtr[0] << 0) |
                ((ushort)bufferPtr[1] << 8));
        }
        public void WriteUShort(ushort i)
        {
            WriteUShort(_buffer, _offset, i);
            _offset += 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUShort(
            byte[] buffer, int offset, ushort i)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 2) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteUShortUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteUShortUnsafe(
            byte[] buffer, int offset, ushort i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteUShortUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteUShortUnsafe(
            byte* bufferPtr, int offset, ushort i)
        {
            bufferPtr[0] = (byte)((i >> 0) & 0xFF);
            bufferPtr[1] = (byte)((i >> 8) & 0xFF);
        }

        public ushort PeekUShortNetwork()
        {
            fixed (byte* bufferPtr = &_buffer[_offset])
            {
            return (ushort)(((ushort)bufferPtr[0] << 8) |
                ((ushort)bufferPtr[1] << 0));
            }
        }

        public ushort ReadUShortNetwork()
        {
            var result = PeekUShortNetwork();
            _offset += 2;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReadUShortNetwork(
            byte[] buffer, int offset)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 2) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadUShortNetworkUnsafe(bufferPtr, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReadUShortNetworkUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadUShortNetworkUnsafe(bufferPtr, offset);
            }
        }

        public static ushort ReadUShortNetworkUnsafe(
            byte* bufferPtr, int offset)
        {
            return (ushort)(((ushort)bufferPtr[0] << 8) |
                ((ushort)bufferPtr[1] << 0));
        }
        public void WriteUShortNetwork(ushort i)
        {
            WriteUShortNetwork(_buffer, _offset, i);
            _offset += 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUShortNetwork(
            byte[] buffer, int offset, ushort i)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 2) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteUShortNetworkUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteUShortNetworkUnsafe(
            byte[] buffer, int offset, ushort i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteUShortNetworkUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteUShortNetworkUnsafe(
            byte* bufferPtr, int offset, ushort i)
        {
            bufferPtr[0] = (byte)((i >> 8) & 0xFF);
            bufferPtr[1] = (byte)((i >> 0) & 0xFF);
        }

        public int PeekInt()
        {
            fixed (byte* bufferPtr = &_buffer[_offset])
            {
            return ((int)bufferPtr[0] << 0) |
                ((int)bufferPtr[1] << 8) |
                ((int)bufferPtr[2] << 16) |
                ((int)bufferPtr[3] << 24);
            }
        }

        public int ReadInt()
        {
            var result = PeekInt();
            _offset += 4;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadInt(
            byte[] buffer, int offset)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 4) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadIntUnsafe(bufferPtr, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadIntUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadIntUnsafe(bufferPtr, offset);
            }
        }

        public static int ReadIntUnsafe(
            byte* bufferPtr, int offset)
        {
            return ((int)bufferPtr[0] << 0) |
                ((int)bufferPtr[1] << 8) |
                ((int)bufferPtr[2] << 16) |
                ((int)bufferPtr[3] << 24);
        }
        public void WriteInt(int i)
        {
            WriteInt(_buffer, _offset, i);
            _offset += 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt(
            byte[] buffer, int offset, int i)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 4) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteIntUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteIntUnsafe(
            byte[] buffer, int offset, int i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteIntUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteIntUnsafe(
            byte* bufferPtr, int offset, int i)
        {
            bufferPtr[0] = (byte)((i >> 0) & 0xFF);
            bufferPtr[1] = (byte)((i >> 8) & 0xFF);
            bufferPtr[2] = (byte)((i >> 16) & 0xFF);
            bufferPtr[3] = (byte)((i >> 24) & 0xFF);
        }

        public int PeekIntNetwork()
        {
            fixed (byte* bufferPtr = &_buffer[_offset])
            {
            return ((int)bufferPtr[0] << 24) |
                ((int)bufferPtr[1] << 16) |
                ((int)bufferPtr[2] << 8) |
                ((int)bufferPtr[3] << 0);
            }
        }

        public int ReadIntNetwork()
        {
            var result = PeekIntNetwork();
            _offset += 4;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadIntNetwork(
            byte[] buffer, int offset)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 4) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadIntNetworkUnsafe(bufferPtr, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadIntNetworkUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadIntNetworkUnsafe(bufferPtr, offset);
            }
        }

        public static int ReadIntNetworkUnsafe(
            byte* bufferPtr, int offset)
        {
            return ((int)bufferPtr[0] << 24) |
                ((int)bufferPtr[1] << 16) |
                ((int)bufferPtr[2] << 8) |
                ((int)bufferPtr[3] << 0);
        }
        public void WriteIntNetwork(int i)
        {
            WriteIntNetwork(_buffer, _offset, i);
            _offset += 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteIntNetwork(
            byte[] buffer, int offset, int i)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 4) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteIntNetworkUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteIntNetworkUnsafe(
            byte[] buffer, int offset, int i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteIntNetworkUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteIntNetworkUnsafe(
            byte* bufferPtr, int offset, int i)
        {
            bufferPtr[0] = (byte)((i >> 24) & 0xFF);
            bufferPtr[1] = (byte)((i >> 16) & 0xFF);
            bufferPtr[2] = (byte)((i >> 8) & 0xFF);
            bufferPtr[3] = (byte)((i >> 0) & 0xFF);
        }

        public uint PeekUInt()
        {
            fixed (byte* bufferPtr = &_buffer[_offset])
            {
            return ((uint)bufferPtr[0] << 0) |
                ((uint)bufferPtr[1] << 8) |
                ((uint)bufferPtr[2] << 16) |
                ((uint)bufferPtr[3] << 24);
            }
        }

        public uint ReadUInt()
        {
            var result = PeekUInt();
            _offset += 4;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReadUInt(
            byte[] buffer, int offset)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 4) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadUIntUnsafe(bufferPtr, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReadUIntUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadUIntUnsafe(bufferPtr, offset);
            }
        }

        public static uint ReadUIntUnsafe(
            byte* bufferPtr, int offset)
        {
            return ((uint)bufferPtr[0] << 0) |
                ((uint)bufferPtr[1] << 8) |
                ((uint)bufferPtr[2] << 16) |
                ((uint)bufferPtr[3] << 24);
        }
        public void WriteUInt(uint i)
        {
            WriteUInt(_buffer, _offset, i);
            _offset += 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt(
            byte[] buffer, int offset, uint i)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 4) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteUIntUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteUIntUnsafe(
            byte[] buffer, int offset, uint i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteUIntUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteUIntUnsafe(
            byte* bufferPtr, int offset, uint i)
        {
            bufferPtr[0] = (byte)((i >> 0) & 0xFF);
            bufferPtr[1] = (byte)((i >> 8) & 0xFF);
            bufferPtr[2] = (byte)((i >> 16) & 0xFF);
            bufferPtr[3] = (byte)((i >> 24) & 0xFF);
        }

        public uint PeekUIntNetwork()
        {
            fixed (byte* bufferPtr = &_buffer[_offset])
            {
            return ((uint)bufferPtr[0] << 24) |
                ((uint)bufferPtr[1] << 16) |
                ((uint)bufferPtr[2] << 8) |
                ((uint)bufferPtr[3] << 0);
            }
        }

        public uint ReadUIntNetwork()
        {
            var result = PeekUIntNetwork();
            _offset += 4;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReadUIntNetwork(
            byte[] buffer, int offset)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 4) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadUIntNetworkUnsafe(bufferPtr, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReadUIntNetworkUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadUIntNetworkUnsafe(bufferPtr, offset);
            }
        }

        public static uint ReadUIntNetworkUnsafe(
            byte* bufferPtr, int offset)
        {
            return ((uint)bufferPtr[0] << 24) |
                ((uint)bufferPtr[1] << 16) |
                ((uint)bufferPtr[2] << 8) |
                ((uint)bufferPtr[3] << 0);
        }
        public void WriteUIntNetwork(uint i)
        {
            WriteUIntNetwork(_buffer, _offset, i);
            _offset += 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUIntNetwork(
            byte[] buffer, int offset, uint i)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 4) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteUIntNetworkUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteUIntNetworkUnsafe(
            byte[] buffer, int offset, uint i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteUIntNetworkUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteUIntNetworkUnsafe(
            byte* bufferPtr, int offset, uint i)
        {
            bufferPtr[0] = (byte)((i >> 24) & 0xFF);
            bufferPtr[1] = (byte)((i >> 16) & 0xFF);
            bufferPtr[2] = (byte)((i >> 8) & 0xFF);
            bufferPtr[3] = (byte)((i >> 0) & 0xFF);
        }

        public long PeekLong()
        {
            fixed (byte* bufferPtr = &_buffer[_offset])
            {
            return ((long)bufferPtr[0] << 0) |
                ((long)bufferPtr[1] << 8) |
                ((long)bufferPtr[2] << 16) |
                ((long)bufferPtr[3] << 24) |
                ((long)bufferPtr[4] << 32) |
                ((long)bufferPtr[5] << 40) |
                ((long)bufferPtr[6] << 48) |
                ((long)bufferPtr[7] << 56);
            }
        }

        public long ReadLong()
        {
            var result = PeekLong();
            _offset += 8;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ReadLong(
            byte[] buffer, int offset)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 8) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadLongUnsafe(bufferPtr, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ReadLongUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadLongUnsafe(bufferPtr, offset);
            }
        }

        public static long ReadLongUnsafe(
            byte* bufferPtr, int offset)
        {
            return ((long)bufferPtr[0] << 0) |
                ((long)bufferPtr[1] << 8) |
                ((long)bufferPtr[2] << 16) |
                ((long)bufferPtr[3] << 24) |
                ((long)bufferPtr[4] << 32) |
                ((long)bufferPtr[5] << 40) |
                ((long)bufferPtr[6] << 48) |
                ((long)bufferPtr[7] << 56);
        }
        public void WriteLong(long i)
        {
            WriteLong(_buffer, _offset, i);
            _offset += 8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLong(
            byte[] buffer, int offset, long i)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 8) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteLongUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteLongUnsafe(
            byte[] buffer, int offset, long i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteLongUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteLongUnsafe(
            byte* bufferPtr, int offset, long i)
        {
            bufferPtr[0] = (byte)((i >> 0) & 0xFF);
            bufferPtr[1] = (byte)((i >> 8) & 0xFF);
            bufferPtr[2] = (byte)((i >> 16) & 0xFF);
            bufferPtr[3] = (byte)((i >> 24) & 0xFF);
            bufferPtr[4] = (byte)((i >> 32) & 0xFF);
            bufferPtr[5] = (byte)((i >> 40) & 0xFF);
            bufferPtr[6] = (byte)((i >> 48) & 0xFF);
            bufferPtr[7] = (byte)((i >> 56) & 0xFF);
        }

        public long PeekLongNetwork()
        {
            fixed (byte* bufferPtr = &_buffer[_offset])
            {
            return ((long)bufferPtr[0] << 56) |
                ((long)bufferPtr[1] << 48) |
                ((long)bufferPtr[2] << 40) |
                ((long)bufferPtr[3] << 32) |
                ((long)bufferPtr[4] << 24) |
                ((long)bufferPtr[5] << 16) |
                ((long)bufferPtr[6] << 8) |
                ((long)bufferPtr[7] << 0);
            }
        }

        public long ReadLongNetwork()
        {
            var result = PeekLongNetwork();
            _offset += 8;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ReadLongNetwork(
            byte[] buffer, int offset)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 8) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadLongNetworkUnsafe(bufferPtr, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ReadLongNetworkUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadLongNetworkUnsafe(bufferPtr, offset);
            }
        }

        public static long ReadLongNetworkUnsafe(
            byte* bufferPtr, int offset)
        {
            return ((long)bufferPtr[0] << 56) |
                ((long)bufferPtr[1] << 48) |
                ((long)bufferPtr[2] << 40) |
                ((long)bufferPtr[3] << 32) |
                ((long)bufferPtr[4] << 24) |
                ((long)bufferPtr[5] << 16) |
                ((long)bufferPtr[6] << 8) |
                ((long)bufferPtr[7] << 0);
        }
        public void WriteLongNetwork(long i)
        {
            WriteLongNetwork(_buffer, _offset, i);
            _offset += 8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLongNetwork(
            byte[] buffer, int offset, long i)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 8) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteLongNetworkUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteLongNetworkUnsafe(
            byte[] buffer, int offset, long i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteLongNetworkUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteLongNetworkUnsafe(
            byte* bufferPtr, int offset, long i)
        {
            bufferPtr[0] = (byte)((i >> 56) & 0xFF);
            bufferPtr[1] = (byte)((i >> 48) & 0xFF);
            bufferPtr[2] = (byte)((i >> 40) & 0xFF);
            bufferPtr[3] = (byte)((i >> 32) & 0xFF);
            bufferPtr[4] = (byte)((i >> 24) & 0xFF);
            bufferPtr[5] = (byte)((i >> 16) & 0xFF);
            bufferPtr[6] = (byte)((i >> 8) & 0xFF);
            bufferPtr[7] = (byte)((i >> 0) & 0xFF);
        }

        public ulong PeekULong()
        {
            fixed (byte* bufferPtr = &_buffer[_offset])
            {
            return ((ulong)bufferPtr[0] << 0) |
                ((ulong)bufferPtr[1] << 8) |
                ((ulong)bufferPtr[2] << 16) |
                ((ulong)bufferPtr[3] << 24) |
                ((ulong)bufferPtr[4] << 32) |
                ((ulong)bufferPtr[5] << 40) |
                ((ulong)bufferPtr[6] << 48) |
                ((ulong)bufferPtr[7] << 56);
            }
        }

        public ulong ReadULong()
        {
            var result = PeekULong();
            _offset += 8;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReadULong(
            byte[] buffer, int offset)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 8) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadULongUnsafe(bufferPtr, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReadULongUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadULongUnsafe(bufferPtr, offset);
            }
        }

        public static ulong ReadULongUnsafe(
            byte* bufferPtr, int offset)
        {
            return ((ulong)bufferPtr[0] << 0) |
                ((ulong)bufferPtr[1] << 8) |
                ((ulong)bufferPtr[2] << 16) |
                ((ulong)bufferPtr[3] << 24) |
                ((ulong)bufferPtr[4] << 32) |
                ((ulong)bufferPtr[5] << 40) |
                ((ulong)bufferPtr[6] << 48) |
                ((ulong)bufferPtr[7] << 56);
        }
        public void WriteULong(ulong i)
        {
            WriteULong(_buffer, _offset, i);
            _offset += 8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteULong(
            byte[] buffer, int offset, ulong i)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 8) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteULongUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteULongUnsafe(
            byte[] buffer, int offset, ulong i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteULongUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteULongUnsafe(
            byte* bufferPtr, int offset, ulong i)
        {
            bufferPtr[0] = (byte)((i >> 0) & 0xFF);
            bufferPtr[1] = (byte)((i >> 8) & 0xFF);
            bufferPtr[2] = (byte)((i >> 16) & 0xFF);
            bufferPtr[3] = (byte)((i >> 24) & 0xFF);
            bufferPtr[4] = (byte)((i >> 32) & 0xFF);
            bufferPtr[5] = (byte)((i >> 40) & 0xFF);
            bufferPtr[6] = (byte)((i >> 48) & 0xFF);
            bufferPtr[7] = (byte)((i >> 56) & 0xFF);
        }

        public ulong PeekULongNetwork()
        {
            fixed (byte* bufferPtr = &_buffer[_offset])
            {
            return ((ulong)bufferPtr[0] << 56) |
                ((ulong)bufferPtr[1] << 48) |
                ((ulong)bufferPtr[2] << 40) |
                ((ulong)bufferPtr[3] << 32) |
                ((ulong)bufferPtr[4] << 24) |
                ((ulong)bufferPtr[5] << 16) |
                ((ulong)bufferPtr[6] << 8) |
                ((ulong)bufferPtr[7] << 0);
            }
        }

        public ulong ReadULongNetwork()
        {
            var result = PeekULongNetwork();
            _offset += 8;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReadULongNetwork(
            byte[] buffer, int offset)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 8) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadULongNetworkUnsafe(bufferPtr, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReadULongNetworkUnsafe(
            byte[] buffer, int offset)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                return ReadULongNetworkUnsafe(bufferPtr, offset);
            }
        }

        public static ulong ReadULongNetworkUnsafe(
            byte* bufferPtr, int offset)
        {
            return ((ulong)bufferPtr[0] << 56) |
                ((ulong)bufferPtr[1] << 48) |
                ((ulong)bufferPtr[2] << 40) |
                ((ulong)bufferPtr[3] << 32) |
                ((ulong)bufferPtr[4] << 24) |
                ((ulong)bufferPtr[5] << 16) |
                ((ulong)bufferPtr[6] << 8) |
                ((ulong)bufferPtr[7] << 0);
        }
        public void WriteULongNetwork(ulong i)
        {
            WriteULongNetwork(_buffer, _offset, i);
            _offset += 8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteULongNetwork(
            byte[] buffer, int offset, ulong i)
        {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }
            if (offset < 0) { throw new ArgumentOutOfRangeException(nameof(offset), offset, "Value must be positive."); }
            if (buffer.Length - offset < 8) { throw new ArgumentException("Index out of range.", nameof(buffer)); }

            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteULongNetworkUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteULongNetworkUnsafe(
            byte[] buffer, int offset, ulong i)
        {
            fixed (byte* bufferPtr = &buffer[offset])
            {
                WriteULongNetworkUnsafe(bufferPtr, offset, i);
            }
        }

        public static void WriteULongNetworkUnsafe(
            byte* bufferPtr, int offset, ulong i)
        {
            bufferPtr[0] = (byte)((i >> 56) & 0xFF);
            bufferPtr[1] = (byte)((i >> 48) & 0xFF);
            bufferPtr[2] = (byte)((i >> 40) & 0xFF);
            bufferPtr[3] = (byte)((i >> 32) & 0xFF);
            bufferPtr[4] = (byte)((i >> 24) & 0xFF);
            bufferPtr[5] = (byte)((i >> 16) & 0xFF);
            bufferPtr[6] = (byte)((i >> 8) & 0xFF);
            bufferPtr[7] = (byte)((i >> 0) & 0xFF);
        }

    }
}
