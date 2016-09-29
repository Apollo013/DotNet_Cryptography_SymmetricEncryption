using DotNet_Cryptography_SymmetricEncryption.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace DotNet_Cryptography_SymmetricEncryption
{
    public class TripleDESExample
    {
        public static void Run()
        {
            // Vars
            string plainText = "I'm a lumberjack and I don't care, I sleep all day and I drink all night";
            MD5CryptoServiceProvider hashCrypto = new MD5CryptoServiceProvider();
            EncryptionHelper crypto = new EncryptionHelper(new TripleDESCryptoServiceProvider());


            // Configure cipher
            var key = crypto.RandomKeyToString();
            crypto.Key = hashCrypto.ComputeHash(Encoding.UTF8.GetBytes(key));
            crypto.Mode = CipherMode.ECB;
            crypto.KeySize = 128;
            crypto.BlockSize = 64;


            // Encrypt / Decrypt file contents (we will use default properties)
            string encryptedText = crypto.Encrypt(plainText);
            string decryptedText = crypto.Decrypt(encryptedText); // Should eventually be the same as 'plainText'


            // Print Details
            Print("ORIGINAL TEXT", plainText);
            Print("IV", crypto.ConvertedIVBytes);
            Print("Key", crypto.ConvertedKeyBytes);
            Print("ENCRYPTED TEXT", encryptedText);
            Print("DECRYPTED TEXT", decryptedText);
        }

        private static void Print(string title, string message)
        {
            string divider = new string('-', 160);
            System.Console.WriteLine(divider);
            System.Console.WriteLine(title);
            System.Console.WriteLine(divider);
            System.Console.WriteLine(message);
            System.Console.WriteLine(divider);
            System.Console.WriteLine();
        }
    }
}
