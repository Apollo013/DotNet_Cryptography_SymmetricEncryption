using System;
using System.IO;
using System.Text;

namespace DotNet_Cryptography_SymmetricEncryption.Helpers
{
    // Although the 'FileInfo' object has 'Encrypt' & 'Decrypt' functionality,
    // we will not be using them for these exercises

    /// <summary>
    /// File Helper
    /// </summary>
    public class FileIOHelper
    {
        public string SourceFileName { get; set; }
        public string DestinationFileName { get; set; }

        public FileIOHelper(string sourceFileName, string destinationFileName = null)
        {
            SourceFileName = sourceFileName;
            DestinationFileName = destinationFileName;
        }

        /// <summary>
        /// Reads string content from a file
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            // Check file name specified
            if (String.IsNullOrWhiteSpace(SourceFileName))
            {
                throw new ArgumentNullException("Please provide a file name");
            }

            // Create file object
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

        /// <summary>
        /// Writes string content out to a file
        /// </summary>
        /// <param name="content"></param>
        public void Write(string content)
        {
            // Check file name specified
            if (String.IsNullOrWhiteSpace(DestinationFileName))
            {
                throw new ArgumentNullException("Please provide a destination file name");
            }

            // Create file object
            FileInfo file = new FileInfo(DestinationFileName);

            // Ensure the file does not exist
            file.Delete();

            // Write to file
            using (StreamWriter writer = file.CreateText())
            {
                writer.WriteLine(content);
            }
        }
    }
}
