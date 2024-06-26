using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace WorldToolKit.Tools
{
    public class WorldTools
    {
        private static byte[] GetHash(string s)
        {
            HashAlgorithm algorithm = SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(s));
        }

        public static string GetHashString(string s)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(s))
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
