using System;
using System.Security.Cryptography;
using System.Text;

namespace DotNet_Cryptography_SymmetricEncryption.Helpers
{
    public class EncryptionHelper
    {
        private SymmetricAlgorithm _cipher;
        protected SymmetricAlgorithm Cipher
        {
            get
            {
                if (_cipher == null)
                {
                    // Create a default cipher if none specified
                    _cipher = CreateDefaultCipher();
                }
                return _cipher;
            }
            set { _cipher = value; }
        }
        public int KeySize { get; set; } = 256;
        public int BlockSize { get; set; } = 128;
        public PaddingMode PaddingMode { get; set; } = PaddingMode.ISO10126;
        public CipherMode CipherMode { get; set; } = CipherMode.CBC;
        private ICryptoTransform Encryptor { get { return Cipher.CreateEncryptor(); } }
        private ICryptoTransform Decryptor { get { return Cipher.CreateDecryptor(); } }

        public string ConvertedKeyBytes { get { return BitConverter.ToString(Cipher.Key); } }
        public string ConvertedIVBytes { get { return BitConverter.ToString(Cipher.IV); } }

        public string Encrypt(string message)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(message);
            byte[] encryptedBytes = Encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] plainBytes = Decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(plainBytes);
        }

        /// <summary>
        /// Creates a default cipher using 'RijndaelManaged'
        /// </summary>
        /// <returns></returns>
        private RijndaelManaged CreateDefaultCipher()
        {
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.KeySize = KeySize;
            cipher.BlockSize = BlockSize;
            cipher.Padding = PaddingMode;
            cipher.Mode = CipherMode;
            byte[] key = HexToByteArray(GetEncryptionKey());
            cipher.Key = key;
            return cipher;
        }

        /// <summary>
        /// Converts a hex string to bytes
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private byte[] HexToByteArray(string hexString)
        {
            if (0 != (hexString.Length % 2))
            {
                throw new ArgumentException("Hex string must be multiple of 2 in length");
            }

            int byteCount = hexString.Length / 2;
            byte[] byteValues = new byte[byteCount];
            for (int i = 0; i < byteCount; i++)
            {
                byteValues[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return byteValues;
        }

        private string GetEncryptionKey()
        {
            byte[] key = new byte[KeySize / 8];
            GenerateRandomBytes(key);
            return BitConverter.ToString(key).Replace("-", string.Empty);
        }

        /// <summary>
        /// Generates 
        /// </summary>
        /// <param name="buffer"></param>
        private void GenerateRandomBytes(byte[] buffer)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
        }
    }

}
