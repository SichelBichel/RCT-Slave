using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RCT_Slave
{
    public static class CryptoCore
    {
        private static readonly byte[] key = Encoding.UTF8.GetBytes("ChangeAndCompile"); // 16b
        private static readonly byte[] iv = Encoding.UTF8.GetBytes("ChangeAndCompile"); // 16b

        public static string Encrypt(string plainText)
        {
            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] input = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = encryptor.TransformFinalBlock(input, 0, input.Length);

            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string encryptedText)
        {
            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor();
            byte[] input = Convert.FromBase64String(encryptedText);
            byte[] decrypted = decryptor.TransformFinalBlock(input, 0, input.Length);

            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
