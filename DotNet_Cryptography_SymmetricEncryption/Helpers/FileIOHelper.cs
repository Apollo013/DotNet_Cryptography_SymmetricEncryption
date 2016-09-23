using System;
using System.IO;
using System.Text;

namespace DotNet_Cryptography_SymmetricEncryption.Helpers
{
    public class FileIOHelper
    {
        public string SourceFileName { get; set; }
        public string DestinationFileName { get; set; }

        public FileIOHelper(string sourceFileName, string destinationFileName = null)
        {
            SourceFileName = sourceFileName;
            DestinationFileName = destinationFileName;
        }

        public string Read()
        {
            // Check file name specified
            if (String.IsNullOrWhiteSpace(SourceFileName))
            {
                throw new ArgumentNullException("Please provide a file name");
            }

            FileInfo file = new FileInfo(SourceFileName);

            // Check that file exists
            if (!file.Exists)
            {
                throw new FileNotFoundException("File does not exist");
            }

            // Read file
            StringBuilder sb = new StringBuilder();

            using (StreamReader reader = file.OpenText())
            {
                string s = "";
                while ((s = reader.ReadLine()) != null)
                {
                    sb.Append(s);
                }
            }

            return sb.ToString();
        }

        public void Write(string content)
        {

        }
    }
}
