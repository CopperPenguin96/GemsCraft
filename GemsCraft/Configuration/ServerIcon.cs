using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Utils;

namespace GemsCraft.Configuration
{
    internal class ServerIcon
    {
        protected bool Equals(ServerIcon other)
        {
            return Equals(_image, other._image);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ServerIcon) obj);
        }

        public override int GetHashCode()
        {
            return (_image != null ? _image.GetHashCode() : 0);
        }

        private readonly Image _image;

        public ServerIcon(Image image)
        {
            _image = image;
        }

        public static implicit operator ServerIcon(Image image)
        {
            return new ServerIcon(image);
        }

        public static bool operator ==(ServerIcon s1, ServerIcon s2)
        {
            return s1 == s2;
        }

        public static bool operator !=(ServerIcon s1, ServerIcon s2)
        {
            return s1 != s2;
        }

        public override string ToString()
        {
            return ImageUtil.ToString(_image);
        }
    }
}
