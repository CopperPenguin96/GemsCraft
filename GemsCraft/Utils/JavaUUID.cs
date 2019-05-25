using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Utils
{
    public class JavaUUID
    {
        public static string Generate(byte[] input)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(input);
            hash[6] &= 0x0f;
            hash[6] |= 0x30;
            hash[8] &= 0x3f;
            hash[8] |= 0x80;
            string hex = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            return hex.Insert(8, "-").Insert(13, "-").Insert(18, "-").Insert(23, "-");
        }
    }
}
