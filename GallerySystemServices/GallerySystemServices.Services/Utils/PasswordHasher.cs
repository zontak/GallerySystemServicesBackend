using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GallerySystemServices.Services.Utils
{
    public static class PasswordHasher
    {
        public static string ConvertStringToSHA1(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(str))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        private static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}