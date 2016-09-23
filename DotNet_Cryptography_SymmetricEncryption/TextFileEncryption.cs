using DotNet_Cryptography_SymmetricEncryption.Helpers;

namespace DotNet_Cryptography_SymmetricEncryption
{
    public class TextFileEncryption
    {
        public static void Run()
        {
            string sourceFileName = "PlainText.txt";
            string destinationFileName = "EncryptedPlainText.txt";
            FileIOHelper fileIO = new FileIOHelper(sourceFileName, destinationFileName);
            string plainText = fileIO.Read();


        }

    }
}
