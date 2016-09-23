using System.Security.Cryptography;

namespace DotNet_Cryptography_SymmetricEncryption.Helpers
{
    public class EncryptionHelper
    {
        public int KeySize { get; set; } = 256;
        public int BlockSize { get; set; } = 128;
        public int SaltLength { get; set; }
        public byte[] SaltBytes { get; set; } = { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
        public PaddingMode PaddingMode { get; set; } = PaddingMode.ISO10126;
        public CipherMode CipherMode { get; set; } = CipherMode.CBC;


        public byte[] Encrypt(string message)
        {
            return null;
        }

        public string Decrypt()
        {
            return null;
        }
    }

}
