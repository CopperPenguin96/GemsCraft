using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using GemsCraft.AppSystem.Types;

namespace GemsCraft.Utils
{

    public static class UuidUtil
    {
    }

    public static class MathUtil
    {
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

}
