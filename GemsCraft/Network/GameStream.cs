using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using fNbt;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using GemsCraft.ChatSystem;
using GemsCraft.Entities;
using GemsCraft.Entities.Metadata;
using GemsCraft.Network.Packets;

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
                byte current = ReadByte();
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
                byte current = ReadByte();
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
                    WriteByte(terror);
                    break;
                }
                WriteByte((byte)(value & 0x7F | 0x80));
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
                    WriteByte((byte)value);
                    break;
                }
                WriteByte((byte)(value & 0x7F | 0x80));
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

        public new byte ReadByte()
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

        public void WriteByte(byte value)
        {
            base.WriteByte(value);
        }

        public sbyte ReadSByte()
        {
            return (sbyte)ReadByte();
        }

        public void WriteSByte(sbyte value)
        {
            WriteByte((byte)value);
        }

        public ushort ReadShort()
        {
            return (ushort)(
                (ReadByte() << 8) |
                ReadByte());
        }

        public void WriteUShort(ushort value)
        {
            Write(new[]
            {
                (byte)((value & 0xFF00) >> 8),
                (byte)(value & 0xFF)
            }, 0, 2);
        }

        public void WriteShort(short value)
        {
            WriteUShort((ushort)value);
        }

        public uint ReadUInt()
        {
            return (uint)(
                (ReadByte() << 24) |
                (ReadByte() << 16) |
                (ReadByte() << 8) |
                 ReadByte());
        }

        public void WriteUInt(uint value)
        {
            Write(new[]
            {
                (byte)((value & 0xFF000000) >> 24),
                (byte)((value & 0xFF0000) >> 16),
                (byte)((value & 0xFF00) >> 8),
                (byte)(value & 0xFF)
            }, 0, 4);
        }

        public int ReadInt()
        {
            return (int)ReadUInt();
        }

        public void WriteInt(int value)
        {
            WriteUInt((uint)value);
        }

        public ulong ReadULong()
        {
            return unchecked(
                   ((ulong)ReadByte() << 56) |
                   ((ulong)ReadByte() << 48) |
                   ((ulong)ReadByte() << 40) |
                   ((ulong)ReadByte() << 32) |
                   ((ulong)ReadByte() << 24) |
                   ((ulong)ReadByte() << 16) |
                   ((ulong)ReadByte() << 8) |
                    (ulong)ReadByte());
        }

        public void WriteULong(ulong value)
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

        public long ReadLong()
        {
            return (long)ReadULong();
        }

        public void WriteLong(long value)
        {
            WriteULong((ulong)value);
        }

        public byte[] ReadByteArray(int length)
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

        public void WriteByteArray(byte[] value)
        {
            Write(value, 0, value.Length);
        }

        public void WriteByteArray(byte[] value, int offset, int count)
        {
            Write(value, offset, count);
        }

        public sbyte[] ReadSByteArray(int length)
        {
            return (sbyte[])(Array)ReadByteArray(length);
        }

        public void WriteSByteArray(sbyte[] value)
        {
            Write((byte[])(Array)value, 0, value.Length);
        }

        public ushort[] ReadShortArray(int length)
        {
            var result = new ushort[length];
            if (length == 0) return result;
            for (int i = 0; i < length; i++)
                result[i] = ReadShort();
            return result;
        }

        public void WriteUShortArray(ushort[] value)
        {
            foreach (var t in value)
                WriteUShort(t);
        }

        public void WriteShortArray(short[] value)
        {
            WriteUShortArray((ushort[])(Array)value);
        }

        public uint[] ReadUIntArray(int length)
        {
            var result = new uint[length];
            if (length == 0) return result;
            for (int i = 0; i < length; i++)
                result[i] = ReadUInt();
            return result;
        }

        public void WriteUIntArray(uint[] value)
        {
            foreach (var t in value)
                WriteUInt(t);
        }

        public int[] ReadIntArray(int length)
        {
            return (int[])(Array)ReadUIntArray(length);
        }

        public void WriteIntArray(int[] value)
        {
            WriteUIntArray((uint[])(Array)value);
        }

        public ulong[] ReadULongArray(int length)
        {
            var result = new ulong[length];
            if (length == 0) return result;
            for (int i = 0; i < length; i++)
                result[i] = ReadULong();
            return result;
        }

        public void WriteULongArray(ulong[] value)
        {
            foreach (var t in value)
                WriteULong(t);
        }

        public long[] ReadLongArray(int length)
        {
            return (long[])(Array)ReadULongArray(length);
        }

        public void WriteLongArray(long[] value)
        {
            WriteULongArray((ulong[])(Array)value);
        }

        public unsafe float ReadFloat()
        {
            uint value = ReadUInt();
            return *(float*)&value;
        }

        public unsafe void WriteFloat(float value)
        {
            WriteUInt(*(uint*)&value);
        }

        public unsafe double ReadDouble()
        {
            ulong value = ReadULong();
            return *(double*)&value;
        }

        public unsafe void WriteDouble(double value)
        {
            WriteULong(*(ulong*)&value);
        }

        public bool ReadBoolean()
        {
            return ReadByte() != 0;
        }

        public void WriteBoolean(bool value)
        {
            base.WriteByte(value ? (byte)1 : (byte)0);
        }

        public string ReadString()
        {
            VarInt length = ReadVarInt();
            if (length == 0) return string.Empty;
            var data = ReadByteArray((int) length.Value);
            return StringEncoding.GetString(data);
        }

        public void WriteString(string value)
        {
            WriteVarInt(StringEncoding.GetByteCount(value));
            if (value.Length > 0)
                WriteByteArray(StringEncoding.GetBytes(value));
        }

        public override long Position { get; set; }
        
        public void Write(object o)
        {
            switch (o)
            {
                case bool value:
                    WriteBoolean(value);
                    break;
                case sbyte sByte:
                    WriteSByte(sByte);
                    break;
                case byte by:
                    WriteByte(@by);
                    break;
                case short sh:
                    WriteShort(sh);
                    break;
                case ushort ush:
                    WriteUShort(ush);
                    break;
                case int i:
                    WriteInt(i);
                    break;
                case long l:
                    WriteLong(l);
                    break;
                case double d:
                    WriteDouble(d);
                    break;
                case string s:
                    WriteString(s);
                    break;
                case VarInt vi:
                    WriteVarInt(vi);
                    break;
                case byte[] bt:
                    WriteByteArray(bt);
                    break;
                case EntityMetadata ei:
                    WriteMetadata(ei);
                    break;
                case float ft:
                    WriteFloat(ft);
                    break;
            }

        }

        private const byte Closer = 0x7F;
        public void WriteMetadata(EntityMetadata dt)
        {
            WriteByte(dt.Index);
            object o = dt.Value;
            switch (dt.Type)
            {
                case EntityMetadataType.Byte:
                    WriteByte((byte) o);
                    break;
                case EntityMetadataType.VarInt:
                    WriteVarInt((VarInt) o);
                    break;
                case EntityMetadataType.Float:
                    WriteFloat((float) o);
                    break;
                case EntityMetadataType.String:
                    WriteString((string) o);
                    break;
                case EntityMetadataType.Chat:
                    ChatBuilder b = (ChatBuilder) o;
                    WriteString(b.Generate());
                    break;
                case EntityMetadataType.OptChat:
                    OptChat optC = (OptChat) o;
                    if (optC.Enabled)
                    {
                        WriteString(optC.Chat.Generate());
                    }

                    break;
                case EntityMetadataType.Slot:
                    Slot slot = (Slot) o;
                    short id = (short) slot.ItemID.Value;
                    WriteShort(id);
                    if (id != -1)
                    {
                        WriteSByte((sbyte) slot.ItemCount);
                        if (slot.OptionalNbt != null)
                        {
                            var file = new NbtFile(slot.OptionalNbt);
                            var data = file.SaveToBuffer(NbtCompression.GZip);
                            WriteShort((short) data.Length);
                            WriteByteArray(data);
                        }
                        else
                        {
                            WriteShort(-1);
                        }
                    }

                    break;
                case EntityMetadataType.Boolean:
                    WriteBoolean((bool) o);
                    break;
            }
            WriteByte(Closer);
        }
    }
}
