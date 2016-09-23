using System;

namespace DotNet_Cryptography_SymmetricEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
        }



        private static byte[] HexToByteArray(string hexString)
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
    }
}
