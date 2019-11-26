using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;

namespace GemsCraft.Utils
{
    public static class ByteUtil
    {
        public static bool ToBoolean(this byte b)
        {
            return b != 0;
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
            return b ? (byte) 1 : (byte) 0;
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

    public static class StringUtil
    {
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
