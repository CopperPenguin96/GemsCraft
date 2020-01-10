using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using GemBlocks.Blocks;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using ikvm.extensions;

namespace GemsCraft.Utils
{
    public static class BlockUtil
    {
        /// <summary>
        /// Puts the meta and type into an int[]
        /// </summary>
        public static int[] GetMetaAndType(this Block block)
        {
            return new[] { block.Meta, block.Type };
        }
    }

    public static class ByteUtil
    {
        public static bool ToBoolean(this byte b)
        {
            return b != 0;
        }

        public static bool IsBitSet(this byte b, Enum value)
        {
            return (b & (1 << Convert.ToByte(value))) != 0;
        }

        public static byte SetBitOn(this byte b, Enum value, bool on)
        {
            byte val = b;
            byte mask = Convert.ToByte(value);
            if (!on)
            {
                val &= (byte)~mask;
            }
            else
            {
                val |= (byte)mask;
            }

            return val;
        }
    }

    public static class BooleanUtil
    {
        public static byte ToByte(this bool b)
        {
            return b ? (byte)1 : (byte)0;
        }
    }

    public static class ImageUtil
    {
        public static string ToString(this Image image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            MemoryStream ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            byte[] array = ms.ToArray();
            return Convert.ToBase64String(array);
        }
    }

    public static class StringArrayUtil
    {
        public static string ToCommaSeperatedString(this IEnumerable<string> strArray)
        {
            string str = "";
            string[] strNew = strArray.ToArray();
            for (int x = 0; x <= strNew.Length - 1; x++)
            {
                if (x == strNew.Length - 1) str += strNew[x];
                else str += strNew[x] + ",";
            }

            return str;
        }
    }

    public static class StringUtil
    {
        public static bool EndsWith(this string str, string chars)
        {
            if (chars.Length > str.Length) return false;
            int strLength = str.Length - chars.Length;
            return str.substring(strLength, chars.Length) == chars;
        }

        public static bool EndsWith(this string str, char[] chars)
        {
            return EndsWith(str, new string(chars));
        }

        public static bool StartsWith(this string str, string chars)
        {
            if (chars.Length > str.Length) return false;
            return str.Substring(0, chars.Length) == chars;
        }
        
        public static bool StartsWith(this string str, char[] chars)
        {
            return StartsWith(str, new string(chars));
        }

        public static Block GetBlock(this string block)
        {
            foreach (Block blk in BlockRegistry.Blocks)
            {
                if (string.Equals(blk.Name, block, StringComparison.CurrentCultureIgnoreCase))
                {
                    return blk;
                }
            }

            return Block.Undefined;
        }

        public static string ToCompactString(this DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
        }

        public static string ToCompactString(this TimeSpan span)
        {
            return $"{span.Days}.{span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}";
        }

        public static string[] Alphabet =
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i",
            "j", "k", "l", "m", "n", "o", "p", "q", "r",
            "s", "t", "u", "v", "w", "x", "y", "z"
        };

        public static Image ToImage(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            byte[] array = Convert.FromBase64String(str);
            Image image = Image.FromStream(new MemoryStream(array));
            return image;
        }

        public static byte[] ToBytes(this string str, [Optional] Encoding enc)
        {
            if (enc == null)
            {
                enc = Encoding.UTF8;
            }

            return enc.GetBytes(str);
        }

        public static byte[] ToBytes(this string str, [Optional] Encoding enc, out int length)
        {
            byte[] b = ToBytes(str, enc);
            length = b.Length;
            return b;
        }

        public static int GetByteLength(this string str, [Optional] Encoding enc)
        {
            if (enc == null)
            {
                enc = Encoding.UTF8;
            }

            ToBytes(str, enc, out int length);
            return length;
        }

        public static bool IsValidURL(this string str)
        {
            bool result = Uri.TryCreate(str, UriKind.Absolute, out var uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }
    }

    public static class UrlUtil
    {
    }

    public static class ArrayUtil
    {
        public static void Fill<T>(this T[] array, int start, int end, T value)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (start < 0 || start >= end)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (end >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(end));
            }

            for (int i = start; i < end; i++)
            {
                array[i] = value;
            }
        }

        public static void Fill<T>(this T[] array, T value)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));

            for (int x = 0; x <= array.Length - 1; x++)
            {
                array[x] = value;
            }
        }

        public static void Copy<T>(this T[] array,
            int srcIndex, T[] dest, int destIndex, int length)
        {
            Array.Copy(array, srcIndex, dest, destIndex, length);
        }
    }
}
