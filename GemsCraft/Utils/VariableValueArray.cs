using System;

namespace GemsCraft.Utils
{
    public class VariableValueArray : ICloneable
    {
        private long[] Backing { get; }
        private int Capacity { get; }
        private int BitsPerValue { get; }
        private long ValueMask { get; }

        public VariableValueArray(int bitsPerValue, int cap)
        {
            if (cap < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cap));
            }

            if (bitsPerValue < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(bitsPerValue));
            }

            if (bitsPerValue > 64)
            {
                throw new ArgumentOutOfRangeException(nameof(bitsPerValue));
            }

            Backing = new long[(int) Math.Ceiling(bitsPerValue * cap / 64.0)];
            BitsPerValue = bitsPerValue;
            ValueMask = (1L << bitsPerValue) - 1L;
            Capacity = cap;
        }

        public static int CalculateNeededBits(int number)
        {
            int count = 0;
            do
            {
                count++;
                number = (int) ((uint) number >> 1);
            } while (number != 0);

            return count;
        }

        public long GetLargestPossibleValue()
        {
            return ValueMask;
        }

        public int Get(int index)
        {
            CheckIndex(index);

            index *= BitsPerValue;
            int i0 = index >> 6;
            int i1 = index & 0x3f;

            long value = (int)((uint)Backing[i0] >> i1);
            int i2 = i1 + BitsPerValue;

            if (i2 > 64)
            {
                value |= Backing[++i0] << 64 - i1;
            }

            return (int) (value & ValueMask);
        }

        public void Set(int index, int value)
        {
            CheckIndex(index);

            if (value < 0 || value > ValueMask)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            index *= BitsPerValue;
            int i0 = index >> 6;
            int i1 = index & 0x3f;

            Backing[i0] = Backing[i0] & ~(ValueMask << i1) | (value & ValueMask) << i1;
            int i2 = i1 + BitsPerValue;

            if (i2 <= 64) return;
            i0++;
            Backing[i0] = Backing[i0] & ~((1L << i2 - 64) - 1L) | value >> 64 - i1;
        }

        public void CheckIndex(int index)
        {
            if (index < 0 || index >= Capacity)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public VariableValueArray IncreaseBitsPerValueTo(int newValue)
        {
            if (newValue < BitsPerValue || newValue == BitsPerValue)
            {
                throw new ArgumentException(nameof(newValue));
            }

            VariableValueArray returned = new VariableValueArray(newValue, Capacity);
            for (int i = 0; i < Capacity; i++)
            {
                returned.Set(i, Get(i));
            }

            return returned;
        }

        public object Clone()
        {
            VariableValueArray clone = new VariableValueArray(BitsPerValue, Capacity);
            Backing.Copy(0, Backing, 0, Backing.Length);
            return clone;
        }
    }
}
