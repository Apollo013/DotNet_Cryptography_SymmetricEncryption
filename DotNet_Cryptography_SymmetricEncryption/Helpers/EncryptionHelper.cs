using System;
using System.Security.Cryptography;
using System.Text;

namespace DotNet_Cryptography_SymmetricEncryption.Helpers
{
    public class EncryptionHelper
    {
        #region Properties
        private SymmetricAlgorithm Cipher { get; set; }
        public int KeySize
        {
            get { return Cipher.KeySize; }
            set { Cipher.KeySize = value; }
        }
        public int BlockSize
        {
            get { return Cipher.BlockSize; }
            set { Cipher.BlockSize = value; }
        }
        public PaddingMode PaddingMode
        {
            get { return Cipher.Padding; }
            set { Cipher.Padding = value; }
        }
        public CipherMode Mode
        {
            get { return Cipher.Mode; }
            set { Cipher.Mode = value; }
        }
        public byte[] Key
        {
            get { return Cipher.Key; }
            set { Cipher.Key = value; }
        }
        private ICryptoTransform Encryptor { get { return Cipher.CreateEncryptor(); } }
        private ICryptoTransform Decryptor { get { return Cipher.CreateDecryptor(); } }

        // No real need for these, just there so we can print them to console
        public string ConvertedKeyBytes { get { return BitConverter.ToString(Cipher.Key); } }
        public string ConvertedIVBytes { get { return BitConverter.ToString(Cipher.IV); } }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor that will create a 'RijndaelManaged' cypher
        /// </summary>
        public EncryptionHelper() : this(new RijndaelManaged())
        { }

        public EncryptionHelper(SymmetricAlgorithm cypher)
        {
            if (cypher == null)
            {
                throw new ArgumentNullException("Please supply a valid Symmetric Algorithm");
            }
            Cipher = cypher;
            InitializeCypher();
        }

        private void InitializeCypher()
        {
            // Do not set key size or block size here, as different algorithms use different legal key sizes
            Cipher.Padding = PaddingMode.ISO10126; ;
            Cipher.Mode = CipherMode.CBC;
            Cipher.Key = HexToByteArray(RandomKeyToString());
        }
        #endregion

        #region Encrypt / Decrypt Methods
        /// <summary>
        /// Encrypts a message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string Encrypt(string message)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(message);
            byte[] encryptedBytes = Encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Decrypts a message
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public string Decrypt(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] plainBytes = Decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(plainBytes);
        }
        #endregion

        #region Private Helpers
        /// <summary>
        /// Converts a hex string to bytes
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public byte[] HexToByteArray(string hexString)
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

        /// <summary>
        /// Creates a random encryption key and returns it's string representation
        /// </summary>
        /// <returns></returns>
        public string RandomKeyToString()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] key = new byte[KeySize / 8];
            rng.GetBytes(key);
            return BitConverter.ToString(key).Replace("-", string.Empty); // 'BitConverter' pairs bytes with a '-' seperating them
        }

        /// <summary>
        /// Creates a random encryption key
        /// </summary>
        /// <returns></returns>
        public byte[] RandomKey()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] key = new byte[KeySize / 8];
            rng.GetBytes(key);
            return key;
        }
        #endregion
    }
}
