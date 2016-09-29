using System;
using System.Security.Cryptography;
using System.Text;

namespace TripleDesExample
{
    class Program
    {
        private static TripleDESCryptoServiceProvider crypto = new TripleDESCryptoServiceProvider();
        private static MD5CryptoServiceProvider hashCrypto = new MD5CryptoServiceProvider();

        static void Main(string[] args)
        {
            var originalText = "This is the plain text message";
            var key = GetEncryptionKey();
            var encryptedText = Encrypt(originalText, key);
            var decryptedText = Decrypt(encryptedText, key);

            Console.WriteLine($"Original Text:\t{originalText}");
            Console.WriteLine($"Encrypted Text:\t{encryptedText}");
            Console.WriteLine($"Decrypted Text:\t{decryptedText}");
            Console.WriteLine($"Key: \t\t{key}");
        }

        private static string Encrypt(string plainText, string key)
        {
            // Encode message
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // Configure crypto
            crypto.Key = hashCrypto.ComputeHash(Encoding.UTF8.GetBytes(key));
            crypto.Mode = CipherMode.ECB;

            // Encrypt
            return Convert.ToBase64String(crypto.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length));
        }

        private static string Decrypt(string encodedText, string key)
        {
            // Decode message
            byte[] encodedBytes = Convert.FromBase64String(encodedText);

            // Configure crypto
            crypto.Key = hashCrypto.ComputeHash(Encoding.UTF8.GetBytes(key));
            crypto.Mode = CipherMode.ECB;

            // Decrypt
            return Encoding.UTF8.GetString(crypto.CreateDecryptor().TransformFinalBlock(encodedBytes, 0, encodedBytes.Length));
        }

        /// <summary>
        /// Creates a random encryption key
        /// </summary>
        /// <returns></returns>
        private static string GetEncryptionKey()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] key = new byte[256 / 8];
            rng.GetBytes(key);
            return BitConverter.ToString(key).Replace("-", string.Empty); // 'BitConverter' pairs bytes with a '-' seperating them
        }
    }
}
