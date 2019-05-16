using System;
using GemsCraft.Network;

namespace GemsCraft.AppSystem
{
    public sealed class VarInt : IEquatable<VarInt>
    {
        public const long MaxValue = 2147483647;
        public const long MinValue = -2147483648;
        public readonly long Value;

        public VarInt(long value)
        {
            if (value < MinValue) throw new ArgumentOutOfRangeException();
            if (value > MaxValue) throw new ArgumentOutOfRangeException();
            Value = value;
        }

        public int Length
        {
            get
            {
                uint result = 0;
                int length = 0;
                while (true)
                {
                    byte current = (byte) Value;
                    result |= (current & 0x7Fu) << length++ * 7;
                    if (length > 5)
                    {
                        throw new FormatException("VarInt cannot be longer than 5");
                    }

                    return length;
                }
            }
        }

        public static explicit operator VarInt(int val)
        {
            return new VarInt(val);
        }

        public static implicit operator VarInt(long val)
        {
            return new VarInt(val);
        }
        
        public static VarInt operator +(VarInt first, VarInt second)
        {
            return new VarInt(first.Value + second.Value);
        }

        public static VarInt operator -(VarInt first, VarInt second)
        {
            return new VarInt(first.Value - second.Value);
        }

        public static bool operator ==(VarInt first, VarInt second)
        {
            return first.Value == second.Value;
        }

        public static bool operator !=(VarInt first, VarInt second)
        {
            return first.Value != second.Value;
        }

        public override bool Equals(object o)
        {
            return this == (VarInt) o;
        }

        public bool Equals(VarInt other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
