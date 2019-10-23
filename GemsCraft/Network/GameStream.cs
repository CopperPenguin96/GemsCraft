using System;
using System.IO;
using System.Text;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;

namespace GemsCraft.Network
{
    /*
Copyright (c) 2011-2013 Drew DeVault

Permission is hereby granted, free of charge, 
to any person obtaining a copy of this software 
and associated documentation files (the "Software"), 
to deal in the Software without restriction, including 
without limitation the rights to use, copy, modify, 
merge, publish, distribute, sublicense, and/or sell 
copies of the Software, and to permit persons to whom 
the Software is furnished to do so, subject to the following 
conditions:

The above copyright notice and this permission notice shall be 
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY 
CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
     */
     
    public class GameStream : Stream
    {
        static GameStream()
        {
            StringEncoding = Encoding.UTF8; // Make sure this is set to UTF8, Do not change it
        }

        public GameStream()
        {

        }

        public GameStream(Stream baseStream)
        {
            BaseStream = baseStream;
        }

        public Stream BaseStream { get; set; }

        public long Payload;
        public byte[] SharedKey;
        public SessionState State;
        internal byte[] VerifyToken;
        public string Username;
        public string ServerId;

        public override bool CanRead => BaseStream.CanRead;
        public override bool CanSeek => BaseStream.CanSeek;
        public override bool CanWrite => BaseStream.CanWrite;
        public override long Length => BaseStream.Length;

        /*public override long Position
        {
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }*/

        public override int Read(byte[] buffer, int offset, int count)
        {
            return BaseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            BaseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            try
            {
                BaseStream.Write(buffer, offset, count);
            }
            catch (Exception e)
            {
                Logger.Write(e.ToString());
            }
        }

        public override void Flush()
        {
            BaseStream.Flush();
        }

        public static Encoding StringEncoding;

        public VarInt ReadVarInt()
        {
            uint result = 0;
            int length = 0;
            while (true)
            {
                byte current = ReadUInt8();
                result |= (current & 0x7Fu) << length++ * 7;
                if (length > 5)
                {
                    if (length > 5) current--;
                }

                if ((current & 0x80) != 128)
                {
                    break;
                }
            }

            return (int)result;
        }

        public VarInt ReadVarInt(out int length)
        {
            uint result = 0;
            length = 0;
            while (true)
            {
                byte current = ReadUInt8();
                result |= (current & 0x7Fu) << length++ * 7;
                if (length > 5)
                {
                    throw new InvalidDataException("VarInt may not be longer than 60 bits.");
                }

                if ((current & 0x80) != 128)
                {
                    break;
                }
            }

            return (int)result;
        }

        public void WriteVarInt(int v)
        {
            long _value = v;
            uint value = (uint)_value;
            while (true)
            {
                if ((value & 0xFFFFFF80u) == 0)
                {
                    byte terror = (byte)value;
                    WriteUInt8(terror);
                    break;
                }
                WriteUInt8((byte)(value & 0x7F | 0x80));
                value >>= 7;
            }
        }

        public void WriteVarInt(VarInt v)
        {
            WriteVarInt((int) v.Value);
        }

        public void WriteVarInt(int v, out int length)
        {
            long _value = v;
            uint value = (uint)_value;
            Console.WriteLine(value.ToString("X"));
            length = 0;
            while (true)
            {
                length++;
                if ((value & 0xFFFFFF80u) == 0)
                {
                    WriteUInt8((byte)value);
                    break;
                }
                WriteUInt8((byte)(value & 0x7F | 0x80));
                value >>= 7;
            }
        }

        public void WriteVarInt(VarInt v, out int length)
        {
            int i;
            WriteVarInt((int) v.Value, out i);
            length = i;
        }

        public static int GetVarIntLength(int v)
        {
            long _value = v;
            uint value = (uint)_value;
            int length = 0;
            while (true)
            {
                length++;
                if ((value & 0xFFFFFF80u) == 0)
                    break;
                value >>= 7;
            }
            return length;
        }

        public byte ReadUInt8()
        {
            try
            {
                int value = BaseStream.ReadByte();
                //if (value == -1) throw new EndOfStreamException();
                return (byte)value;
            }
            catch (Exception e)
            {
                Logger.Write("Attempt to read data after disconnected.", LogType.Warning);
                return 0x00;
            }
        }

        public void WriteUInt8(byte value)
        {
            WriteByte(value);
        }

        public sbyte ReadInt8()
        {
            return (sbyte)ReadUInt8();
        }

        public void WriteInt8(sbyte value)
        {
            WriteUInt8((byte)value);
        }

        public ushort ReadUInt16()
        {
            return (ushort)(
                (ReadUInt8() << 8) |
                ReadUInt8());
        }

        public void WriteUInt16(ushort value)
        {
            Write(new[]
            {
                (byte)((value & 0xFF00) >> 8),
                (byte)(value & 0xFF)
            }, 0, 2);
        }

        public short ReadInt16()
        {
            return (short)ReadUInt16();
        }

        public void WriteInt16(short value)
        {
            WriteUInt16((ushort)value);
        }

        public uint ReadUInt32()
        {
            return (uint)(
                (ReadUInt8() << 24) |
                (ReadUInt8() << 16) |
                (ReadUInt8() << 8) |
                 ReadUInt8());
        }

