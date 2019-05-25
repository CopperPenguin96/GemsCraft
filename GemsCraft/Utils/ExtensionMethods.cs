using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GemsCraft.AppSystem;

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
    }
}
