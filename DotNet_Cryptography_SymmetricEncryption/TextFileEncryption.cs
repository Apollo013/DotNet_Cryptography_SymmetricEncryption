using DotNet_Cryptography_SymmetricEncryption.Helpers;
using System.IO;

namespace DotNet_Cryptography_SymmetricEncryption
{
    public class TextFileEncryption
    {
        public static void Run()
        {
            // Vars
            string currentDirectory = Directory.GetCurrentDirectory() + "\\Content\\";
            string sourceFileName = currentDirectory + "Text.txt";
            string destinationFileName = currentDirectory + "EncryptedText.txt";

            // Read File
            FileIOHelper fileIO = new FileIOHelper(sourceFileName, destinationFileName);
            string plainText = fileIO.Read();

            // Encrypt / Decrypt file contents
            EncryptionHelper crypto = new EncryptionHelper();
            string encryptedText = crypto.Encrypt(plainText);
            string decryptedText = crypto.Decrypt(encryptedText); // Should eventually be the same as 'plainText'

            // Write Encrypted text
            fileIO.Write(encryptedText);

            // Print Details
            Print("PLAIN TEXT FILE", sourceFileName);
            Print("ORIGINAL TEXT", plainText);
            Print("IV", crypto.ConvertedIVBytes);
            Print("Key", crypto.ConvertedKeyBytes);
            Print("ENCRYPTED TEXT", encryptedText);
            Print("DECRYPTED TEXT", decryptedText);
            Print("ENCRYPTED FILE", destinationFileName);
        }

        private static void Print(string title, string message)
        {
            string divider = new string('-', 100);
            System.Console.WriteLine(divider);
            System.Console.WriteLine(title);
            System.Console.WriteLine(divider);
            System.Console.WriteLine(message);
            System.Console.WriteLine(divider);
            System.Console.WriteLine();
        }
    }
}