        public void WriteUInt32(uint value)
        {
            Write(new[]
            {
                (byte)((value & 0xFF000000) >> 24),
                (byte)((value & 0xFF0000) >> 16),
                (byte)((value & 0xFF00) >> 8),
                (byte)(value & 0xFF)
            }, 0, 4);
        }

        public int ReadInt32()
        {
            return (int)ReadUInt32();
        }

        public void WriteInt32(int value)
        {
            WriteUInt32((uint)value);
        }

        public ulong ReadUInt64()
        {
            return unchecked(
                   ((ulong)ReadUInt8() << 56) |
                   ((ulong)ReadUInt8() << 48) |
                   ((ulong)ReadUInt8() << 40) |
                   ((ulong)ReadUInt8() << 32) |
                   ((ulong)ReadUInt8() << 24) |
                   ((ulong)ReadUInt8() << 16) |
                   ((ulong)ReadUInt8() << 8) |
                    (ulong)ReadUInt8());
        }

        public void WriteUInt64(ulong value)
        {
            Write(new[]
            {
                (byte)((value & 0xFF00000000000000) >> 56),
                (byte)((value & 0xFF000000000000) >> 48),
                (byte)((value & 0xFF0000000000) >> 40),
                (byte)((value & 0xFF00000000) >> 32),
                (byte)((value & 0xFF000000) >> 24),
                (byte)((value & 0xFF0000) >> 16),
                (byte)((value & 0xFF00) >> 8),
                (byte)(value & 0xFF)
            }, 0, 8);
        }

        public long ReadInt64()
        {
            return (long)ReadUInt64();
        }

        public void WriteInt64(long value)
        {
            WriteUInt64((ulong)value);
        }

        public byte[] ReadUInt8Array(int length)
        {
            var result = new byte[length];
            if (length == 0) return result;
            int n = length;
            while (true)
            {
                n -= Read(result, length - n, n);
                if (n == 0)
                    break;
                System.Threading.Thread.Sleep(1);
            }
            return result;
        }

        public void WriteUInt8Array(byte[] value)
        {
            Write(value, 0, value.Length);
        }

        public void WriteUInt8Array(byte[] value, int offset, int count)
        {
            Write(value, offset, count);
        }

        public sbyte[] ReadInt8Array(int length)
        {
            return (sbyte[])(Array)ReadUInt8Array(length);
        }

        public void WriteInt8Array(sbyte[] value)
        {
            Write((byte[])(Array)value, 0, value.Length);
        }

        public ushort[] ReadUInt16Array(int length)
        {
            var result = new ushort[length];
            if (length == 0) return result;
            for (int i = 0; i < length; i++)
                result[i] = ReadUInt16();
            return result;
        }

        public void WriteUInt16Array(ushort[] value)
        {
            foreach (var t in value)
                WriteUInt16(t);
        }

        public short[] ReadInt16Array(int length)
        {
            return (short[])(Array)ReadUInt16Array(length);
        }

        public void WriteInt16Array(short[] value)
        {
            WriteUInt16Array((ushort[])(Array)value);
        }

        public uint[] ReadUInt32Array(int length)
        {
            var result = new uint[length];
            if (length == 0) return result;
            for (int i = 0; i < length; i++)
                result[i] = ReadUInt32();
            return result;
        }

        public void WriteUInt32Array(uint[] value)
        {
            foreach (var t in value)
                WriteUInt32(t);
        }

        public int[] ReadInt32Array(int length)
        {
            return (int[])(Array)ReadUInt32Array(length);
        }

        public void WriteInt32Array(int[] value)
        {
            WriteUInt32Array((uint[])(Array)value);
        }

        public ulong[] ReadUInt64Array(int length)
        {
            var result = new ulong[length];
            if (length == 0) return result;
            for (int i = 0; i < length; i++)
                result[i] = ReadUInt64();
            return result;
        }

        public void WriteUInt64Array(ulong[] value)
        {
            foreach (var t in value)
                WriteUInt64(t);
        }

        public long[] ReadInt64Array(int length)
        {
            return (long[])(Array)ReadUInt64Array(length);
        }

        public void WriteInt64Array(long[] value)
        {
            WriteUInt64Array((ulong[])(Array)value);
        }

        public unsafe float ReadSingle()
        {
            uint value = ReadUInt32();
            return *(float*)&value;
        }

        public unsafe void WriteSingle(float value)
        {
            WriteUInt32(*(uint*)&value);
        }

        public unsafe double ReadDouble()
        {
            ulong value = ReadUInt64();
            return *(double*)&value;
        }

        public unsafe void WriteDouble(double value)
        {
            WriteUInt64(*(ulong*)&value);
        }

        public bool ReadBoolean()
        {
            return ReadUInt8() != 0;
        }

        public void WriteBoolean(bool value)
        {
            WriteUInt8(value ? (byte)1 : (byte)0);
        }

        public string ReadString()
        {
            VarInt length = ReadVarInt();
            if (length == 0) return string.Empty;
            var data = ReadUInt8Array((int) length.Value);
            return StringEncoding.GetString(data);
        }

        public void WriteString(string value)
        {
            WriteVarInt(StringEncoding.GetByteCount(value));
            if (value.Length > 0)
                WriteUInt8Array(StringEncoding.GetBytes(value));
        }

        public override long Position { get; set; }
    }
}
